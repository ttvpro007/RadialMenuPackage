using System;

namespace RadialMenu.MenuTypes
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