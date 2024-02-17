using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// ���ݹ�����
/// </summary>
public class DataMgr : MonoSingleton<DataMgr>
{
    /// <summary>
    /// ��Ϸ��������
    /// </summary>
    private enum GameDataType
    {
        GameSetting,
        GameSave,
        PlayerData,
    }
    //Ĭ�ϵ�һ���浵Ϊ�Զ��浵
    private const int GameSavesNum = 8;

    public GameSetting GameGlobalSetting { get; set; }

    private List<GameSaveData> GameSaves { get; set; }

    public GameSaveData CurrentGameSave { get; set; }

    public override void Init()
    {
        base.Init();

        InitGameData();

        //WARN������˵��Ӧ�÷����Ӧ�÷���Ϸ��ʼ��֮��ĵط���������Ϸ���ã������Ƿ�ʹ��
        //if (false)
        //{
        //    SetAutoSaveGameData();
        //}
    }

    /// <summary>
    /// �����Զ�������Ϸ
    /// </summary>
    public void SetAutoSaveGameData()
    {
        TimerMgr.Instance.SetTimer(5f * 60f, () =>
        {
            Save(0);
        });
    }

    /// <summary>
    /// ��ʼ����Ϸ����
    /// </summary>
    private void InitGameData()
    {
        string name;
        name = GameDataType.GameSetting.ToString();
        if (SaveGame.Exists(name))
        {
            GameGlobalSetting = SaveGame.Load<GameSetting>(name);
        }
        else
        {
            //TODO����ʵӦ�������ñ�������Ĭ�����ø���һ��
            GameGlobalSetting = new GameSetting
            {
                AutoSavingEnable = true,
                AutoSavingTime = 5f * 60f,
            };
        }

        name = GameDataType.GameSave.ToString();
        if (SaveGame.Exists(name))
        {
            GameSaves = SaveGame.Load<List<GameSaveData>>(name);
        }
        else
        {
            GameSaves = new List<GameSaveData>();
            for (int i = 0; i < GameSavesNum; i++)
            {
                GameSaveData gameSave = new GameSaveData();
                GameSaves.Add(gameSave);
            };
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="index">�浵λ��</param>
    public void Save(int index)
    {
        GameSaves[index] = this.CurrentGameSave;
        SaveGame.Save<List<GameSaveData>>(GameDataType.GameSave.ToString(), this.GameSaves);
    }

    /// <summary>
    /// ��ȡ
    /// </summary>
    /// <param name="index">�浵λ��</param>
    public void Load(int index)
    {
        CurrentGameSave = GameSaves[index];

        //WARN��Ӧ��ֱ����UI��������������
        if (CurrentGameSave == null)
        {
            Debug.LogError("���Զ�ȡ�մ浵");
        }
    }

    /// <summary>
    /// ����Ϸ
    /// </summary>
    public void NewGame()
    {
        CurrentGameSave = new GameSaveData()
        {
            playerData = new PlayerData
            {
                Name = "�����˿����Ů",
                Selection = new List<int>(),
                Items = new Dictionary<int, int>(),
            }
        };
    }

    private void OnApplicationQuit()
    {
        //�Զ�������0�浵
        Save(0);
    }
}