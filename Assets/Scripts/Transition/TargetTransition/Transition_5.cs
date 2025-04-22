using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_5 : Transition
{
    public GameObject lastTarget;
    public GameObject line;
    public GameObject button;
    public GameObject slider;
    public GameObject spawner;


    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            CameraController.Instance.isShaking = true;
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){
        line.SetActive(true);
        button.SetActive(true);
        slider.SetActive(true);
        spawner.SetActive(true);
    }

    
}
