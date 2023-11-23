using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des：计时器状态类
/// </summary>
public enum TimerState
{
    Run = 0,
    Stop = 1,
}

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des：计时器类
/// </summary>
public class Timer
{
    /// <summary>
    /// 是否受TimeScale影响
    /// </summary>
    private bool unscaled;

    /// <summary>
    /// 过去的时间
    /// </summary>
    private float currentTime;

    /// <summary>
    /// 需要等待的时间
    /// </summary>
    private float waitTime;

    /// <summary>
    /// 持续时间
    /// </summary>
    private float duration;

    /// <summary>
    /// 循环次数
    /// </summary>
    private int count;

    /// <summary>
    /// 需要执行的函数
    /// </summary>
    private Action action;

    private TimerState state;

    /// <summary>
    /// 计时器
    /// </summary>
    public Timer()
    {

    }

    /// <summary>
    /// 开启计时器
    /// </summary>
    public void Open(float duration, bool unscaled, int count, Action action)
    {
        this.duration = duration;
        this.action = action;
        this.count = count;
        this.unscaled = unscaled;
        this.waitTime = (unscaled ? Time.time : Time.unscaledTime) + duration;//在开始就在等待时间中增加了当前时间

        this.state = TimerState.Run;
    }

    /// <summary>
    /// 计时器执行时
    /// </summary>
    public void OnUpdate()
    {
        if (state != TimerState.Run)//如果不在运行中
        {
            return;
        }

        if (unscaled)//如果受缩放时间影响
        {
            currentTime = Time.time;
        }
        else
        {
            currentTime = Time.unscaledTime;
        }

        if (currentTime >= waitTime)//如果过去的时间大于需要等待的时间
        {
            action?.Invoke();
            waitTime += duration;
            count--;

            if (count == 0)//如果循环次数为0
            {
                this.Colse();
            }
        }
    }

    /// <summary>
    /// 暂停计时器
    /// </summary>
    public void Stop()
    {
        state = TimerState.Stop;
    }

    /// <summary>
    /// 关闭计时器
    /// </summary>
    public void Colse()
    {
        state = TimerState.Stop;
        TimerMgr.Instance.Push(this);
    }
}
