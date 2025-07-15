using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class StartSceneAudio : StartScene
{
    public int audioIndex = 0;
    protected override void StartSceneAction(){
        AudioManager.Instance.PlayNext(audioIndex);
    }



    

}
