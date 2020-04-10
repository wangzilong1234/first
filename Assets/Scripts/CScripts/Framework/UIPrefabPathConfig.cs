using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrefabPathConfig  {

    public static Dictionary<Type, string> paths = new Dictionary<Type, string> {
        //{typeof(UILogin),"Prefab/UI_Login" },
        //{typeof(UIHome),"Prefab/UI_Home" },
        //{typeof(UITopBar),"Prefab/UI_TopBar" },
    };
}

public class UILayerConfig
{
    public static Dictionary<Type, Layer> layers = new Dictionary<Type, Layer>()
    {
        //{typeof(UILogin),Layer.LuanchLayer},
        //{typeof(UIHome),Layer.BackgroudLayer},
        //{typeof(UITopBar),Layer.InfoLayer},
    };
}

public class ViewStackConfig
{
    public static Dictionary<Type, bool> viewStacks = new Dictionary<Type, bool>() { };
}
