using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    PressKeyA,
    PressKeyB,
}

/// <summary>
/// �¼�������
/// </summary>
public class EventMgr : MonoSingleton<EventMgr>
{
    private Dictionary<EventType, List<EventHandler>> events;

    public override void Init()
    {
        base.Init();
        events = new Dictionary<EventType, List<EventHandler>>();
    }

    /// <summary>
    /// ע���¼�����
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    public void Subscribe(EventType id, EventHandler handler)
    {
        if (handler == null)
        {
            Debug.Log("��ί��");
        }
        List<EventHandler> handlers;
        if (!events.TryGetValue(id, out handlers))//������ܻ�ȡ��ί���б���ע��ί���б�
        {
            handlers = new List<EventHandler>();
            events.Add(id, handlers);
        }
        if (handlers.Contains(handler))
        {
            throw new System.Exception("Try Subscribe Repeated Handler.");
        }
        handlers.Add(handler);
    }

    /// <summary>
    /// �Ƴ��¼�����
    /// </summary>
    /// <param name="id"></param>
    public void Unsubscribe(EventType id, EventHandler handler)
    {
        List<EventHandler> handlers;
        if (!events.TryGetValue(id, out handlers))
        {
            throw new System.Exception("No Target id Event Subscribed.");
        }
        if (handler == null)
        {
            events.Remove(id);
        }
        else
        {
            handlers.Remove(handler);
        }
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="id"></param>
    public void DispatchEvent(EventType id, params object[] param)
    {
        List<EventHandler> handlers;
        if (!events.TryGetValue(id, out handlers))
        {
            throw new System.Exception("No Target id Event Subscribed.");
        }
        foreach (EventHandler handler in handlers)
        {
            handler?.Invoke(param);
        }
    }
}

public delegate void EventHandler(params object[] param);