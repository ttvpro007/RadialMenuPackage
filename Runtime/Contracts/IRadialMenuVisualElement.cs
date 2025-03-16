using UnityEngine.UIElements;

namespace RadialMenu.Contracts
{
    public interface IRadialMenuVisualElement
    {
        public VisualElement GetVisualElement() => this as VisualElement;
    }
}