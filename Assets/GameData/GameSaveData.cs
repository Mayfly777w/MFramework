using System.Collections.Generic;
using Unity.VisualScripting;

public class GameSaveData
{
    public PlayerData playerData;
}

/// <summary>
/// �������
/// </summary>
public class PlayerData
{
    /// <summary>
    /// �������
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// �Ի�ѡ��
    /// </summary>
    public List<int> Selection { get; set; }

    /// <summary>
    /// ȫ������
    /// </summary>
    public Dictionary<int, int> Items { get; set; }
}

public class GameSetting
{
    /// <summary>
    /// �Ƿ������Զ�����
    /// </summary>
    public bool AutoSavingEnable { get; set; }

    //TODO��������Ӧ��Ū��ÿ�����������Ϊ���Զ�����ģ���ʱ�䱣������ԱȽϴ󣬵�ÿ��������Ϊ�Զ�������Ҫ��UI�߼��б�д����������޹���
    /// <summary>
    /// �Զ�����ʱ��
    /// </summary>
    public float AutoSavingTime { get; set; }
}