using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_13 : Transition
{
    public GameObject lastTarget;
    public GameObject popObject;
    public SpriteRenderer popRenderer;
    
    protected override void OnSceneTransitionEvent(){
        if(!lastTarget.activeInHierarchy){
            StartScene();
            this.gameObject.SetActive(false);
        }
    }

    void StartScene(){
        popObject.SetActive(true);
        popRenderer.sprite = FoodManager.Instance.foodList[FoodManager.Instance.currentIndex].foodSprites[FoodManager.Instance.finalCount];

    }


    
}
