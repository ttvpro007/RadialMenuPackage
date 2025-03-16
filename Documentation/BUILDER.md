# RadialMenuBuilder Documentation

## Overview
The `RadialMenuBuilder` class provides an easy way to configure and create a radial menu in Unity. It allows you to customize various aspects such as size, colors, animations, actions, and more.

## Creating a Radial Menu
To create a radial menu, use the `Create` method and chain configuration methods before calling `Build<T>()`.

### Example Usage
```csharp
IRadialMenu radialMenu = RadialMenuBuilder
    .Create(items) // Define menu items
    .WithVisibilityAnimationTime(0.1f) // Set animation time
    .WithMainOuterRadius(236, 240) // Set main outer radius
    .WithMainInnerRadius(150, 160) // Set main inner radius
    .WithMainColors(Color.white, Color.red, Color.white, Color.red) // Define colors
    .WithActionOnClickOutOfBounds(RadialMenuAction.PerformItemAndClose) // Define actions
    .Build<ScaledRadialMenu>(); // Build the menu
```

## Methods

### `Create(params IRadialMenuItem[] items)`
Creates a new `RadialMenuBuilder` instance with the specified menu items.

### `Build<T>() where T : IRadialMenu, new()`
Builds and returns an instance of the radial menu with the specified type `T`.

### `WithVisibilityAnimationTime(float time)`
Sets the time in seconds for the visibility animation.

### `WithMainOuterRadius(int radius, int highlightedElementRadius = -1)`
Defines the outer radius of the main menu segments. Negative highlightedElementRadius value will just use radius value

### `WithMainInnerRadius(int radius, int highlightedElementRadius = -1)`
Defines the inner radius of the main menu segments. Negative highlightedElementRadius value will just use radius value

### `WithSegmentSpacing(int spacing)`
Sets the spacing between segments in the radial menu.

### `WithSegmentStrokeWidth(int width, int highlightedElementWidth = -1)`
Sets the stroke width of menu segments. Negative highlightedElementWidth value will just use radius value

### `InScreenPosition(Vector2 position)`
Defines the screen position where the radial menu should appear.

### `WithAdditionalStyleSheet(StyleSheet styleSheet)`
Applies an additional style sheet to customize the menu appearance.

### `WithMainColors(Color fillColor, Color highlightedColor, Color strokeColor, Color highlightedStrokeColor)`
Sets the colors for the main menu segments.

### `WithCenterElementRadius(int radius, int highlightedRadius = -1)`
Sets the radius of the center element.

### `WithCenterElementColors(Color fillColor, Color highlightedColor, Color strokeColor, Color highlightedStrokeColor)`
Defines the colors for the center element.

### `WithActionOnClickOutOfBounds(RadialMenuAction action, Action customCallback = null)`
Sets the action when clicking outside the radial menu.

### `WithActionOnClickInCenterElement(RadialMenuAction action, Action customCallback = null)`
Defines the action when clicking the center element.

### `WithDefaultActionOnClick(RadialMenuAction action, Action customCallback = null)`
Defines the default action when clicking a menu item.

### `WithDefaultCenterElement(Func<VisualElement> createFunc)`
Sets a custom center element by providing a function that returns a `VisualElement`.
