using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu.Elements
{
    public class RadialMenuRoot : VisualElement
    {
        public event Action<Vector2> PointerMoved;
        public event Action<Vector2> PointerClick; 
        
        private RadialMenuElement _element;
        
        internal RadialMenuRoot()
        {
            name = "radial-menu-parent";
            
            RegisterCallback<PointerMoveEvent>(OnPointerMove);
            RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            PointerMoved?.Invoke(evt.position);
        }

        private void OnClick(ClickEvent evt)
        {
            PointerClick?.Invoke(evt.position);
        }
    }
}