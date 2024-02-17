using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具管理类
/// </summary>
public class InventoryMgr : Singleton<InventoryMgr>
{
    private Dictionary<int, ItemInfo> itemInfos;//存储拥有的道具的数据，键为道具Id，值为道具信息，通过RefreshItemInfos()方法将道具数据传入PlayerData的Items

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
    /// 初始化所有道具
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
    /// 创建道具信息
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private ItemInfo CreateItemInfo(int itemId, int count = 0)
    {
        Item cfg = CfgMgr.Instance.GetItem(itemId);
        return new ItemInfo(itemId, cfg.Name, cfg.Info, cfg.Classify, cfg.Type, count);//返回对应itemId的ItemInfo
    }

    /// <summary>
    /// 添加道具
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
    /// 减少道具
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="count"></param>
    public void ReduceItem(int itemId, int count = 1)
    {
        if (itemInfos.ContainsKey(itemId))
        {
            if (itemInfos[itemId].count < count)//道具不足
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
        else//道具不存在
        {

        }
    }

    /// <summary>
    /// 获取道具
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public ItemInfo GetItem(int itemId)
    {
        ItemInfo item;
        if (!itemInfos.TryGetValue(itemId, out item))//没有道具
        {

        }
        return item;
    }

    /// <summary>
    /// 刷新道具信息到playerData的items
    /// </summary>
    public void RefreshItemInfos()
    {
        Dictionary<int, int> newItems = new Dictionary<int, int>();//创建newItems，将itemInfos所有的道具数据传入PlayerData的items
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
