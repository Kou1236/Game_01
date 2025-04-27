using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_9 : Transition
{

    public GameObject lastTarget;

    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){
        EventHandler.CallStartScrollEvent();
    }

    
}
