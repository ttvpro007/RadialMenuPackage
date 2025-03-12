using UnityEngine.UIElements;

namespace RadialMenu
{
    public interface IRadialMenuItem
    {
        public VisualElement CreateItemElement();
        public VisualElement CreateCenterItemElementOnHover();
        public void OnItemPerform();
    }
}