using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public record RadialMenuSettings(
        IRadialMenuItem[] Items,
        Vector2 ScreenPosition,
        int VisibilityAnimationTime,
        
        int MainOuterRadius,
        int MainInnerRadius,
        int MainSegmentSpacing,
        int MainSegmentStrokeWidth,
        
        Color MainColor,
        Color MainHighlightedColor,
        Color MainStrokeColor,
        Color MainHighlightedStrokeColor,
        
        int CenterElementRadius,
        Color CenterElementColor,
        Color CenterElementHighlightedColor,
        Color CenterElementStrokeColor,
        Color CenterElementHighlightedStrokeColor,
        
        RadialMenuAction ActionAppliedOnClickOutOfBounds,
        Action ActionAppliedOnClickOutOfBoundsCallback,
        RadialMenuAction ActionAppliedOnClickInCenterElement,
        Action ActionAppliedOnClickInCenterElementCallback,
        RadialMenuAction DefaultActionOnClick,
        Action DefaultActionOnClickCallback,
        Func<VisualElement> DefaultCenterElementCreateFunc)
    {
        public IRadialMenuItem[] Items { get; internal set; } = Items;
        public Vector2 ScreenPosition { get; internal set; } = ScreenPosition;
        public int VisibilityAnimationTime { get; internal set; } = VisibilityAnimationTime;
        
        public int MainInnerRadius { get; internal set; } = MainInnerRadius;
        public int MainOuterRadius { get; internal set; } = MainOuterRadius;
        public int MainSegmentSpacing { get; internal set; } = MainSegmentSpacing;
        public int MainSegmentStrokeWidth { get; internal set; } = MainSegmentStrokeWidth;
        
        public Color MainColor { get; internal set; } = MainColor;
        public Color MainHighlightedColor { get; internal set; } = MainHighlightedColor;
        public Color MainStrokeColor { get; internal set; } = MainStrokeColor;
        public Color MainHighlightedStrokeColor { get; internal set; } = MainHighlightedStrokeColor;
        
        public int CenterElementRadius { get; internal set; } = CenterElementRadius;
        public Color CenterElementColor { get; internal set; } = CenterElementColor;
        public Color CenterElementHighlightedColor { get; internal set; } = CenterElementHighlightedColor;
        public Color CenterElementStrokeColor { get; internal set; } = CenterElementStrokeColor;
        public Color CenterElementHighlightedStrokeColor { get; internal set; } = CenterElementHighlightedStrokeColor;
        
        public RadialMenuAction ActionAppliedOnClickOutOfBounds { get; internal set; } = ActionAppliedOnClickOutOfBounds;
        public Action ActionAppliedOnClickOutOfBoundsCallback { get; internal set; } = ActionAppliedOnClickOutOfBoundsCallback;
        public RadialMenuAction ActionAppliedOnClickInCenterElement { get; internal set; } = ActionAppliedOnClickInCenterElement;
        public Action ActionAppliedOnClickInCenterElementCallback { get; internal set; } = ActionAppliedOnClickInCenterElementCallback;
        public RadialMenuAction DefaultActionOnClick { get; internal set; } = DefaultActionOnClick;
        public Action DefaultActionOnClickCallback { get; internal set; } = DefaultActionOnClickCallback;
        public Func<VisualElement> DefaultCenterElementCreateFunc { get; internal set; } = DefaultCenterElementCreateFunc;
    }
}