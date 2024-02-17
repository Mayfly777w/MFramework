using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ع�����
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
    /// ��ȡ��Ϸ����
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
    /// ������Ϸ����
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
            poolDic[name] = pool;
            poolDic[name].Enqueue(gameObject);
        }
    }
}
