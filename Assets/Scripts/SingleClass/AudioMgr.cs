using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EMail:2123344255@qq.com
/// Time:2023/11/16
/// Des������������
/// </summary>
public class AudioMgr : MonoSingleton<AudioMgr>
{
    /// <summary>
    /// �������ֲ�����
    /// </summary>
    private AudioSource music;

    /// <summary>
    /// ��Ч������
    /// </summary>
    private GameObject sound;

    /// <summary>
    /// ��Ч������ɶ���
    /// </summary>
    private Queue<AudioSource> endSoundQueue;

    /// <summary>
    /// ��������Ч�б�
    /// </summary>
    private List<AudioSource> playingSoundList;

    /// <summary>
    /// ��������������С
    /// </summary>
    private float musicVolume = 1.0f;

    /// <summary>
    /// ��Ч������С
    /// </summary>
    private float soundVolume = 1.0f;

    public override void Init()
    {
        base.Init();

        InitChildren();

        endSoundQueue = new Queue<AudioSource>();
        playingSoundList = new List<AudioSource>();
    }

    public void InitChildren()
    {
        GameObject musicGameobject = new GameObject("Music");
        music = musicGameobject.AddComponent<AudioSource>();
        music.loop = true;
        musicGameobject.transform.SetParent(transform);

        sound = new GameObject("Sound");
        AudioSource soundAudioSource = sound.AddComponent<AudioSource>();
        soundAudioSource.loop = false;
        sound.transform.SetParent(transform);
    }

    /// <summary>
    /// ���ñ�������
    /// </summary>
    /// <param name="name"></param>
    public void SetMusic(string name)
    {
        music.clip = ResMgr.Instance.Load<AudioClip>(name);
        music.volume = musicVolume;
        music.Play();
    }

    /// <summary>
    /// ������Ч
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
        TimerMgr.Instance.SetTimer(audioSource.clip.length, () =>
         {
             StopSound(audioSource);
         }, false);
    }

    /// <summary>
    /// ���ñ������ִ�С
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        music.volume = musicVolume;
    }

    /// <summary>
    /// ������Ч��С
    /// </summary>
    /// <param name="volume"></param>
    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
    }

    /// <summary>
    /// ��ͣ��������
    /// </summary>
    public void StopMusic()
    {
        music.Pause();
    }

    /// <summary>
    /// ֹͣ��Ч
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