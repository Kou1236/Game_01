using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController_1 : VideoController
{
    public Button myButton;          // 在 Inspector 中拖拽 Button 对象
    // public RenderTexture renderTexture;  // 在 Inspector 中拖拽 RenderTexture 对象

    public override void OnVideoStart()
    {
        // 隐藏按钮
        myButton.gameObject.SetActive(false);
    }

    // 当视频播放完毕时调用此方法
    public override void OnVideoFinished(VideoPlayer vp)
    {
        // 显示按钮
        myButton.gameObject.SetActive(true);
    }




}
