using System;
using RadialMenu.Contracts;
using RadialMenu.Elements;
using RadialMenu.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public abstract class RadialMenuBase : IRadialMenu
    {
        public RadialMenuState State { get; private set; }

        protected RadialMenuSettings Settings { get; private set; }
        protected UIDocument Document { get; private set; }
        protected RadialMenuRoot Root { get; private set; }
        protected RadialMenuElement Element { get; private set; }
        protected Vector2 ElementCenterScreenPos { get; private set; }
        
        private int _activeItemIndex;
        private bool _closeAfterOpen;

        void IRadialMenu.Initialize(PanelSettings panelSettings, StyleSheet baseStyleSheet, RadialMenuSettings settings)
        {
            Settings = settings;
            ElementCenterScreenPos = new Vector2(settings.ScreenPosition.x, Screen.height - settings.ScreenPosition.y);
            
            GameObject documentObj = new GameObject($"{GetType().Name} document object");
            GameObject.DontDestroyOnLoad(documentObj);
            Document = documentObj.AddComponent<UIDocument>();
            Document.panelSettings = panelSettings;
            Document.rootVisualElement.styleSheets.Add(baseStyleSheet);
            if (settings.AdditionalStyleSheet != null)
                Document.rootVisualElement.styleSheets.Add(settings.AdditionalStyleSheet);

            Document.rootVisualElement.Add(Root = new RadialMenuRoot());
            Root.Add(Element = new RadialMenuElement(settings));
            Root.PointerMoved += RootOnPointerMoved;
            Root.PointerClick += RootOnPointerClick;

            InitializeHiddenState();
            State = RadialMenuState.Hidden;
            SetRootVisibility(false);
        }

        ~RadialMenuBase()
        {
            Destroy();
        }

        private void RootOnPointerMoved(Vector2 pointerScreenPosition)
        {
            UpdateState(pointerScreenPosition);
            Element.UpdateCenterElement();
        }

        private void RootOnPointerClick(Vector2 position)
        {
            if (State != RadialMenuState.Visible)
                return;
            
            UpdateState(position);
            
            float pointerDistanceFromCenter = Vector2.Distance(position, ElementCenterScreenPos);
            if (pointerDistanceFromCenter > Settings.MainOuterRadius)
            {
                ApplyAction(Settings.ActionAppliedOnClickOutOfBounds, Settings.ActionAppliedOnClickOutOfBoundsCallback);
            }
            else if (pointerDistanceFromCenter < Settings.CenterElementRadius)
            {
                ApplyAction(Settings.ActionAppliedOnClickInCenterElement, Settings.ActionAppliedOnClickInCenterElementCallback);
            }
            else if (pointerDistanceFromCenter <= Settings.MainOuterRadius && pointerDistanceFromCenter > Settings.MainInnerRadius && _activeItemIndex >= 0)
            {
                Settings.Items[_activeItemIndex].OnItemPerform();
                Hide();
            }
            else
            {
                ApplyAction(Settings.DefaultActionOnClick, Settings.DefaultActionOnClickCallback);
            }
        }

        private void ApplyAction(RadialMenuAction action, Action customCallback)
        {
            switch (action)
            {
                case RadialMenuAction.None:
                    break;
                case RadialMenuAction.Close:
                    Hide();
                    break;
                case RadialMenuAction.PerformItem:
                    if (_activeItemIndex >= 0)
                        Settings.Items[_activeItemIndex].OnItemPerform();
                    break;
                case RadialMenuAction.PerformItemAndClose:
                    if (_activeItemIndex >= 0)
                        Settings.Items[_activeItemIndex].OnItemPerform();
                    Hide();
                    break;
                case RadialMenuAction.CustomAction:
                    customCallback?.Invoke();
                    break;
                case RadialMenuAction.CustomActionAndClose:
                    customCallback?.Invoke();
                    Hide();
                    break;
                
                default:
                    break;
            }
        }

        private void UpdateState(Vector2 pointerScreenPosition)
        {
            float angleStep = 360f / Settings.Items.Length;

            Vector2 direction = pointerScreenPosition - ElementCenterScreenPos;
            float pointerDistanceFromCenter = Vector2.Distance(pointerScreenPosition, ElementCenterScreenPos);
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;

            _activeItemIndex = -1;
            for (int i = 0; i < Settings.Items.Length; i++)
            {
                float startAngle = Mathf.Max(i * angleStep + Settings.MainSegmentSpacing, 1);
                float endAngle = Mathf.Max(startAngle + angleStep - Settings.MainSegmentSpacing, 2);
                
                bool isHoveredSegment = angle < endAngle && angle > startAngle;
                if (Settings.ActionAppliedOnClickOutOfBounds is RadialMenuAction.PerformItem or RadialMenuAction.Close
                    or RadialMenuAction.CustomAction)
                {
                    isHoveredSegment = isHoveredSegment && (pointerDistanceFromCenter < Settings.MainOuterRadius && pointerDistanceFromCenter > Settings.MainInnerRadius);
                }
                else
                {
                    isHoveredSegment = isHoveredSegment && pointerDistanceFromCenter > Settings.MainInnerRadius;
                }

                if (isHoveredSegment)
                    _activeItemIndex = i;
            }

            Element.ActiveItemIndex = _activeItemIndex;
            Element.CenterElementHovered = pointerDistanceFromCenter <= Settings.CenterElementRadius;
            Element.UpdatePointerPosition(pointerScreenPosition);
        }

        private void SetRootVisibility(bool isVisible)
        {
            Root.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        protected abstract void AnimateShow(Action callback);
        protected abstract void AnimateHide(Action callback);
        protected abstract void InitializeHiddenState();

        public void Show()
        {
            if (State != RadialMenuState.Hidden)
                return;

            State = RadialMenuState.ShowTransition;
            SetRootVisibility(true);
            AnimateShow(() =>
            {
                State = RadialMenuState.Visible;
                if (_closeAfterOpen)
                {
                    Hide();
                }
                _closeAfterOpen = false;
            });
        }

        public void Hide()
        {
            if (State == RadialMenuState.ShowTransition)
                _closeAfterOpen = true;
            
            if (State != RadialMenuState.Visible)
                return;
            
            State = RadialMenuState.HideTransition;
            AnimateHide(() =>
            {
                State = RadialMenuState.Hidden;
                SetRootVisibility(false);
            });
        }

        public void SetScreenPosition(Vector2 position)
        {
            ElementCenterScreenPos = new Vector2(position.x, Screen.height - position.y);
            Element.SetPosition(position);
        }

        public void Destroy()
        {
            if (Document && Document.gameObject)
                GameObject.Destroy(Document.gameObject);
        }
    }
}