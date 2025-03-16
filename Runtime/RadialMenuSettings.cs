using System;
using RadialMenu.Contracts;
using RadialMenu.Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public record RadialMenuSettings(
        IRadialMenuItem[] Items,
        Vector2 ScreenPosition,
        int VisibilityAnimationTime,
        StyleSheet AdditionalStyleSheet,
        
        int MainOuterRadius,
        int MainInnerRadius,
        int HighlightedElementOuterRadius,
        int HighlightedElementInnerRadius,
        int MainSegmentSpacing,
        int MainSegmentStrokeWidth,
        int MainSegmentHighlightedStrokeWidth,
        
        Color MainColor,
        Color MainHighlightedColor,
        Color MainStrokeColor,
        Color MainHighlightedStrokeColor,
        
        int CenterElementRadius,
        int CenterElementHighlightedRadius,
        int CenterElementStrokeWidth,
        int CenterElementHighlightedStrokeWidth,
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
        public StyleSheet AdditionalStyleSheet { get; internal set; } = AdditionalStyleSheet;
        
        public int MainInnerRadius { get; internal set; } = MainInnerRadius;
        public int MainOuterRadius { get; internal set; } = MainOuterRadius;
        public int HighlightedElementOuterRadius { get; internal set; } = HighlightedElementOuterRadius;
        public int HighlightedElementInnerRadius { get; internal set; } = HighlightedElementInnerRadius;
        public int MainSegmentSpacing { get; internal set; } = MainSegmentSpacing;
        public int MainSegmentStrokeWidth { get; internal set; } = MainSegmentStrokeWidth;
        public int MainSegmentHighlightedStrokeWidth { get; internal set; } = MainSegmentHighlightedStrokeWidth;
        
        public Color MainColor { get; internal set; } = MainColor;
        public Color MainHighlightedColor { get; internal set; } = MainHighlightedColor;
        public Color MainStrokeColor { get; internal set; } = MainStrokeColor;
        public Color MainHighlightedStrokeColor { get; internal set; } = MainHighlightedStrokeColor;
        
        public int CenterElementRadius { get; internal set; } = CenterElementRadius;
        public int CenterElementHighlightedRadius { get; internal set; } = CenterElementHighlightedRadius;
        public int CenterElementStrokeWidth { get; internal set; } = CenterElementStrokeWidth;
        public int CenterElementHighlightedStrokeWidth { get; internal set; } = CenterElementHighlightedStrokeWidth;
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