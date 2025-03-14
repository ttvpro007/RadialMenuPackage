using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu.Elements
{
    public class RadialMenuElement : VisualElement
    {
        public VisualElement[] ItemElements;
        internal int ActiveItemIndex = -1;
        
        private readonly RadialMenuSettings _settings;
        private readonly Vector2 _center;
        private Vector2 _pointerPosition;

        internal RadialMenuElement(RadialMenuSettings settings)
        {
            name = "radial-menu-element";
            
            _settings = settings;
            _center = new Vector2(settings.OuterRadius, settings.OuterRadius);
            ActiveItemIndex = -1;

            GenerateItemElements();
            generateVisualContent += GenerateVisualContent;

            style.width = settings.OuterRadius * 2f;
            style.height = settings.OuterRadius * 2f;

            _pointerPosition = settings.Position;
            SetPosition(settings.Position);
        }

        private Vector2 PolarToCartesian(float radius, float angle)
        {
            float rad = Mathf.Deg2Rad * angle;
            return new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius) + _center;
        }

        private void GenerateItemElements()
        {
            ItemElements = new VisualElement[_settings.Items.Length];
            float size = _settings.OuterRadius - _settings.InnerRadius; 
            for (int i = 0; i < _settings.Items.Length; i++)
            {
                var itemElement = _settings.Items[i].CreateItemElement();
                ItemElements[i] = itemElement;
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

            for (int i = 0; i < _settings.Items.Length; i++)
            {
                float startAngle = Mathf.Max(i * angleStep + _settings.SegmentSpacing, 1);
                float endAngle = Mathf.Max(startAngle + angleStep - _settings.SegmentSpacing, 2);
                float midAngle = (startAngle + endAngle) / 2;
                
                float radius = (_settings.OuterRadius + _settings.InnerRadius) / 2;
                Vector2 position = PolarToCartesian(radius, midAngle);

                bool isHoveredSegment = i == ActiveItemIndex;
                
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
                
                ItemElements[i].style.left = position.x - (ItemElements[i].resolvedStyle.width / 2);
                ItemElements[i].style.top = position.y - (ItemElements[i].resolvedStyle.height / 2);
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

        public void UpdatePointerPosition(Vector2 pointerPosition)
        {
            if (_pointerPosition == pointerPosition)
                return;

            _pointerPosition = pointerPosition;
            MarkDirtyRepaint();
        }

        public void SetPosition(Vector2 position)
        {
            style.left = position.x - _settings.OuterRadius;
            style.bottom = position.y - _settings.OuterRadius;
        }
    }
}