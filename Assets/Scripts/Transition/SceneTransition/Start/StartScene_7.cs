using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_7 : StartScene
{
    public GameObject scene_1;
    public float duration = 1f;
    
    protected override void StartSceneAction(){

        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine(){
        yield return new WaitForSeconds(duration);
        scene_1.SetActive(true);
        Debug.Log("StartScene_7");

        
        
    }
}
