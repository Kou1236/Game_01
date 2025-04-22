using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_3 : StartScene
{
    public GameObject scene_1;
    public GameObject scene_2;
    public GameObject scene_3;
    
    protected override void StartSceneAction(){
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine(){
        scene_1.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        scene_2.SetActive(true);
        yield return new WaitForSeconds(3f);
        scene_3.SetActive(true);
    }
}
