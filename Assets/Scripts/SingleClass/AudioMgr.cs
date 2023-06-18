using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EMail:2123344255@qq.com
/// Time:2023/6/15
/// Des：声音管理类
/// </summary>
public class AudioMgr : MonoSingleton<AudioMgr>
{
    /// <summary>
    /// 背景音乐播放器（通过检查器赋值）
    /// </summary>
    public AudioSource music;

    /// <summary>
    /// 音效播放器（通过检查器赋值）
    /// </summary>
    public GameObject sound;

    /// <summary>
    /// 音效播放完成队列
    /// </summary>
    private Queue<AudioSource> endSoundQueue;

    /// <summary>
    /// 播放中音效列表
    /// </summary>
    private List<AudioSource> playingSoundList;

    /// <summary>
    /// 背景音乐音量大小
    /// </summary>
    private float musicVolume = 1.0f;

    /// <summary>
    /// 音效音量大小
    /// </summary>
    private float soundVolume = 1.0f;

    public override void Init()
    {
        base.Init();
        endSoundQueue = new Queue<AudioSource>();
        playingSoundList = new List<AudioSource>();
    }

    /// <summary>
    /// 设置背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void SetMusic(string name)
    {
        music.clip = ResMgr.Instance.Load<AudioClip>(name);
        music.volume = musicVolume;
        music.Play();
    }

    /// <summary>
    /// 设置音效
    /// </summary>
    /// <param name="name"></param>
    public void SetSound(string name)
    {
        AudioSource audioSource;
        if (endSoundQueue.Count > 0)
        {
            audioSource = endSoundQueue.Dequeue();
        }
        else
        {
            audioSource = sound.AddComponent<AudioSource>();
        }

        playingSoundList.Add(audioSource);
        audioSource.clip = ResMgr.Instance.Load<AudioClip>(name);
        audioSource.volume = soundVolume;
        audioSource.Play();
        TimerMgr.Instance.SetTimer(audioSource.clip.length, false, 1, () =>
         {
             StopSound(audioSource);
         });
    }

    /// <summary>
    /// 设置背景音乐大小
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        music.volume = musicVolume;
    }

    /// <summary>
    /// 设置音效大小
    /// </summary>
    /// <param name="volume"></param>
    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void StopMusic()
    {
        music.Pause();
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopSound(AudioSource audioSource)
    {
        if (playingSoundList.Contains(audioSource))
        {
            audioSource.Stop();
            playingSoundList.Remove(audioSource);
            endSoundQueue.Enqueue(audioSource);
        }
    }
}