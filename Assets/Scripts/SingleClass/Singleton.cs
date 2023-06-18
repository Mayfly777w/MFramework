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
                //���������������ڴ���ʱ���������Ͻű���Awake������T��Awake(T��Awakeʵ�����Ǽ̳еĸ���ģ�
                //���Դ�ʱ����Ϊinstance��ֵ�������Awake�и�ֵ����ȻҲ���ʼ����������init()
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {
    
    }
}
