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
/// Des�����ù�����
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
            Debug.LogError("�Ƿ�δ�������ļ�����Addressable�������ƴ���Addressable�������ļ�������ӦΪ�����ļ����ļ�������");
            throw;
        }
    }

    public Test GetTest(int id)
    {
        return TABLES.TbTest.Get(id);
    }
}