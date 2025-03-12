using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public static class RadialMenuBuilder
    {
        public static RadialMenuRaw New(params IRadialMenuItem[] items)
        {
            return new RadialMenuRaw(items)
            {
                DefaultPanelSettings = Resources.Load<PanelSettings>("DefaultRadialMenuPanelSettings"),
                BaseStyleSheet = Resources.Load<StyleSheet>("RadialMenuBaseStyle"),
            };
        }
    }

    public record RadialMenuRaw
    {
        internal PanelSettings DefaultPanelSettings;
        internal StyleSheet BaseStyleSheet;
        internal Vector2 Position;
        internal int OuterRadius;
        internal int InnerRadius;
        internal int SegmentSpacing;
        internal int CenterElementRadius;
        internal Color MainColor;
        internal Color MainStrokeColor;
        internal Color CenterElementColor;
        internal Color CenterElementStrokeColor;
        internal IRadialMenuItem[] Items;

        public RadialMenuRaw(IRadialMenuItem[] items)
        {
            Items = items;
        }
        
        public IRadialMenu Build()
        {
            return new SimpleRadialMenu(DefaultPanelSettings, BaseStyleSheet, 
                new RadialMenuSettings(Items, InnerRadius, OuterRadius, SegmentSpacing, CenterElementRadius, Position, 
                    MainColor, MainStrokeColor, CenterElementColor, CenterElementStrokeColor));
        }

        public RadialMenuRaw WithOuterRadius(int radius)
        {
            OuterRadius = radius;
            return this;
        }

        public RadialMenuRaw WithInnerRadius(int radius)
        {
            InnerRadius = radius;
            return this;
        }

        public RadialMenuRaw WithSegmentSpacing(int spacing)
        {
            SegmentSpacing = spacing;
            return this;
        }

        public RadialMenuRaw InPosition(Vector2 position)
        {
            Position = position;
            return this;
        }

        public RadialMenuRaw WithMainColors(Color fillColor, Color strokeColor)
        {
            MainColor = fillColor;
            MainStrokeColor = strokeColor;
            return this;
        }

        public RadialMenuRaw WithCenterElementColors(Color fillColor, Color strokeColor)
        {
            CenterElementColor = fillColor;
            CenterElementStrokeColor = strokeColor;
            return this;
        }
    }
}