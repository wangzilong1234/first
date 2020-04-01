using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessengerCenter;
[XLua.Hotfix]
public class Test : MonoBehaviour
{
    private void Awake() {
        //ES3.Save<System.Int32>("KEY_SCORE",100,"PLAYERDATA.abs");
        EventCenter.AddParamsListener(100000,OnExpChange);
        //Debug.Log("__________________:"+"add");
    }

    public void OnExpChange(params object[] param) {
        Debug.Log("______________________:"+param[0]);
    }

    public void OnDestroy() {
        //Debug.Log("__________________:"+"remove");
        EventCenter.AddParamsListener(100000,OnExpChange);
    }

    void Start()
    {
        //Debug.Log(ES3.Load<System.Int32>("KEY_SCORE"));
        LuaMgr.Instance.InitLuaMain();
        EventCenter.BroadcastParams(MessengerCenter.CMSGID.GETNEWHERO,30);


    }

    public int Add(int a,int b) {
        //Debug.Log(BBB(a,b));
        return a+b;
    }

    public void CCC(string str) {
        //Debug.Log(str);
    }

    public string BBB(int a,int b) {
        return string.Format("c# a:{0},b:{1}",a,b);
    }


}
