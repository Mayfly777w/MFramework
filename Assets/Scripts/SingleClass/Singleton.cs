using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// E-mail:2123344255@qq.com
/// Des������ģʽ����
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {

    }
}

/// <summary>
/// ����ģʽ����
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>//ע���Լ��ΪT����Ϊ�䱾�������
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
    /// ��ʼ��������ֶ���������Ҫ�ֶ�ִ��Init()
    /// </summary>
    public virtual void Init()
    {
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}
