using UnityEngine.UIElements;

namespace RadialMenu.Elements
{
    public class RadialMenuRoot : VisualElement
    {
        private RadialMenuElement _element;
        
        internal RadialMenuRoot(RadialMenuSettings settings)
        {
            name = "radial-menu-parent";
            
            Add(_element = new RadialMenuElement(settings));
            
            RegisterCallback<PointerMoveEvent>(OnPointerMove);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            _element.PointerPosition = evt.position;
        }
    }
}