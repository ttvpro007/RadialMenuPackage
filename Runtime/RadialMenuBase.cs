using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public abstract class RadialMenuBase : IRadialMenu
    {
        private UIDocument _document;
        
        internal RadialMenuBase(PanelSettings panelSettings, StyleSheet baseStyleSheet, RadialMenuSettings settings)
        {
            GameObject documentObj = new GameObject();
            _document = documentObj.AddComponent<UIDocument>();
            _document.panelSettings = panelSettings;
            _document.rootVisualElement.styleSheets.Add(baseStyleSheet);
            
            _document.rootVisualElement.Add(new WheelElement(settings)
            {
                name = "radial-menu__wheel"
            });
        }
    }

    public class WheelElement : VisualElement
    {
        private readonly RadialMenuSettings _settings;

    internal WheelElement(RadialMenuSettings settings)
    {
        _settings = settings;

        generateVisualContent += GenerateVisualContent;

        style.width = 0;
        style.height = 0;

        style.left = settings.Position.x;
        style.bottom = settings.Position.y;
    }

    private void GenerateVisualContent(MeshGenerationContext ctx)
    {
        const int SegmentCount = 10;
        var painter = ctx.painter2D;
        float angleStep = 360f / SegmentCount;

        for (int i = 0; i < SegmentCount; i++)
        {
            float startAngle = Mathf.Max(i * angleStep + _settings.SegmentSpacing, 1);
            float endAngle = Mathf.Max(startAngle + angleStep - _settings.SegmentSpacing, 2);

            painter.fillColor = _settings.MainColor;
            painter.strokeColor = _settings.MainStrokeColor;
            painter.lineWidth = 2;

            painter.BeginPath();
            painter.Arc(Vector2.zero, _settings.OuterRadius, startAngle, endAngle);
            painter.LineTo(PolarToCartesian(_settings.InnerRadius, endAngle));
            painter.Arc(Vector2.zero, _settings.InnerRadius, endAngle, startAngle, ArcDirection.CounterClockwise);
            painter.ClosePath();
            painter.Fill();
            painter.Stroke();
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

    // Converts polar coordinates to Cartesian (for arc points)
    private Vector2 PolarToCartesian(float radius, float angle)
    {
        float rad = Mathf.Deg2Rad * angle;
        return new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);
    }
}
    
    internal record RadialMenuSettings(int InnerRadius, int OuterRadius, int SegmentSpacing, int CenterElementRadius,
        Vector2 Position,
        Color MainColor, Color MainStrokeColor, Color CenterElementColor, Color CenterElementStrokeColor)
    {
        public int InnerRadius { get; } = InnerRadius;
        public int OuterRadius { get; } = OuterRadius;
        public int SegmentSpacing { get; } = SegmentSpacing;
        public int CenterElementRadius { get; } = CenterElementRadius;
        public Vector2 Position { get; } = Position;
        public Color MainColor { get; } = MainColor;
        public Color MainStrokeColor { get; } = MainStrokeColor;
        public Color CenterElementColor { get; } = CenterElementColor;
        public Color CenterElementStrokeColor { get; } = CenterElementStrokeColor;
    }
}