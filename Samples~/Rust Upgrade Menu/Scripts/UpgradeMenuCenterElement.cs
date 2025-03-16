using RadialMenu;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rust_Upgrade_Menu
{
    public class UpgradeMenuCenterElement : RadialMenuItemCenterElement
    {
        public UpgradeMenuCenterElement(Sprite icon, Color iconColor, string name, string description)
        {
            style.alignItems = Align.Center;
            style.justifyContent = Justify.Center;
            
            Add(new Image()
            {
                sprite = icon,
                tintColor = iconColor
            });
            Add(new Label()
            {
                text = name
            });
            Add(new Label()
            {
                text = description
            });
        }
    }
}