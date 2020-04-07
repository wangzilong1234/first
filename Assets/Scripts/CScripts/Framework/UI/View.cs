using System.Collections;
using UnityEngine;

namespace Game.UI
{
    public class View : MonoBehaviour, IView
    {
        public Animator animator;
        public AnimatorFinishEventTrigger animatorTrigger;
        public string showAnimName = "Open";
        public string hideAnimName = "Close";
        public event System.Action onStart;
        public event System.Action onClose;
        public virtual void OnStart() {

        }
        public virtual void OnClose() {

        }
        public virtual void OnBack()
        {

        }
        void Start()
        {
            OnStart();
            onStart?.Invoke();
        }

        private void OnDestroy()
        {
            OnClose();
            onClose?.Invoke();
        }

        IEnumerator PlayAnimation()
        {
            if (animator != null && animatorTrigger != null)
            {
                UIHelper.PlayAnimator(animator, showAnimName, animatorTrigger, () => {

                });
            }
            yield return null;
        }
    }
}

