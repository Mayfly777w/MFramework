using cfg.MFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Blog:AFoolZWT.github.io
/// EMail:848832649@qq.com
/// Time:2023/02/17
/// Des����Ϸ���
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
    }

    private void Update()
    {
        //test();
    }

    private void test()
    {

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
        UIMgr.Instance.Init();
        PoolMgr.Instance.Init();
        TimerMgr.Instance.Init();
        AudioMgr.Instance.Init();
    }
}