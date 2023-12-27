using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 窗口管理类
/// </summary>
public class UIMgr : MonoSingleton<UIMgr>
{
    private Transform Canvas;

    private Dictionary<string, GameObject> uiGameObjectDic = new Dictionary<string, GameObject>();
    //private Dictionary<string, WindowsBase> uiScriptsDic = new Dictionary<string, WindowsBase>();

    public override void Init()
    {
        base.Init();

        InitChildren();
    }

    public void InitChildren()
    {
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.transform.SetParent(this.transform);
        this.Canvas = canvasObj.transform;

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
        eventSystem.transform.SetParent(this.transform);
    }

    /// <summary>
    /// 打开窗户
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    public void OpenView<T>(params object[] parameters)
    {
        string uiName = typeof(T).Name;
        WindowsBase baseWnd = this.LoadViewAsset<T>(uiName);
        baseWnd.OnShow(parameters);
        GameObject uiObj = baseWnd.gameObject;
        RectTransform rectTransform = uiObj.GetComponent<RectTransform>();
        if (rectTransform == null) { Debug.LogError("UI窗口没有RectTransform组件"); return; }
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        uiObj.SetActive(true);
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void CloseView<T>()
    {
        string uiName = typeof(T).Name;
        if (uiGameObjectDic.ContainsKey(uiName) && uiGameObjectDic[uiName] != null)
        {
            GameObject uiObj = uiGameObjectDic[uiName];
            WindowsBase baseWnd = uiObj.GetComponent<WindowsBase>();
            baseWnd.Close();
            baseWnd.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 加载窗口资源
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    private WindowsBase LoadViewAsset<T>(string uiName)
    {
        GameObject uiObj;
        WindowsBase baseWnd;
        if (uiGameObjectDic.ContainsKey(uiName) && uiGameObjectDic[uiName] != null)//如果已经加载过则直接返回字典中的值，否则初始化
        {
            uiObj = uiGameObjectDic[uiName];
            baseWnd = uiObj.GetComponent<WindowsBase>();
        }
        else
        {
            uiObj = Instantiate(ResMgr.Instance.Load<GameObject>(uiName));
            baseWnd = uiObj.GetComponent<WindowsBase>();
            uiGameObjectDic.Add(uiName, uiObj);
            //uiScriptsDic.Add(uiName, baseWnd);
            baseWnd.Init();
        }
        uiObj.transform.SetParent(Canvas);
        Debug.Log(Canvas);
        Debug.Log(uiObj.transform.parent);

        return baseWnd;
    }

    /// <summary>
    /// 获取UI的GameObject
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public GameObject GetUIGameObject(string uiName)
    {
        return uiGameObjectDic[uiName];
    }

    /// <summary>
    /// 获取UI的脚本（方便直接调用UI脚本中的一些方法）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    //public T GetUIScript<T>() where T : WindowsBase
    //{
    //    return uiScriptsDic[typeof(T).ToString()] as T;
    //}
}