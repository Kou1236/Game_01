using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    [Header("两个 AudioSource，用于交叉淡入淡出")]
    public AudioSource sourceA;
    public AudioSource sourceB;

    [Header("音轨列表")]
    public AudioClip[] clips;

    [Header("淡入/淡出时长 (秒)")]
    public float fadeDuration = 2f;

    // 当前使用的是哪一个源
    public AudioSource currentSource;
    public AudioSource nextSource;

    // 保存当前的淡入淡出协程
    private Coroutine fadeCoroutine;


    /// <summary>
    /// 切换并播放指定索引的音乐
    /// </summary>
    public void PlayNext(int num)
    {
        if (num < 0 || num >= clips.Length) return;
        AudioClip clip = clips[num];

        // 如果已有正在进行的淡入淡出，先停止
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(CrossFadeToNext(clip));
    }

    /// <summary>
    /// 立即停止所有播放，并重置状态
    /// </summary>
    public void Stop()
    {
        // 取消淡入淡出
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        // 停止两个 AudioSource 的播放
        currentSource.Stop();
        nextSource.Stop();

        // 恢复音量到默认值
        currentSource.volume = 1f;
        nextSource.volume    = 1f;
    }

    private IEnumerator CrossFadeToNext(AudioClip newClip)
    {
        // 准备下一源
        nextSource.clip   = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            currentSource.volume = Mathf.Lerp(1f, 0f, t);
            nextSource.volume    = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        // 完成后，停止旧源并交换引用
        currentSource.Stop();
        currentSource.volume = 1f;
        (currentSource, nextSource) = (nextSource, currentSource);

        // 淡入淡出协程结束
        fadeCoroutine = null;
    }

}
