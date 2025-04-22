using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_8 : Transition
{
    public GameObject background;
    public GameObject foreground;

    protected override void OnSceneTransitionEvent(){
        StartScene();
        this.gameObject.SetActive(false);
    }

    void StartScene(){
        background.SetActive(false);
        foreground.SetActive(true);
    }

    
}
