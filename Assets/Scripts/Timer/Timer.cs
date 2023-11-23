using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des����ʱ��״̬��
/// </summary>
public enum TimerState
{
    Run = 0,
    Stop = 1,
}

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/16
/// Des����ʱ����
/// </summary>
public class Timer
{
    /// <summary>
    /// �Ƿ���TimeScaleӰ��
    /// </summary>
    private bool unscaled;

    /// <summary>
    /// ��ȥ��ʱ��
    /// </summary>
    private float currentTime;

    /// <summary>
    /// ��Ҫ�ȴ���ʱ��
    /// </summary>
    private float waitTime;

    /// <summary>
    /// ����ʱ��
    /// </summary>
    private float duration;

    /// <summary>
    /// ѭ������
    /// </summary>
    private int count;

    /// <summary>
    /// ��Ҫִ�еĺ���
    /// </summary>
    private Action action;

    private TimerState state;

    /// <summary>
    /// ��ʱ��
    /// </summary>
    public Timer()
    {

    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    public void Open(float duration, bool unscaled, int count, Action action)
    {
        this.duration = duration;
        this.action = action;
        this.count = count;
        this.unscaled = unscaled;
        this.waitTime = (unscaled ? Time.time : Time.unscaledTime) + duration;//�ڿ�ʼ���ڵȴ�ʱ���������˵�ǰʱ��

        this.state = TimerState.Run;
    }

    /// <summary>
    /// ��ʱ��ִ��ʱ
    /// </summary>
    public void OnUpdate()
    {
        if (state != TimerState.Run)//�������������
        {
            return;
        }

        if (unscaled)//���������ʱ��Ӱ��
        {
            currentTime = Time.time;
        }
        else
        {
            currentTime = Time.unscaledTime;
        }

        if (currentTime >= waitTime)//�����ȥ��ʱ�������Ҫ�ȴ���ʱ��
        {
            action?.Invoke();
            waitTime += duration;
            count--;

            if (count == 0)//���ѭ������Ϊ0
            {
                this.Colse();
            }
        }
    }

    /// <summary>
    /// ��ͣ��ʱ��
    /// </summary>
    public void Stop()
    {
        state = TimerState.Stop;
    }

    /// <summary>
    /// �رռ�ʱ��
    /// </summary>
    public void Colse()
    {
        state = TimerState.Stop;
        TimerMgr.Instance.Push(this);
    }
}
