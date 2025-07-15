using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
   
    // 音效的 AudioSource
    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource bgmSource;

    // 可通过 Inspector 批量拖入
    [Header("Audio Clips")]
    public AudioClip[] sfxclips;
    public AudioClip[] bgmclips;

    /// <summary>
    /// 播放指定名称的音效（PlayOneShot 不会打断当前正在播放的音效）
    /// </summary>
    public void PlaySFX(int index)
    {
        sfxSource.PlayOneShot(sfxclips[index]);
    }

    public void PlayBGM(int index)
    {
        bgmSource.clip = bgmclips[index];
        bgmSource.Play();
    }

    /// <summary>
    /// 停止所有音效
    /// </summary>
    public void StopAllSFX()
    {
        sfxSource.Stop();
    }

    public void StopAllBGM()
    {
        bgmSource.Stop();
    }



}
