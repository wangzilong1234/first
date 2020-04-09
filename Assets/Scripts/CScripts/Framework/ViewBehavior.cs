using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 封装MonoBehaviour的API
/// </summary>
public class ViewBehavior:MonoBehaviour {

    public void DelayCall(float delay, Action action = null) {
        StartCoroutine(DelayTimer(delay, action));
    }
    IEnumerator DelayTimer(float delay, Action action = null) {
        yield return new WaitForSeconds(delay);
        if (action != null) {
            action();
        }
        yield break;
    }
    /// <summary>
    /// 延迟到某一操作返回true 执行action
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="action"></param>
    public void DelayUntilCall(Func<bool> predicate,Action action = null ) {
        StartCoroutine(DelayUntilFunction(predicate, action));
    }
    private IEnumerator DelayUntilFunction(Func<bool> predicate, Action action = null) {
        yield return new WaitUntil(predicate);
        if(action!=null) {
            action();
        }
        yield break;
    }
    public void RepeatCall(float delay,bool isLoop,int repeatCount = 0,Action completeCallback = null,Action delayCallback = null) {
        StartCoroutine(RepeatTimer(delay, isLoop, repeatCount, delayCallback, completeCallback));
    }
    private IEnumerator RepeatTimer(float delay, bool isLoop, int repeatCount,Action action = null,Action completeCallback = null) {
        yield return new WaitForSeconds(delay);
        if (isLoop) {
            while (true) {
                if (action != null) {
                    action();
                    yield return new WaitForSeconds(delay);
                }
            }
        } else {
            while (repeatCount > 0) {
                if (action != null) {
                    action();
                }
                repeatCount = repeatCount - 1;
                if (repeatCount > 0) {
                    yield return new WaitForSeconds(delay);
                } else {                            
                    if (completeCallback != null) {
                        completeCallback();
                    }
                    yield break;
                }
            }
        }
    }
    public virtual void OnDestroy() {
        Log.Info("ViewBehavior OnDestory");
        StopAllCoroutines();
    }
}
