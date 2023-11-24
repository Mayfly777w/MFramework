using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des����ʱ��������
/// </summary>
public class TimerMgr : MonoSingleton<TimerMgr>
{
    /// <summary>
    /// ִ���еļ�ʱ���б�
    /// </summary>
    private List<Timer> runningTimersList;

    public override void Init()
    {
        base.Init();

        runningTimersList = new List<Timer>();
    }

    private void Update()
    {
        for (int i = 0; i < runningTimersList.Count; i++)
        {
            runningTimersList[i]?.OnUpdate();//���Ϊ����ִ��
        }
    }

    /// <summary>
    /// ���ü�ʱ��
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="unscaled"></param>
    /// <param name="count"></param>
    /// <param name="action"></param>
    public void SetTimer(float duration, Action action, bool unscaled = true, int count = 1)
    {
        Timer timer = PoolMgr.Instance.GetObj<Timer>();

        timer.Open(duration, action, unscaled, count);
        runningTimersList.Add(timer);
    }

    /// <summary>
    /// �ֶ������ʱ��
    /// </summary>
    /// <param name="timer"></param>
    public void PushTimer(Timer timer)
    {
        if (!runningTimersList.Contains(timer))
        {
            runningTimersList.Add(timer);
        }
    }

    /// <summary>
    /// �Ƴ���ʱ��
    /// </summary>
    public void RemoveTimer(Timer timer)
    {
        if (runningTimersList.Contains(timer))
        {
            runningTimersList.Remove(timer);
        }
    }
}
