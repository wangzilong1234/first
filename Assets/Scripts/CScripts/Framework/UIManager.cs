using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Layer
{
    LuanchLayer,
    SceneLayer,
    BackgroudLayer,
    NormalLayer,
    InfoLayer,
    TipLayer,
    TopLayer,
}

public class UIManager : MonoSingleton<UIManager> {
    public GameObject uiRoot;
    public EventSystem eventSystem;
    public Camera camera2D;
    public Transform luanchLayer;
    public Vector2 vResolution = new Vector2(1024, 768);
    public Dictionary<Layer, Transform> layers;
    public Dictionary<Type, BaseView> views;
    protected override void Init()
    {
        base.Init();
        views = new Dictionary<Type, BaseView>();
        layers = new Dictionary<Layer, Transform>();
        uiRoot = GameObject.Find("UIRoot");
        eventSystem = GameObject.Find("EventSystem").transform.GetComponent<EventSystem>();
        camera2D = GameObject.Find("2DCamera").transform.GetComponent<Camera>();
        luanchLayer = GameObject.Find("LuanchLayer").transform;
        DontDestroyOnLoad(uiRoot);
        DontDestroyOnLoad(eventSystem);
        InitLayer();
    }
    private void InitLayer()
    {
        Log.Info("Init Layer");
        layers.Add(Layer.LuanchLayer, luanchLayer);
        layers.Add(Layer.SceneLayer, CreatLayer("SceneLayer", 700, 0));
        layers.Add(Layer.BackgroudLayer, CreatLayer("BackgroudLayer", 600, 1000));
        layers.Add(Layer.NormalLayer, CreatLayer("NormalLayer", 500, 2000));
        layers.Add(Layer.InfoLayer, CreatLayer("InfoLayer", 400, 3000));
        layers.Add(Layer.TipLayer, CreatLayer("TipLayer", 300, 4000));
        layers.Add(Layer.TopLayer, CreatLayer("TopLayer", 200, 5000));      
    }

    private Transform CreatLayer(string layerName, int panelDistance, int orderInLayer)
    {
        GameObject layer = new GameObject(layerName);
        UILayer uiLayer = layer.transform.GetComponent<UILayer>();
        if (uiLayer == null)
        {
            uiLayer = layer.AddComponent<UILayer>();
        }
        uiLayer.SetLayer(layerName,panelDistance,orderInLayer);
        return uiLayer.transform;
    }

    public T Open<T>() where T : BaseView
    {
        var type = typeof(T);
        if (views.ContainsKey(type)){        
            return (T)views[type];
        }
        if (!UIPrefabPathConfig.paths.ContainsKey(type))
        {
            Log.Error("UIPrefabPathConfig not ContainsKey: " + type.ToString());
            return null;
        }
        var path = UIPrefabPathConfig.paths[type];
        if (!UILayerConfig.layers.ContainsKey(type))
        {
            Log.Error("UILayerConfig not ContainsKey: " + type.ToString());
            return null;
        }
        //var layer = UILayerConfig.layers[type];
        //var Obj = ResourcesManager.LoadPrefab(path);
        //BaseView baseView = Obj.AddComponent<T>();   
        //UIHelper.SetParent(layers[layer], baseView.transform);
        //views.Add(type, baseView);
        //return (T)baseView;
        return null;
    }
}
