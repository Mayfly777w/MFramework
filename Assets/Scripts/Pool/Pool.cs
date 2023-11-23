using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/15
/// Des�������
/// </summary>
public class Pool
{
    /// <summary>
    /// �洢����
    /// </summary>
    private readonly Queue<GameObject> queue;

    /// <summary>
    /// ���еĴ洢����
    /// </summary>
    public int Count => queue.Count;

    /// <summary>
    /// �����λ��
    /// </summary>
    private readonly Transform transform;

    /// <summary>
    /// �����
    /// </summary>
    public Pool(string name)
    {
        GameObject objPool = new GameObject(name + "��");
        objPool.transform.SetParent(PoolMgr.Instance.transform);
        this.transform = objPool.transform;
        this.queue = new Queue<GameObject>();
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <returns></returns>
    public GameObject Dequeue()
    {
        GameObject gameObject = queue.Dequeue();
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        return gameObject;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="gameObject"></param>
    public void Enqueue(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(transform);
        gameObject.transform.position = transform.position;
        queue.Enqueue(gameObject);
    }
}