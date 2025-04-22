using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_4 : Transition
{
    public GameObject lastTarget;
    public GameObject video;

    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            video.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    
}
