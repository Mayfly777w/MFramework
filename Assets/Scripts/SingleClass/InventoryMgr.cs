using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߹�����
/// </summary>
public class InventoryMgr : Singleton<InventoryMgr>
{
    private Dictionary<int, ItemInfo> itemInfos;//�洢ӵ�еĵ��ߵ����ݣ���Ϊ����Id��ֵΪ������Ϣ��ͨ��RefreshItemInfos()�������������ݴ���PlayerData��Items

    public override void Init()
    {
        base.Init();

        itemInfos = new Dictionary<int, ItemInfo>(64);
        if (DataMgr.Instance.CurrentGameSave.playerData != null && DataMgr.Instance.CurrentGameSave.playerData.Items != null)
        {
            this.InitItems(DataMgr.Instance.CurrentGameSave.playerData.Items);
        }
    }

    /// <summary>
    /// ��ʼ�����е���
    /// </summary>
    private void InitItems(Dictionary<int, int> items)
    {
        foreach (KeyValuePair<int, int> item in items)
        {
            int itemId = item.Key;
            int count = item.Value;
            itemInfos[itemId] = this.CreateItemInfo(itemId, count);
        }
    }

    /// <summary>
    /// ����������Ϣ
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private ItemInfo CreateItemInfo(int itemId, int count = 0)
    {
        Item cfg = CfgMgr.Instance.GetItem(itemId);
        return new ItemInfo(itemId, cfg.Name, cfg.Info, cfg.Classify, cfg.Type, count);//���ض�ӦitemId��ItemInfo
    }

    /// <summary>
    /// ��ӵ���
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="count"></param>
    public void AddItem(int itemId, int count = 1)
    {
        if (itemInfos.ContainsKey(itemId))
        {
            itemInfos[itemId].count += count;
        }
        else
        {
            itemInfos[itemId] = CreateItemInfo(itemId, count);
        }
    }

    /// <summary>
    /// ���ٵ���
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="count"></param>
    public void ReduceItem(int itemId, int count = 1)
    {
        if (itemInfos.ContainsKey(itemId))
        {
            if (itemInfos[itemId].count < count)//���߲���
            {

            }
            else
            {
                itemInfos[itemId].count -= count;
                if (itemInfos[itemId].count == 0)
                {

                }
            }
        }
        else//���߲�����
        {

        }
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public ItemInfo GetItem(int itemId)
    {
        ItemInfo item;
        if (!itemInfos.TryGetValue(itemId, out item))//û�е���
        {

        }
        return item;
    }

    /// <summary>
    /// ˢ�µ�����Ϣ��playerData��items
    /// </summary>
    public void RefreshItemInfos()
    {
        Dictionary<int, int> newItems = new Dictionary<int, int>();//����newItems����itemInfos���еĵ������ݴ���PlayerData��items
        foreach (KeyValuePair<int, ItemInfo> item in itemInfos)
        {
            newItems[item.Key] = item.Value.count;
        }
        DataMgr.Instance.CurrentGameSave.playerData.Items = newItems;
    }
}

public enum Item_Classify
{
    level = 1,
    clue = 2,
}

public enum Item_Type
{
    cost = 1,
    equipment = 2,
    others = 3,
}

public class ItemInfo
{
    public int id;
    public string name;
    public string info;
    public Item_Classify classify;
    public Item_Type type;
    public int count;
    public ItemInfo(int _id, string _name, string _info, int _classify, int _type, int _count)
    {
        id = _id;
        name = _name;
        info = _info;
        classify = (Item_Classify)_classify;
        type = (Item_Type)_type;
        count = _count;
    }

    public void ShowData()
    {
        Debug.Log(string.Format("id:{0}|name:{1},info:{2},classify:{3},type:{4},count:{5}", id, name, info, classify, type, count));
    }
}
