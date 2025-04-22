using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_2 : Transition
{
    public GameObject video;
    public GameObject lastTarget;

    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            video.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    
}
