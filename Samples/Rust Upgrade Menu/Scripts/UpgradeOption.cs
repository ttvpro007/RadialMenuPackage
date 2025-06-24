using RadialMenu.Contracts;
using UnityEngine;

namespace Rust_Upgrade_Menu
{
    public record UpgradeOption : IRadialMenuItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        
        private readonly Color _regularColor;
        private readonly Color _highlightedColor;
        
        public UpgradeOption(string upgradeName, string description, Sprite icon, Color regularColor, Color highlightedColor)
        {
            Name = upgradeName;
            Description = description;
            Icon = icon;
            
            _regularColor = regularColor;
            _highlightedColor = highlightedColor;
        }
        
        public IRadialMenuItemElement CreateItemElement() => new UpgradeMenuElement(Icon, _regularColor, _highlightedColor);

        public IRadialMenuItemCenterElement CreateItemCenterElement() => new UpgradeMenuCenterElement(Icon, _regularColor, Name, Description);

        public void OnItemPerform()
        {
            Debug.Log(Name + " performed");
        }
    }
}