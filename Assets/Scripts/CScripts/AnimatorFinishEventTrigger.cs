using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public class AnimatorFinishEventTrigger : MonoBehaviour {
    public Action<string> OnFinishAnimation; 
    private RuntimeAnimatorController ac;
    void Awake() {
        ac = GetComponent<UnityEngine.Animator>().runtimeAnimatorController;
        string methodName = "OnFinishAnimationTrigger";
        if(ac != null && ac.animationClips != null){ 
            foreach(var clip in ac.animationClips) {
                bool isAdd = false;
                if(clip.events != null){ 
                    foreach(var e in clip.events){ 
                        if(e.functionName == methodName){ 
                            isAdd = true;
                        }
                    }
                }
                if(isAdd){ 
                    continue;
                }
                var finishEvent = new AnimationEvent {
                    functionName=methodName,
                    stringParameter=clip.name,
                    time=clip.length
                };
                clip.AddEvent(finishEvent);
            }
        }
    }

    private void OnFinishAnimationTrigger(string name) {
        StartCoroutine(DoOnFinishAnimation(name));
    }

    private IEnumerator DoOnFinishAnimation(string name) {
        yield return new WaitForEndOfFrame();
        OnFinishAnimation?.Invoke(name);
    }
}
