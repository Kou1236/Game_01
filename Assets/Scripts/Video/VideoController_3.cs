using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController_3 : VideoController
{

    public GameObject backgroud;
    public GameObject people;
    // 当视频播放完毕时调用此方法
    public override void OnVideoFinished(VideoPlayer vp)
    {
        // 显示按钮
        videoPlayer.gameObject.SetActive(false);
        backgroud.SetActive(false);
        people.SetActive(true);
        GamePlayManager.Instance.StartGame();
        // ShakeCamera.Instance.ShakeLayer();

    }




}
