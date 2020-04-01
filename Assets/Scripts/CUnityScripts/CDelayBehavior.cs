using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDelayBehavior :MonoSingleton<CDelayBehavior>
{
    protected override void Init() {
        base.Init();
    }
    public void RegisterDelayFunc(float time,System.Action action) {
        StartCoroutine(DelayTime(time,action));
    }
    IEnumerator DelayTime(float time,System.Action action) {
        yield return new WaitForSeconds(time);
        action?.Invoke();
       
    }
    public void UnregisterDelayFunc(float time,System.Action action) {
      
    }
}
