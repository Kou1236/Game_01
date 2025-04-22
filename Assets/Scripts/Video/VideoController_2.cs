using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController_2 : VideoController
{

    public GameObject nextScene;
    public GameObject backScene;

    // 当视频播放完毕时调用此方法
    public override void OnVideoFinished(VideoPlayer vp)
    {
        // 显示按钮
        videoPlayer.gameObject.SetActive(false);
        nextScene.SetActive(true);
        backScene.SetActive(true);
    }




}
