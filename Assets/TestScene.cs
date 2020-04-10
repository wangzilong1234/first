using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class TestScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var uiDef = XLuaManager.Instance.GetLuaEnv().DoString("return require'UIDef'")[0] as LuaTable;
        uiDef.Get<LuaTable>("Views").ForEach((string k,LuaTable v) => {
            Log.Info(v.Get<string>("prefab"));
        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
