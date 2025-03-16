using RadialMenu.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu.Elements
{
    public class RadialMenuElement : VisualElement
    {
        internal int ActiveItemIndex = -1;
        internal bool CenterElementHovered;
        
        private readonly RadialMenuSettings _settings;
        private readonly Vector2 _center;
        private IRadialMenuItemElement[] _itemElements;
        private IRadialMenuItemCenterElement[] _itemCenterElements;
        private VisualElement _centerElementHolder;
        private VisualElement _defaultCenterElement;
        private Vector2 _pointerPosition;

        internal RadialMenuElement(RadialMenuSettings settings)
        {
            name = "radial-menu-element";
            
            _settings = settings;
            _center = new Vector2(settings.MainOuterRadius, settings.MainOuterRadius);
            ActiveItemIndex = -1;
            CenterElementHovered = false;

            CreateCenterElementHolder();
            GenerateItemElements();
            generateVisualContent += GenerateVisualContent;

            style.width = settings.MainOuterRadius * 2f;
            style.height = settings.MainOuterRadius * 2f;

            _pointerPosition = settings.ScreenPosition;
            SetPosition(settings.ScreenPosition);
        }

        private Vector2 PolarToCartesian(float radius, float angle)
        {
            float rad = Mathf.Deg2Rad * angle;
            return new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius) + _center;
        }

        private void CreateCenterElementHolder()
        {
            _centerElementHolder = new VisualElement();
            Add(_centerElementHolder);
            _centerElementHolder.style.position = Position.Absolute;
            _centerElementHolder.style.width = _settings.CenterElementRadius * 2f;
            _centerElementHolder.style.height = _settings.CenterElementRadius * 2f;
            _centerElementHolder.style.top = _settings.MainOuterRadius - _settings.CenterElementRadius;
            _centerElementHolder.style.left = _settings.MainOuterRadius - _settings.CenterElementRadius;

            _defaultCenterElement = _settings.DefaultCenterElementCreateFunc?.Invoke();
            if (_defaultCenterElement == null)
                _defaultCenterElement = new VisualElement();
        }

        private void GenerateItemElements()
        {
            _itemElements = new IRadialMenuItemElement[_settings.Items.Length];
            _itemCenterElements = new IRadialMenuItemCenterElement[_settings.Items.Length];
            float size = _settings.MainOuterRadius - _settings.MainInnerRadius; 
            for (int i = 0; i < _settings.Items.Length; i++)
            {
                _itemElements[i] = _settings.Items[i].CreateItemElement();
                
                var itemElement = _itemElements[i].GetVisualElement();
                itemElement.style.position = Position.Absolute;
    
                itemElement.style.width = size;
                itemElement.style.height = size;

                Add(itemElement);
                
                _itemCenterElements[i] = _settings.Items[i].CreateItemCenterElement();
                var itemCenterElement = _itemCenterElements[i].GetVisualElement();
                _centerElementHolder.Add(itemCenterElement);
                itemCenterElement.style.display = DisplayStyle.None;
            }
        }

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;
            float angleStep = 360f / _settings.Items.Length;

            for (int i = 0; i < _settings.Items.Length; i++)
            {
                float startAngle = Mathf.Max(i * angleStep + _settings.MainSegmentSpacing, 1);
                float endAngle = Mathf.Max(startAngle + angleStep - _settings.MainSegmentSpacing, 2);
                float midAngle = (startAngle + endAngle) / 2;
                bool isHoveredSegment = i == ActiveItemIndex;
                
                float radius = (_settings.MainOuterRadius + _settings.MainInnerRadius) / 2;
                if (isHoveredSegment)
                    radius = (_settings.HighlightedElementOuterRadius + _settings.HighlightedElementInnerRadius) / 2;
                
                Vector2 position = PolarToCartesian(radius, midAngle);

                painter.fillColor = isHoveredSegment ? _settings.MainHighlightedColor : _settings.MainColor;
                painter.strokeColor = isHoveredSegment ? _settings.MainHighlightedStrokeColor : _settings.MainStrokeColor;
                painter.lineWidth = isHoveredSegment ? _settings.MainSegmentHighlightedStrokeWidth : _settings.MainSegmentStrokeWidth;

                painter.BeginPath();
                painter.Arc(_center, _settings.MainOuterRadius, startAngle, endAngle);
                painter.LineTo(PolarToCartesian(_settings.MainInnerRadius, endAngle));
                painter.Arc(_center, _settings.MainInnerRadius, endAngle, startAngle, ArcDirection.CounterClockwise);
                painter.ClosePath();
                painter.Fill();
                painter.Stroke();

                var item = _itemElements[i].GetVisualElement();
                item.style.left = position.x - (item.resolvedStyle.width / 2);
                item.style.top = position.y - (item.resolvedStyle.height / 2);
            }

            painter.fillColor = CenterElementHovered ? _settings.CenterElementHighlightedColor : _settings.CenterElementColor;
            painter.strokeColor = CenterElementHovered ? _settings.CenterElementHighlightedStrokeColor : _settings.CenterElementStrokeColor;
            painter.lineWidth = CenterElementHovered ? _settings.CenterElementHighlightedStrokeWidth : _settings.CenterElementStrokeWidth;
            painter.BeginPath();
            painter.Arc(_center, CenterElementHovered ? _settings.CenterElementHighlightedRadius : _settings.CenterElementRadius, 0, 360);
            painter.ClosePath();
            painter.Stroke();
            painter.Fill();
        }

        public void UpdatePointerPosition(Vector2 pointerPosition)
        {
            if (_pointerPosition == pointerPosition)
                return;

            _pointerPosition = pointerPosition;
            for (var i = 0; i < _itemElements.Length; i++)
            {
                _itemElements[i].SetHighlighted(i == ActiveItemIndex);
            }
            MarkDirtyRepaint();
        }

        public void UpdateCenterElement()
        {
            _defaultCenterElement.style.display = DisplayStyle.None;
            for (var i = 0; i < _itemCenterElements.Length; i++)
            {
                _itemCenterElements[i]?.SetVisible(i == ActiveItemIndex);
                
                if (i == ActiveItemIndex && _itemCenterElements[i] == null)
                    _defaultCenterElement.style.display = DisplayStyle.Flex;
            }

            if (ActiveItemIndex < 0)
                _defaultCenterElement.style.display = DisplayStyle.Flex;
        }

        public void SetPosition(Vector2 position)
        {
            style.left = position.x - _settings.MainOuterRadius;
            style.bottom = position.y - _settings.MainOuterRadius;
        }
    }
}