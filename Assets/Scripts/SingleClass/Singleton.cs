using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模式基类，需要手动执行Init初始化函数
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : Singleton<T>, new()
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
/// 自动单例模式基类，不需要手动执行Init初始化函数
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonAuto<T> where T : SingletonAuto<T>, new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
                instance.Init();
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
/// Mono单例模式基类，需要手动挂载在unity，并手动执行Init初始化函数
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>//注意此约束为T必须为其本身或子类
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public virtual void Awake()
    {
        instance = this as T;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        //依赖GameRoot类是为了开发过程中方便查看单例，可以删除
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}

/// <summary>
/// 自动Mono单例模式基类，不需要手动挂载在unity，不需要手动执行Init初始化函数
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingletonAuto<T> : MonoBehaviour where T : MonoSingletonAuto<T>//注意此约束为T必须为其本身或子类
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
        //依赖GameRoot类是为了开发过程中方便查看单例，可以删除
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}