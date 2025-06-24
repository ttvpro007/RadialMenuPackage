using RadialMenu;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rust_Upgrade_Menu
{
    public class UpgradeMenuElement : RadialMenuItemElement
    {
        private readonly Color _regularColor;
        private readonly Color _highlightedColor;
        private Image _image;

        public UpgradeMenuElement(Sprite icon, Color regularColor, Color highlightedColor)
        {
            Add(_image = new Image()
            {
                name = "element-icon"
            });
            
            _regularColor = regularColor;
            _highlightedColor = highlightedColor;

            _image.style.backgroundImage = new StyleBackground(icon);
            _image.style.unityBackgroundImageTintColor = regularColor;
        }
        
        protected override void OnHighlightStarted()
        {
            _image.style.unityBackgroundImageTintColor = _highlightedColor;
        }

        protected override void OnHighlightEnded()
        {
            _image.style.unityBackgroundImageTintColor = _regularColor;
        }
    }
}