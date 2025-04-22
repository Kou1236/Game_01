using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_1 : Transition
{
    public GameObject slider;
    protected override void OnSceneTransitionEvent(){
        PopManager.Instance.StartPop();
        slider.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
