using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/15
/// Des：对象池
/// </summary>
public class Pool
{
    /// <summary>
    /// 存储队列
    /// </summary>
    private readonly Queue<GameObject> queue;

    /// <summary>
    /// 队列的存储数量
    /// </summary>
    public int Count => queue.Count;

    /// <summary>
    /// 对象池位置
    /// </summary>
    private readonly Transform transform;

    /// <summary>
    /// 对象池
    /// </summary>
    public Pool(string name)
    {
        this.transform = new GameObject(name + "池").transform;
    }

    /// <summary>
    /// 出列
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
    /// 入列
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