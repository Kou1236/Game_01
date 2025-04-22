using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_16 : Transition
{
    public GameObject lastTarget;

    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){

    }
}
