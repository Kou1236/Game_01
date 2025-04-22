using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_11 : Transition
{
    public GameObject character;
    public GameObject background;

    protected override void OnSceneTransitionEvent(){
        StartScene();
        this.gameObject.SetActive(false);
    }

    void StartScene(){
        character.SetActive(true);
        background.SetActive(true);
    }


    
}
