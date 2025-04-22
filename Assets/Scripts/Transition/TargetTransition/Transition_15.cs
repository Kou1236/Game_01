using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_15 : Transition
{
    public GameObject lastTarget;
    public SpriteRenderer Character;
    public Sprite CharacterSprite;
    
    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){
        Character.sprite = CharacterSprite;
    }
}
