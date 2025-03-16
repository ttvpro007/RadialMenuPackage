using System;
using RadialMenu.Contracts;
using RadialMenu.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public class RadialMenuBuilder
    {
        private readonly RadialMenuSettings _settings;
        private PanelSettings _defaultPanelSettings;
        private PanelSettings _panelSettings;
        private StyleSheet _baseStyleSheet;
        
        private RadialMenuBuilder(IRadialMenuItem[] items)
        {
            _settings = new RadialMenuSettings(items, Vector2.zero, 0, null,
                100, 90, 100, 90, 0, 1, 1,
                Color.white, Color.white, Color.white, Color.black, 
                50, 50, 1, 1, 
                Color.white, Color.black, Color.white, Color.black, 
                RadialMenuAction.None, null, 
                RadialMenuAction.Close, null, 
                RadialMenuAction.Close, null,
                () => null);
        }
        
        public T Build<T>() where T : IRadialMenu, new()
        {
            T result = new T();
            result.Initialize(_panelSettings == null ? _defaultPanelSettings : _panelSettings, _baseStyleSheet, _settings);
            return result;
        }
        
        public static RadialMenuBuilder Create(params IRadialMenuItem[] items)
        {
            return new RadialMenuBuilder(items)
            {
                _defaultPanelSettings = Resources.Load<PanelSettings>("DefaultRadialMenuPanelSettings"),
                _baseStyleSheet = Resources.Load<StyleSheet>("RadialMenuBaseStyle"),
            };
        }

        public RadialMenuBuilder WithVisibilityAnimationTime(float time)
        {
            _settings.VisibilityAnimationTime = Mathf.RoundToInt(time * 1000);
            return this;
        }

        public RadialMenuBuilder WithMainOuterRadius(int radius, int highlightedElementRadius = -1)
        {
            _settings.MainOuterRadius = radius;
            _settings.HighlightedElementOuterRadius = highlightedElementRadius < 0 ? radius : highlightedElementRadius;
            return this;
        }

        public RadialMenuBuilder WithMainInnerRadius(int radius, int highlightedElementRadius = -1)
        {
            _settings.MainInnerRadius = radius;
            _settings.HighlightedElementInnerRadius = highlightedElementRadius < 0 ? radius : highlightedElementRadius;
            return this;
        }

        public RadialMenuBuilder WithSegmentSpacing(int spacing)
        {
            _settings.MainSegmentSpacing = spacing;
            return this;
        }

        public RadialMenuBuilder WithSegmentStrokeWidth(int width, int highlightedElementWidth = -1)
        {
            _settings.MainSegmentStrokeWidth = width;
            _settings.MainSegmentHighlightedStrokeWidth = highlightedElementWidth < 0 ? width : highlightedElementWidth;
            return this;
        }

        public RadialMenuBuilder InScreenPosition(Vector2 position)
        {
            _settings.ScreenPosition = position;
            return this;
        }

        public RadialMenuBuilder WithAdditionalStyleSheet(StyleSheet styleSheet)
        {
            _settings.AdditionalStyleSheet = styleSheet;
            return this;
        }

        public RadialMenuBuilder WithMainColors(Color fillColor, Color highlightedColor, 
            Color strokeColor, Color highlightedStrokeColor)
        {
            _settings.MainColor = fillColor;
            _settings.MainHighlightedColor = highlightedColor;
            _settings.MainStrokeColor = strokeColor;
            _settings.MainHighlightedStrokeColor = highlightedStrokeColor;
            return this;
        }

        public RadialMenuBuilder WithCenterElementRadius(int radius, int highlightedRadius = -1)
        {
            _settings.CenterElementRadius = radius;
            _settings.CenterElementHighlightedRadius = highlightedRadius < 0 ? radius : highlightedRadius;
            return this;
        }

        public RadialMenuBuilder WithCenterElementStrokeWidth(int width, int highlightedWidth = -1)
        {
            _settings.CenterElementStrokeWidth = width;
            _settings.CenterElementHighlightedStrokeWidth = highlightedWidth < 0 ? width : highlightedWidth;
            return this;
        }

        public RadialMenuBuilder WithCenterElementColors(Color fillColor, Color highlightedColor, 
            Color strokeColor, Color highlightedStrokeColor)
        {
            _settings.CenterElementColor = fillColor;
            _settings.CenterElementHighlightedColor = highlightedColor;
            _settings.CenterElementStrokeColor = strokeColor;
            _settings.CenterElementHighlightedStrokeColor = highlightedStrokeColor;
            return this;
        }

        public RadialMenuBuilder WithActionOnClickOutOfBounds(RadialMenuAction action, Action customCallback = null)
        {
            if (action == RadialMenuAction.CustomAction && customCallback == null)
                Debug.LogWarning($"Action on click out of bounds is set to custom callback, but the callback is null");
            if (action != RadialMenuAction.CustomAction && customCallback != null)
                Debug.LogWarning($"Action on click out of bounds callback is not empty, but the callback type is not custom callback");
            
            _settings.ActionAppliedOnClickOutOfBounds = action;
            _settings.ActionAppliedOnClickOutOfBoundsCallback = customCallback;
            return this;
        }

        public RadialMenuBuilder WithActionOnClickInCenterElement(RadialMenuAction action, Action customCallback = null)
        {
            if (action == RadialMenuAction.CustomAction && customCallback == null)
                Debug.LogWarning($"Action on click out of bounds is set to custom callback, but the callback is null");
            if (action != RadialMenuAction.CustomAction && customCallback != null)
                Debug.LogWarning($"Action on click out of bounds callback is not empty, but the callback type is not custom callback");
            
            _settings.ActionAppliedOnClickInCenterElement = action;
            _settings.ActionAppliedOnClickInCenterElementCallback = customCallback;
            return this;
        }

        public RadialMenuBuilder WithDefaultActionOnClick(RadialMenuAction action, Action customCallback = null)
        {
            if (action == RadialMenuAction.CustomAction && customCallback == null)
                Debug.LogWarning($"Action on click out of bounds is set to custom callback, but the callback is null");
            if (action != RadialMenuAction.CustomAction && customCallback != null)
                Debug.LogWarning($"Action on click out of bounds callback is not empty, but the callback type is not custom callback");
            
            _settings.DefaultActionOnClick = action;
            _settings.DefaultActionOnClickCallback = customCallback;
            return this;
        }

        public RadialMenuBuilder WithDefaultCenterElement(Func<VisualElement> createFunc)
        {
            _settings.DefaultCenterElementCreateFunc = createFunc;
            return this;
        }
    }
}