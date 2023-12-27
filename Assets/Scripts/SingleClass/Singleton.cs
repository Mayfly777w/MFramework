using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// E-mail:2123344255@qq.com
/// Des：单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : Singleton<T>,new()
{
    private static T instance;

    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }
}

/// <summary>
/// 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>//注意此约束为T必须为其本身或子类
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
                instance.Init();
            }

            return instance;
        }
    }

    /// <summary>
    /// 初始化，如果手动挂载则需要手动执行Init()
    /// </summary>
    public virtual void Init()
    {
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}
