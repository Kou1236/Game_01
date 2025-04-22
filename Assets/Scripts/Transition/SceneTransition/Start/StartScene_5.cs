using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_5 : StartScene
{
    public GameObject scene_1;
    // public float duration = 2f;
    
    protected override void StartSceneAction(){

        scene_1.SetActive(true);
    }

    // IEnumerator StartCoroutine(){
    //     yield return new WaitForSeconds(1f);
    //     scene_1.SetActive(true);
        
    // }
}
