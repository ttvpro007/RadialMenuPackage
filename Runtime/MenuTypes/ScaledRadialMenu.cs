using System;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace RadialMenu.MenuTypes
{
    public class ScaledRadialMenu : RadialMenuBase
    {
        private ValueAnimation<float> _animation;
        
        protected override void AnimateShow(Action callback)
        {
            _animation?.Recycle();
            _animation = Element.experimental.animation.Scale(1, Settings.VisibilityAnimationTime);
            _animation.from = 0;
            _animation.OnCompleted(() =>
            {
                _animation = null;
                callback?.Invoke();
            });
        }

        protected override void AnimateHide(Action callback)
        {
            _animation?.Recycle();
            _animation = Element.experimental.animation.Scale(0, Settings.VisibilityAnimationTime);
            _animation.OnCompleted(() =>
            {
                _animation = null;
                callback?.Invoke();  
            });
        }

        protected override void InitializeHiddenState()
        {
            Element.style.scale = new StyleScale(0);
        }
    }
}