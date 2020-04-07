using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ViewShowAnimator {
    public static void PlayAnimator(Animator animator,string clipName,AnimatorFinishEventTrigger eventTrigger,System.Action callback) {
        if (animator!=null) {
            animator.Play(clipName,-1,0);
        }
        if (eventTrigger!=null) {
            eventTrigger.OnFinishAnimation=aniName => {
                if (callback!=null&&aniName==clipName) {
                    callback();
                }
            };
        }
    }
}

public class View : MonoBehaviour,IView
{
    public Animator animator;
    public AnimatorFinishEventTrigger animatorTrigger;
    public string showAnimName = "Open";
    public string hideAnimName = "Close";
    public event System.Action onStart;
    public event System.Action onClose;
    public virtual void OnStart() {}
    public virtual void OnClose() {}
    void Start()
    {
        OnStart();
        onStart?.Invoke();
    }

    private void OnDestroy() {
        OnClose();
        onClose?.Invoke();
    }

    IEnumerator PlayAnimation() {
        if (animator!=null&&animatorTrigger!=null) {
            ViewShowAnimator.PlayAnimator(animator,showAnimName,animatorTrigger,() => {
                
            });
        }
        yield return null;
    }
}
