using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/15
/// Des������ع�����
/// </summary>
public class PoolMgr : MonoSingleton<PoolMgr>
{
    /// <summary>
    /// ������ֵ�
    /// </summary>
    private Dictionary<string, Pool> poolDic;

    public override void Init()
    {
        base.Init();

        poolDic = new Dictionary<string, Pool>();
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObj(string name)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            return poolDic[name].Dequeue();
        }
        else
        {
            return ResMgr.Instance.Load<GameObject>(name);
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="name"></param>
    /// <param name="gameObject"></param>
    public void PushObj(string name, GameObject gameObject)
    {
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Enqueue(gameObject);
        }
        else//���û�������
        {
            Pool pool = new Pool(name);//�½���
            pool.Enqueue(gameObject);
        }
    }
}
