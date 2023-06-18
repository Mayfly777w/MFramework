//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.item
{ 

public sealed partial class Item :  Bright.Config.BeanBase 
{
    public Item(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["name"].IsString) { throw new SerializationException(); }  Name = _json["name"]; }
        { if(!_json["desc"].IsString) { throw new SerializationException(); }  Desc = _json["desc"]; }
        { if(!_json["price"].IsNumber) { throw new SerializationException(); }  Price = _json["price"]; }
        { if(!_json["upgrade_to_item_id"].IsNumber) { throw new SerializationException(); }  UpgradeToItemId = _json["upgrade_to_item_id"]; }
        { var _j = _json["expire_time"]; if (_j.Tag != JSONNodeType.None && _j.Tag != JSONNodeType.NullValue) { { if(!_j.IsNumber) { throw new SerializationException(); }  ExpireTime = _j; } } else { ExpireTime = null; } }
        { if(!_json["batch_useable"].IsBoolean) { throw new SerializationException(); }  BatchUseable = _json["batch_useable"]; }
        { if(!_json["quality"].IsNumber) { throw new SerializationException(); }  Quality = (item.EQuality)_json["quality"].AsInt; }
        { if(!_json["exchange_stream"].IsObject) { throw new SerializationException(); }  ExchangeStream = item.ItemExchange.DeserializeItemExchange(_json["exchange_stream"]);  }
        { var __json0 = _json["exchange_list"]; if(!__json0.IsArray) { throw new SerializationException(); } ExchangeList = new System.Collections.Generic.List<item.ItemExchange>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { item.ItemExchange __v0;  { if(!__e0.IsObject) { throw new SerializationException(); }  __v0 = item.ItemExchange.DeserializeItemExchange(__e0);  }  ExchangeList.Add(__v0); }   }
        { if(!_json["exchange_column"].IsObject) { throw new SerializationException(); }  ExchangeColumn = item.ItemExchange.DeserializeItemExchange(_json["exchange_column"]);  }
        PostInit();
    }

    public Item(int id, string name, string desc, int price, int upgrade_to_item_id, int? expire_time, bool batch_useable, item.EQuality quality, item.ItemExchange exchange_stream, System.Collections.Generic.List<item.ItemExchange> exchange_list, item.ItemExchange exchange_column ) 
    {
        this.Id = id;
        this.Name = name;
        this.Desc = desc;
        this.Price = price;
        this.UpgradeToItemId = upgrade_to_item_id;
        this.ExpireTime = expire_time;
        this.BatchUseable = batch_useable;
        this.Quality = quality;
        this.ExchangeStream = exchange_stream;
        this.ExchangeList = exchange_list;
        this.ExchangeColumn = exchange_column;
        PostInit();
    }

    public static Item DeserializeItem(JSONNode _json)
    {
        return new item.Item(_json);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; private set; }
    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; private set; }
    /// <summary>
    /// 引用当前表
    /// </summary>
    public int UpgradeToItemId { get; private set; }
    public item.Item UpgradeToItemId_Ref { get; private set; }
    /// <summary>
    /// 过期时间
    /// </summary>
    public int? ExpireTime { get; private set; }
    /// <summary>
    /// 能否批量使用
    /// </summary>
    public bool BatchUseable { get; private set; }
    /// <summary>
    /// 品质
    /// </summary>
    public item.EQuality Quality { get; private set; }
    /// <summary>
    /// 道具兑换配置
    /// </summary>
    public item.ItemExchange ExchangeStream { get; private set; }
    public System.Collections.Generic.List<item.ItemExchange> ExchangeList { get; private set; }
    /// <summary>
    /// 道具兑换配置
    /// </summary>
    public item.ItemExchange ExchangeColumn { get; private set; }

    public const int __ID__ = 2107285806;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        this.UpgradeToItemId_Ref = (_tables["item.TbItem"] as item.TbItem).GetOrDefault(UpgradeToItemId);
        ExchangeStream?.Resolve(_tables);
        foreach(var _e in ExchangeList) { _e?.Resolve(_tables); }
        ExchangeColumn?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        ExchangeStream?.TranslateText(translator);
        foreach(var _e in ExchangeList) { _e?.TranslateText(translator); }
        ExchangeColumn?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "Price:" + Price + ","
        + "UpgradeToItemId:" + UpgradeToItemId + ","
        + "ExpireTime:" + ExpireTime + ","
        + "BatchUseable:" + BatchUseable + ","
        + "Quality:" + Quality + ","
        + "ExchangeStream:" + ExchangeStream + ","
        + "ExchangeList:" + Bright.Common.StringUtil.CollectionToString(ExchangeList) + ","
        + "ExchangeColumn:" + ExchangeColumn + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
