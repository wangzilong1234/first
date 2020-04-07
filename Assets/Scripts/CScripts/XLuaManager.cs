using System.IO;
using UnityEngine;
using XLua;

[Hotfix]
[LuaCallCSharp]
public class XLuaManager : MonoSingleton<XLuaManager>
{
    public const string luaAssetbundleAssetName = "Lua";
    public const string luaScriptsFolder = "LuaScripts";
    const string commonMainScriptName = "Common.Main";
    const string gameMainScriptName = "GameMain";
    const string hotfixMainScriptName = "XLua.HotfixMain";
    LuaEnv luaEnv = null;
    LuaUpdater luaUpdater = null;

    protected override void Init()
    {
        base.Init();     
        InitLuaEnv();
    }

    public bool HasGameStart
    {
        get;
        protected set;
    }

    public LuaEnv GetLuaEnv()
    {
        return luaEnv;
    }

    void InitLuaEnv()
    {
        luaEnv = new LuaEnv();
        HasGameStart = false;
        if (luaEnv != null)
        {
            luaEnv.AddLoader(CustomLoader);
          
        }
        else
        {
           
        }
    }

    public void OnInit()
    {
        if (luaEnv != null)
        {
            LoadScript(commonMainScriptName);
            luaUpdater = gameObject.GetComponent<LuaUpdater>();
            if (luaUpdater == null)
            {
                luaUpdater = gameObject.AddComponent<LuaUpdater>();
            }
            luaUpdater.OnInit(luaEnv);
        }
    }

    public string AssetbundleName
    {
        get;
        protected set;
    }

    // 重启虚拟机：热更资源以后被加载的lua脚本可能已经过时，需要重新加载
    // 最简单和安全的方式是另外创建一个虚拟器，所有东西一概重启
    public void Restart()
    {
        StopHotfix();
        Dispose();
        InitLuaEnv();
        OnInit();
    }

    public void SafeDoString(string scriptContent)
    {
        if (luaEnv != null)
        {
            try
            {
                luaEnv.DoString(scriptContent);
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);
                Log.Error(msg, null);
            }
        }
    }

    public void StartHotfix(bool restart = false)
    {
        if (luaEnv == null)
        {
            return;
        }

        if (restart)
        {
            StopHotfix();
            ReloadScript(hotfixMainScriptName);
        }
        else
        {
            LoadScript(hotfixMainScriptName);
        }
        SafeDoString("HotfixMain.Start()");
    }

    public void StopHotfix()
    {
        SafeDoString("HotfixMain.Stop()");
    }

    public void StartGame()
    {
        if (luaEnv != null)
        {
            LoadScript(gameMainScriptName);
            SafeDoString("GameMain.Start()");
            HasGameStart = true;
        }
    }
    
    public void ReloadScript(string scriptName)
    {
        SafeDoString(string.Format("package.loaded['{0}'] = nil", scriptName));
        LoadScript(scriptName);
    }

    void LoadScript(string scriptName)
    {
        SafeDoString(string.Format("require('{0}')", scriptName));
    }

    public static byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;
        filepath = filepath.Replace(".", "/") + ".lua";
#if UNITY_EDITOR     
        scriptPath = Path.Combine(Application.dataPath, luaScriptsFolder);
        scriptPath = Path.Combine(scriptPath, filepath);
        Log.Info("Load lua script : " + scriptPath);
        return GameUtility.SafeReadAllBytes(scriptPath);
       
#endif     
    }

    private void Update()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();

            if (Time.frameCount % 100 == 0)
            {
                luaEnv.FullGc();
            }
        }
    }

    private void OnLevelWasLoaded()
    {
        if (luaEnv != null && HasGameStart)
        {
            SafeDoString("GameMain.OnLevelWasLoaded()");
        }
    }

    private void OnApplicationQuit()
    {
        if (luaEnv != null && HasGameStart)
        {
            SafeDoString("GameMain.OnApplicationQuit()");
        }
    }

    public override void Dispose()
    {
        if (luaUpdater != null)
        {
            luaUpdater.OnDispose();
        }
        if (luaEnv != null)
        {
            try
            {
                luaEnv.Dispose();
                luaEnv = null;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);                
            }
        }
    }
}
