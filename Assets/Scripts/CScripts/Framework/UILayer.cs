using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour {
    public string layerName;
    public int planeDistance;
    public int orderInLayer;

    public int layer = 5;

    public void SetLayer(string layername, int paneldistance, int orderinLayer)
    {
        layerName = layername;
        planeDistance = paneldistance;
        orderInLayer = orderinLayer;
        UIHelper.SetParent(UIManager.Instance.uiRoot.transform, this.transform);
        this.gameObject.layer = layer;
        Canvas canvas = this.transform.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = this.gameObject.AddComponent<Canvas>();
        }
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = UIManager.Instance.camera2D;
        canvas.planeDistance = paneldistance;
        canvas.sortingLayerName = "UI";
        canvas.sortingOrder = orderinLayer;
        UnityEngine.UI.CanvasScaler canvasScaler = this.transform.GetComponent<UnityEngine.UI.CanvasScaler>();
        if(canvasScaler == null)
        {
            canvasScaler = this.gameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
        }
        canvasScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.referenceResolution = UIManager.Instance.vResolution;
        UnityEngine.UI.GraphicRaycaster graphicRaycaster = this.transform.GetComponent<UnityEngine.UI.GraphicRaycaster>();
        if (graphicRaycaster == null)
        {
            graphicRaycaster = this.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }




    }

}
