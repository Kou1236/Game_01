using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueAudio : MonoBehaviour
{
    public int audioIndex;
    
    private void OnEnable()
    {
        AudioManager.Instance.fadeDuration = 0.21f;
        // 启动播放协程
        StartCoroutine(PlaySequentially());
    }

    private void OnDisable(){
        AudioManager.Instance.fadeDuration = 2f;
        AudioManager.Instance.PlayNext(audioIndex);
    }

    private IEnumerator PlaySequentially()
    {
        for(int i = 0; i <= PianoManager.Instance.noteList.Count - 1; i++)
        {
            AudioManager.Instance.PlayNext(PianoManager.Instance.noteList[i]);
            // 等待当前剪辑播放完毕
            yield return new WaitForSeconds(AudioManager.Instance.clips[PianoManager.Instance.noteList[i]].length);
        }
    }
}
