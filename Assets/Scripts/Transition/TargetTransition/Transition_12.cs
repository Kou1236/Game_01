using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_12 : Transition
{
    public GameObject lastTarget;
    public GameObject button;
    
    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){
        button.SetActive(true);
    }


    
}
