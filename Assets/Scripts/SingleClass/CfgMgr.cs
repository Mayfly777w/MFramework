using cfg;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 配置管理类
/// </summary>
public class CfgMgr : Singleton<CfgMgr>
{
    private Tables TABLES;

    public override void Init()
    {
        TABLES = new Tables(Loader);
    }

    private JSONNode Loader(string name)
    {
        try
        {
            TextAsset textAsset = ResMgr.Instance.Load<TextAsset>(name);
            string jsonContent = textAsset.text;
            return JSON.Parse(jsonContent);
        }
        catch
        {
            Debug.LogError("是否未给配置文件启用Addressable或其名称错误（Addressable中配置文件的名称应为配置文件的文件名本身）");
            throw;
        }
    }

    public cfg.Test GetTest(int id)
    {
        return TABLES.TbTest.Get(id);
    }

    public cfg.Item GetItem(int id)
    {
        return TABLES.TbItem.Get(id);
    }
}