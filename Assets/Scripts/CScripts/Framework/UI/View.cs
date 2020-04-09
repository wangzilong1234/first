using System.Collections;
using UnityEngine;

namespace Game.UI
{
    public abstract class View : MonoBehaviour
    {
        //public Animator animator;
        //public AnimatorFinishEventTrigger animatorTrigger;
        //public string showAnimName = "Open";
        //public string hideAnimName = "Close";
        public event System.Action onStartEvent;
        public event System.Action onCloseEvent;
        public abstract void OnStart();
        public abstract void OnShowUI();
        public abstract void OnClose();
        public abstract void OnBack();
        void Start()
        {
            OnStart();
            if (onStartEvent!=null) {
                onCloseEvent();
            }
        }

        private void OnDestroy()
        {
            OnClose();
            if (onCloseEvent!=null) {
                onCloseEvent();
            }
        }      
    }
}

