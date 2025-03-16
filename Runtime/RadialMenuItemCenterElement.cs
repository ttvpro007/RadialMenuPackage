using RadialMenu.Contracts;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public class RadialMenuItemCenterElement : VisualElement, IRadialMenuItemCenterElement
    {
        public void SetVisible(bool isVisible)
        {
            style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}