using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_7 : Transition
{
    
    public GameObject Movie;

    protected override void OnSceneTransitionEvent(){
        StartScene();
        this.gameObject.SetActive(false);
    }

    void StartScene(){
        Movie.SetActive(true);
    }

    
}
