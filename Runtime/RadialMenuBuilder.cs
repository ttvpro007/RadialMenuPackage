using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public class RadialMenuBuilder
    {
        internal PanelSettings DefaultPanelSettings;
        internal StyleSheet BaseStyleSheet;
        internal Vector2 Position;
        internal float VisibilityAnimationTime;
        internal int OuterRadius;
        internal int InnerRadius;
        internal int SegmentSpacing;
        internal int CenterElementRadius;
        internal Color MainColor;
        internal Color MainStrokeColor;
        internal Color CenterElementColor;
        internal Color CenterElementStrokeColor;
        internal IRadialMenuItem[] Items;

        private RadialMenuBuilder(IRadialMenuItem[] items)
        {
            Items = items;
        }
        
        public T Build<T>() where T : IRadialMenu, new()
        {
            T result = new T();
            result.Initialize(DefaultPanelSettings, BaseStyleSheet, 
                new RadialMenuSettings(Items, Mathf.RoundToInt(VisibilityAnimationTime * 1000), 
                    InnerRadius, OuterRadius, SegmentSpacing, CenterElementRadius, Position, 
                    MainColor, MainStrokeColor, CenterElementColor, CenterElementStrokeColor));
            return result;
        }

        public RadialMenuBuilder WithVisibilityAnimationTime(float time)
        {
            VisibilityAnimationTime = time;
            return this;
        }

        public RadialMenuBuilder WithOuterRadius(int radius)
        {
            OuterRadius = radius;
            return this;
        }

        public RadialMenuBuilder WithInnerRadius(int radius)
        {
            InnerRadius = radius;
            return this;
        }

        public RadialMenuBuilder WithSegmentSpacing(int spacing)
        {
            SegmentSpacing = spacing;
            return this;
        }

        public RadialMenuBuilder InPosition(Vector2 position)
        {
            Position = position;
            return this;
        }

        public RadialMenuBuilder WithMainColors(Color fillColor, Color strokeColor)
        {
            MainColor = fillColor;
            MainStrokeColor = strokeColor;
            return this;
        }

        public RadialMenuBuilder WithCenterElementColors(Color fillColor, Color strokeColor)
        {
            CenterElementColor = fillColor;
            CenterElementStrokeColor = strokeColor;
            return this;
        }
        
        public static RadialMenuBuilder Create(params IRadialMenuItem[] items)
        {
            return new RadialMenuBuilder(items)
            {
                DefaultPanelSettings = Resources.Load<PanelSettings>("DefaultRadialMenuPanelSettings"),
                BaseStyleSheet = Resources.Load<StyleSheet>("RadialMenuBaseStyle"),
            };
        }
    }
}