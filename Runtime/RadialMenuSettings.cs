using UnityEngine;

namespace RadialMenu
{
    internal record RadialMenuSettings(
        IRadialMenuItem[] Items,
        int InnerRadius,
        int OuterRadius,
        int SegmentSpacing,
        int CenterElementRadius,
        Vector2 Position,
        Color MainColor,
        Color MainStrokeColor,
        Color CenterElementColor,
        Color CenterElementStrokeColor)
    {
        public IRadialMenuItem[] Items { get; } = Items;
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