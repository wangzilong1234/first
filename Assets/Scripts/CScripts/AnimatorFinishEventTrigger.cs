using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Joywinds;

[XLua.LuaCallCSharp]
public class AnimatorFinishEventTrigger : MonoBehaviour {

    public Action<string> OnFinishAnimation;

    //private Action<AnimatorEventType> CustomEvent;
    //private Action<int> CustomEventByParam;
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
                var finishEvent = new UnityEngine.AnimationEvent();
                finishEvent.functionName = methodName;
                finishEvent.stringParameter = clip.name;
                finishEvent.time = clip.length;
                clip.AddEvent(finishEvent);
            }
        }
    }

    //public void RemoveAllEvent(){ 
    //    for(int i = 0; i < ac.animationClips.Length; i++){ 
    //        for(int j = 0; j < ac.animationClips[i].events.Length; j++){ 
    //            ac.animationClips[i].events[j] = null;
    //        }
    //    }
    //}

    private void OnFinishAnimationTrigger(string name) {
        StartCoroutine(DOOnFinishAnimation(name));
    }

    //public void OnCustomEventByInt(int eventType) { 
    //    AnimatorEventType t = (AnimatorEventType)eventType;
    //    if(CustomEvent != null) { 
    //        CustomEvent(t);
    //    }
    //}

    //public void OnCustomEventByString(string eventType) { 
    //    AnimatorEventType t = (AnimatorEventType)Enum.Parse(typeof(AnimatorEventType), eventType);
    //    if(CustomEvent != null){ 
    //        CustomEvent(t);
    //    }
    //}

    //public void OnCustomEventByParam(int param) { 
    //    if(CustomEventByParam != null){ 
    //        CustomEventByParam(param);
    //    }
    //}

    //public void RegisterCustonEvent(Action<AnimatorEventType> action) {
    //    CustomEvent = action;
    //}

    //public void RegisterCustonEventParam(Action<int> action) {
    //    CustomEventByParam = action;
    //}

    //public void UnRegisterCustonAllEvent() {
    //    CustomEvent = null;
    //    CustomEventByParam = null;
    //}

    IEnumerator DOOnFinishAnimation(string name) {
        yield return new WaitForEndOfFrame();
        OnFinishAnimation?.Invoke(name);
    }
}
