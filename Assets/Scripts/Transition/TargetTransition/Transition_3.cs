using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_3 : Transition
{
    public GameObject lastTarget;
    public GameObject hands;
    public GameObject bubbles;
    public GameObject items;
    public GameObject time;

    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            hands.SetActive(true);
            bubbles.SetActive(true);
            items.SetActive(true);
            time.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    
}
