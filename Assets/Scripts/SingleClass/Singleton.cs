using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ģʽ���࣬��Ҫ�ֶ�ִ��Init��ʼ������
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {

    }
}

/// <summary>
/// �Զ�����ģʽ���࣬����Ҫ�ֶ�ִ��Init��ʼ������
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {

    }
}

/// <summary>
/// Mono����ģʽ���࣬��Ҫ�ֶ�������unity�����ֶ�ִ��Init��ʼ������
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>//ע���Լ��ΪT����Ϊ�䱾�������
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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {
        //����GameRoot����Ϊ�˿��������з���鿴����������ɾ��
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}

/// <summary>
/// �Զ�Mono����ģʽ���࣬����Ҫ�ֶ�������unity������Ҫ�ֶ�ִ��Init��ʼ������
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingletonAuto<T> : MonoBehaviour where T : MonoSingletonAuto<T>//ע���Լ��ΪT����Ϊ�䱾�������
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
        //����GameRoot����Ϊ�˿��������з���鿴����������ɾ��
        this.transform.SetParent(GameRoot.Instance.transform);
    }
}