using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // 在 Inspector 中拖拽 VideoPlayer 对象
    
    void OnEnable()
    {
        OnVideoStart();
        
        ClearRenderTexture();
        videoPlayer.Play();
        // 注册视频播放完毕的事件
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public virtual void OnVideoStart(){

    }
    // 当视频播放完毕时调用此方法
    public virtual void OnVideoFinished(VideoPlayer vp)
    {
    }

    void ClearRenderTexture()
    {
        videoPlayer.targetTexture.Release();
    }   


}
