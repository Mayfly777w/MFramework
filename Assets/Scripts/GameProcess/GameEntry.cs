using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏入口
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
        DataMgr.Instance.Init();
        TimerMgr.Instance.Init();
        PoolMgr.Instance.Init();
        AudioMgr.Instance.Init();
        UIMgr.Instance.Init();
        EventMgr.Instance.Init();
    }
}