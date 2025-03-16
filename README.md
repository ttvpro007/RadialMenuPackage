# Radial Menu

![Unity](https://img.shields.io/badge/Unity-UPM%20Package-blue)
![GitHub](https://img.shields.io/github/license/Fixer33/RadialMenuPackage)

Easily create procedural radial menus for your needs with the Radial Menu package for Unity.

## Features
- Fully customizable radial menu with adjustable segment spacing, colors, and actions.
- Supports UI Toolkit for smooth rendering and flexibility.
- Builder class for easy and structured menu creation.
- Example sample inspired by the **Rust** upgrade menu.

## Usage
The **RadialMenuBuilder** class provides an intuitive way to create radial menus. To create a new radial menu, use the `Create()` method and customize it using fluent methods before calling `Build<T>()`.

### Example Usage
```csharp
_radialMenu = RadialMenuBuilder
    .Create(_items) // Define menu items
    .WithVisibilityAnimationTime(0.1f) // Set animation time
    .WithMainOuterRadius(236, 240) // Define outer radius
    .WithMainInnerRadius(150, 160) // Define inner radius
    .WithMainColors(Color.white, Color.red, Color.white, Color.red) // Set colors
    .WithActionOnClickOutOfBounds(RadialMenuAction.PerformItemAndClose) // Define what happens when clicked out of radial menu bounds
    .Build<ScaledRadialMenu>(); // Build the menu with 0.1s scale animation on open and close
```

### Notable Methods
- `Create(params IRadialMenuItem[] items)`: Initializes the builder with menu items.
- `Build<T>() where T : IRadialMenu, new()`: Constructs the radial menu.
- `WithMainOuterRadius(int radius, int highlightedElementRadius)`: Sets the outer radius.
- `WithMainInnerRadius(int radius, int highlightedElementRadius)`: Sets the inner radius.
- `WithActionOnClickOutOfBounds(RadialMenuAction action, Action customCallback = null)`: Defines the action when clicking outside the menu.

For a full list of available methods, see the [Builder class documentation](https://github.com/Fixer33/RadialMenuPackage/blob/master/Documentation/BUILDER.md).

## Sample
This package includes a sample demonstrating how to create a **Rust-style upgrade menu** using the radial menu system.

To install the sample:
1. Open **Package Manager** in Unity.
2. Find **Radial Menu** in the list.
3. Click **Samples** and import the "Rust Upgrade Menu".

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

