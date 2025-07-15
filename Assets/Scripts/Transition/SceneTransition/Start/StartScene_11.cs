using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_11 : StartScene
{
    public GameObject button;

    protected override void StartSceneAction(){
        StartCoroutine(StartButton());
    }

    IEnumerator StartButton(){
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }
}
