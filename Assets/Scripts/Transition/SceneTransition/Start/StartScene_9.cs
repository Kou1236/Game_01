using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_9 : StartScene
{
    public GameObject scene_1;
    public GameObject scene_2;
    public float duration = 2f;
    
    protected override void StartSceneAction(){
        StartCoroutine(StartScene());
        
    }

    IEnumerator StartScene(){
        scene_1.SetActive(true);
        yield return new WaitForSeconds(duration);
        scene_2.SetActive(true);
    }

}
