using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口管理类
/// </summary>
public class UIMgr : MonoSingleton<UIMgr>
{
    public Transform Canvas;

    private const string assetPath = "Wnd/";

    private Dictionary<string, GameObject> uiGameObjectDic = new Dictionary<string, GameObject>();
    private Dictionary<string, object> uiScriptsDic = new Dictionary<string, object>();

    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 打开窗户
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    public void OpenView<T>(params object[] parameters)
    {
        string uiName = typeof(T).Name;
        BaseWnd baseWnd = this.LoadViewAsset<T>(uiName);
        baseWnd.OnShow(parameters);
        GameObject uiObj = baseWnd.gameObject;
        RectTransform rectTransform = uiObj.GetComponent<RectTransform>();
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
            BaseWnd baseWnd = uiObj.GetComponent<BaseWnd>();
            baseWnd.Close();
            baseWnd.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 加载窗口资源
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    private BaseWnd LoadViewAsset<T>(string uiName)
    {
        GameObject uiObj;
        BaseWnd baseWnd;
        if (uiGameObjectDic.ContainsKey(uiName) && uiGameObjectDic[uiName] != null)//如果已经加载过则直接返回字典中的值，否则初始化
        {
            uiObj = uiGameObjectDic[uiName];
            baseWnd = uiObj.GetComponent<BaseWnd>();
        }
        else
        {
            string resPath = assetPath + uiName;
            uiObj = Instantiate(Resources.Load<GameObject>(resPath));
            uiGameObjectDic.Add(uiName, uiObj);
            uiScriptsDic.Add(uiName, typeof(T));
            baseWnd = uiObj.GetComponent<BaseWnd>();
            baseWnd.Init();
        }
        uiObj.transform.SetParent(Canvas);

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
    /// 获取UI的脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetUIScript<T>() where T : BaseWnd
    {
        return uiScriptsDic[typeof(T).ToString()] as T;
    }
}