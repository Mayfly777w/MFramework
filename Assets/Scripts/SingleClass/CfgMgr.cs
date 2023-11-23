using cfg;
using cfg.MFramework;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Email:2123344255@qq.com
/// Time:2023/6/15
/// Des：配置管理类
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

    public Test GetTest(int id)
    {
        return TABLES.TbTest.Get(id);
    }
}