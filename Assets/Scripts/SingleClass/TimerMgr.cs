using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des：计时器管理器
/// </summary>
public class TimerMgr : MonoSingleton<TimerMgr>
{
    /// <summary>
    /// 已完成的计时器队列
    /// </summary>
    private Queue<Timer> endTimersQueue;

    /// <summary>
    /// 执行中的计时器列表
    /// </summary>
    private List<Timer> runningTimersList;

    public override void Init()
    {
        base.Init();

        endTimersQueue = new Queue<Timer>();
        runningTimersList = new List<Timer>();
    }

    private void Update()
    {
        for (int i = 0; i < runningTimersList.Count; i++)
        {
            runningTimersList[i]?.OnUpdate();//如果为空则不执行
        }
    }

    /// <summary>
    /// 设置计时器
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="unscaled"></param>
    /// <param name="count"></param>
    /// <param name="action"></param>
    public void SetTimer(float duration, Action action, bool unscaled = true, int count = 1)
    {
        Timer timer;
        if (endTimersQueue.Count > 0)
        {
            timer = endTimersQueue.Dequeue();
        }
        else
        {
            timer = new Timer();
        }

        timer.Open(duration, unscaled, count, action);
        runningTimersList.Add(timer);
    }

    /// <summary>
    /// 推入计时器
    /// </summary>
    public void Push(Timer timer)
    {
        if (runningTimersList.Contains(timer))
        {
            endTimersQueue.Enqueue(timer);
            runningTimersList.Remove(timer);
        }
    }
}
