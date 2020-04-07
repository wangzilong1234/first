using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public static class UIHelper
    {
        public static void PlayAnimator(Animator animator, string clipName, AnimatorFinishEventTrigger eventTrigger, System.Action callback)
        {
            if (animator != null)
            {
                animator.Play(clipName, -1, 0);
            }
            if (eventTrigger != null)
            {
                eventTrigger.OnFinishAnimation = aniName => {
                    if (callback != null && aniName == clipName)
                    {
                        callback();
                    }
                };
            }
        }
    }


}

