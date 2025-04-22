using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_2 : StartScene
{
    public GameObject phone;
    
    protected override void StartSceneAction(){
        Debug.Log("hello");
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine(){
        yield return new WaitForSeconds(1f);
        phone.SetActive(true);
    }
}
