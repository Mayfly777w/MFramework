using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池管理类
/// </summary>
public class PoolMgr : MonoSingleton<PoolMgr>
{
    /// <summary>
    /// 对象池字典
    /// </summary>
    private Dictionary<string, Pool> poolDic;

    public override void Init()
    {
        base.Init();

        poolDic = new Dictionary<string, Pool>();
    }

    /// <summary>
    /// 获取游戏对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string name)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            return poolDic[name].Dequeue();
        }
        else
        {
            return Instantiate(ResMgr.Instance.Load<GameObject>(name));
        }
    }

    /// <summary>
    /// 推入游戏对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="gameObject"></param>
    public void PushObj(string name, GameObject gameObject)
    {
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Enqueue(gameObject);
        }
        else//如果没有这个池
        {
            Pool pool = new Pool(name);//新建池
            poolDic[name] = pool;
            poolDic[name].Enqueue(gameObject);
        }
    }
}
