using RadialMenu.Contracts;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public abstract class RadialMenuItemElement : VisualElement, IRadialMenuItemElement
    {
        public bool IsHighlighted { get; private set; }
        
        public void SetHighlighted(bool isHighlighted)
        {
            if (IsHighlighted == isHighlighted)
                return;

            IsHighlighted = isHighlighted;
            
            if (IsHighlighted)
                OnHighlightStarted();
            else
                OnHighlightEnded();
        }

        protected abstract void OnHighlightStarted();
        protected abstract void OnHighlightEnded();
    }
}