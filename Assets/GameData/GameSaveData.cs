using System.Collections.Generic;
using Unity.VisualScripting;

public class GameSaveData
{
    public PlayerData playerData;
}

/// <summary>
/// 玩家数据
/// </summary>
public class PlayerData
{
    /// <summary>
    /// 玩家名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 对话选择
    /// </summary>
    public List<int> Selection { get; set; }

    /// <summary>
    /// 全部道具
    /// </summary>
    public Dictionary<int, int> Items { get; set; }
}

public class GameSetting
{
    /// <summary>
    /// 是否启用自动保存
    /// </summary>
    public bool AutoSavingEnable { get; set; }

    //TODO：有条件应该弄个每次玩家做出行为就自动保存的，按时间保存局限性比较大，但每次做出行为自动保存需要在UI逻辑中编写，跟这里就无关了
    /// <summary>
    /// 自动保存时间
    /// </summary>
    public float AutoSavingTime { get; set; }
}