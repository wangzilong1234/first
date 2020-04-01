using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LuaMgr:MonoSingleton<LuaMgr>
{
    public XLua.LuaEnv luaEnv { get; private set; }
    protected override void Init() {
        Debug.Log("LuaMgr Init");
        luaEnv = new XLua.LuaEnv();
        luaEnv.AddLoader(LuaLoader);
    }

    public void InitLuaMain() {
        var objs = luaEnv.DoString(string.Format("return require('{0}')","main"));
    }
    private byte[] LuaLoader(ref string fileName) {
        //Application.dataPath 表示Assets路径
        //定义lua路径
        string luaPath;
#if UNITY_EDITOR
        luaPath = Application.dataPath+"/Scripts/LuaScripts/"+fileName+".lua.txt";
#endif
        //读取lua路径中指定lua文件内容
        string strLuaContent = File.ReadAllText(luaPath);
        byte[] byArrayReturn = null; //返回数据
        //数据类型转换
        byArrayReturn=System.Text.Encoding.UTF8.GetBytes(strLuaContent);
        return byArrayReturn;
    }

}
