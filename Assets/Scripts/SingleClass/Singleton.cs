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
                //如果创建对象，则会在创建时调用其身上脚本的Awake即调用T的Awake(T的Awake实际上是继承的父类的）
                //所以此时无需为instance赋值，其会在Awake中赋值，自然也会初始化所以无需init()
                new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this as T;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
    
    }
}
