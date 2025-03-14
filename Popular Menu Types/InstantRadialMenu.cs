using System;
using RadialMenu;

namespace Popular_Menu_Types
{
    public class InstantRadialMenu : RadialMenuBase
    {
        protected override void AnimateShow(Action callback)
        {
            callback?.Invoke();
        }

        protected override void AnimateHide(Action callback)
        {
            callback?.Invoke();
        }

        protected override void InitializeHiddenState()
        {
            
        }
    }
}