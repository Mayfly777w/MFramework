using cfg.MFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Blog:AFoolZWT.github.io
/// EMail:848832649@qq.com
/// Time:2023/02/17
/// Des£ºÓÎÏ·Èë¿Ú
/// </summary>

public class GameEntry : MonoBehaviour
{
    private bool Inited = false;

    private void Start()
    {
        if (!Inited)
        {
            Init();
        }

        //SceneManager.LoadScene("");
        UIMgr.Instance.OpenView<TestWindows>();
    }

    private void Init()
    {
        DontDestroyOnLoad(this.gameObject);
        InitSingletons();

        Debug.Log("GameEntry Init Completed");
    }

    private void InitSingletons()
    {
        ResMgr.Instance.Init();
        CfgMgr.Instance.Init();
        TimerMgr.Instance.Init();
        PoolMgr.Instance.Init();
        AudioMgr.Instance.Init();
        UIMgr.Instance.Init();
        EventMgr.Instance.Init();
    }
}