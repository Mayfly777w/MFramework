using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 数据管理类
/// </summary>
public class DataMgr : MonoSingleton<DataMgr>
{
    /// <summary>
    /// 游戏数据类型
    /// </summary>
    private enum GameDataType
    {
        GameSetting,
        GameSave,
        PlayerData,
    }
    //默认第一个存档为自动存档
    private const int GameSavesNum = 8;

    public GameSetting GameGlobalSetting { get; set; }

    private List<GameSaveData> GameSaves { get; set; }

    public GameSaveData CurrentGameSave { get; set; }

    public override void Init()
    {
        base.Init();

        InitGameData();

        //WARN：按理说不应该放这里，应该放游戏初始化之类的地方，引用游戏设置，决定是否使用
        //if (false)
        //{
        //    SetAutoSaveGameData();
        //}
    }

    /// <summary>
    /// 设置自动保存游戏
    /// </summary>
    public void SetAutoSaveGameData()
    {
        TimerMgr.Instance.SetTimer(5f * 60f, () =>
        {
            Save(0);
        });
    }

    /// <summary>
    /// 初始化游戏数据
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
            //TODO：其实应该用配置表来配置默认设置更好一点
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
    /// 保存
    /// </summary>
    /// <param name="index">存档位置</param>
    public void Save(int index)
    {
        GameSaves[index] = this.CurrentGameSave;
        SaveGame.Save<List<GameSaveData>>(GameDataType.GameSave.ToString(), this.GameSaves);
    }

    /// <summary>
    /// 读取
    /// </summary>
    /// <param name="index">存档位置</param>
    public void Load(int index)
    {
        CurrentGameSave = GameSaves[index];

        //WARN：应该直接在UI层面避免这个问题
        if (CurrentGameSave == null)
        {
            Debug.LogError("尝试读取空存档");
        }
    }

    /// <summary>
    /// 新游戏
    /// </summary>
    public void NewGame()
    {
        CurrentGameSave = new GameSaveData()
        {
            playerData = new PlayerData
            {
                Name = "连体黑丝美少女",
                Selection = new List<int>(),
                Items = new Dictionary<int, int>(),
            }
        };
    }

    private void OnApplicationQuit()
    {
        //自动保存在0存档
        Save(0);
    }
}