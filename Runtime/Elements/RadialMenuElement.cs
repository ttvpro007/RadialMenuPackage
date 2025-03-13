using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu.Elements
{
    public class RadialMenuElement : VisualElement
    {
        internal Vector2 PointerPosition;
        
        private readonly RadialMenuSettings _settings;
        private readonly Vector2 _center;
        private readonly Vector2 _elementCenterScreenPos;
        private int _activeSegment = -1;
        private VisualElement[] _itemElements;

        internal RadialMenuElement(RadialMenuSettings settings)
        {
            name = "radial-menu-element";
            
            _settings = settings;
            _center = new Vector2(settings.OuterRadius, settings.OuterRadius);

            GenerateItemElements();
            generateVisualContent += GenerateVisualContent;

            style.width = settings.OuterRadius * 2f;
            style.height = settings.OuterRadius * 2f;

            PointerPosition = settings.Position;
            _elementCenterScreenPos = new Vector2(settings.Position.x, Screen.height - settings.Position.y);
            style.left = settings.Position.x - settings.OuterRadius;
            style.bottom = settings.Position.y - settings.OuterRadius;

            RegisterCallback<PointerMoveEvent>(OnPointerMove);
            RegisterCallback<ClickEvent>(OnClick);

            experimental.animation.Scale(1, 2000).from = 0;
        }

        private Vector2 PolarToCartesian(float radius, float angle)
        {
            float rad = Mathf.Deg2Rad * angle;
            return new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius) + _center;
        }

        private void GenerateItemElements()
        {
            _itemElements = new VisualElement[_settings.Items.Length];
            float size = _settings.OuterRadius - _settings.InnerRadius; 
            for (int i = 0; i < _settings.Items.Length; i++)
            {
                var itemElement = _settings.Items[i].CreateItemElement();
                _itemElements[i] = itemElement;
                itemElement.style.position = Position.Absolute;
    
                itemElement.style.width = size;
                itemElement.style.height = size;

                Add(itemElement);
            }
        }

        private void GenerateVisualContent(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;
            float angleStep = 360f / _settings.Items.Length;

            Vector2 direction = PointerPosition - _elementCenterScreenPos;
            float pointerDistanceFromCenter = Vector2.Distance(PointerPosition, _elementCenterScreenPos);
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;

            _activeSegment = -1;
            for (int i = 0; i < _settings.Items.Length; i++)
            {
                float startAngle = Mathf.Max(i * angleStep + _settings.SegmentSpacing, 1);
                float endAngle = Mathf.Max(startAngle + angleStep - _settings.SegmentSpacing, 2);
                float midAngle = (startAngle + endAngle) / 2;
                
                float radius = (_settings.OuterRadius + _settings.InnerRadius) / 2;
                Vector2 position = PolarToCartesian(radius, midAngle);
                
                bool isHoveredSegment = pointerDistanceFromCenter < _settings.OuterRadius && pointerDistanceFromCenter > _settings.InnerRadius;
                isHoveredSegment = isHoveredSegment && (angle < endAngle && angle > startAngle);

                if (isHoveredSegment)
                    _activeSegment = i;
                
                painter.fillColor = isHoveredSegment ? Color.black : _settings.MainColor;
                painter.strokeColor = _settings.MainStrokeColor;
                painter.lineWidth = 2;

                painter.BeginPath();
                painter.Arc(_center, _settings.OuterRadius, startAngle, endAngle);
                painter.LineTo(PolarToCartesian(_settings.InnerRadius, endAngle));
                painter.Arc(_center, _settings.InnerRadius, endAngle, startAngle, ArcDirection.CounterClockwise);
                painter.ClosePath();
                painter.Fill();
                painter.Stroke();
                
                _itemElements[i].style.left = position.x - (_itemElements[i].resolvedStyle.width / 2);
                _itemElements[i].style.top = position.y - (_itemElements[i].resolvedStyle.height / 2);
            }

            // // Draw outer circle outline
            // painter.strokeColor = Color.green;
            // painter.lineWidth = 2;
            // painter.BeginPath();
            // painter.Arc(Vector2.zero, _settings.OuterRadius, 0, 360);
            // painter.ClosePath();
            // painter.Stroke();
            //
            // // Draw inner circle outline
            // painter.BeginPath();
            // painter.Arc(Vector2.zero, _settings.InnerRadius, 0, 360);
            // painter.ClosePath();
            // painter.Stroke();
        }

        private void OnClick(ClickEvent evt)
        {
            if (_activeSegment < 0 || _activeSegment >= _settings.Items.Length)
                return;
            
            _settings.Items[_activeSegment].OnItemPerform();
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            MarkDirtyRepaint();
        }
    }
}