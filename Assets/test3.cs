using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealM;

public class test3 : MonoBehaviour
{
    //System.Action action = () => { Debug.Log("3_______:"+Time.realtimeSinceStartup); };
    public void Awake() {
        //this.Hider(1f);
        //this.Shower(2f);
        //this.Looper(3f,5,true,(i) => { Debug.Log(12-(i)*3); });
        //this.Delayer(5,() => { DestroyImmediate(this.gameObject); });
        //this.Delayer(7,() => Debug.Log(gameObject.name));
        //this.DelayerUnscaled(10,() => { Debug.Log("_______________________"); });
        //this.Delayer(3,() => { Debug.Log("log start"); });
        //this.Delayer(4,)
        LuaMgr.Instance.InitLuaMain();
    }

    private void Update() {
        
    }


}
