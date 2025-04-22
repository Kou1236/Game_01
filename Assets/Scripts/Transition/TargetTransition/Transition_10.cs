using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition_10 : Transition
{
    public GameObject openObject;
    public SpriteRenderer closeSpriteRenderer;
    public float duration = 1f;


    protected override void OnSceneTransitionEvent(){
        StartScene();
        this.gameObject.SetActive(false);
    }

    void StartScene(){
        Debug.Log("Start Scene");
        closeSpriteRenderer.DOFade(0f, duration).SetEase(Ease.InOutSine);
        DOVirtual.DelayedCall(duration, () => {
            openObject.SetActive(true);
        });
    }


    
}
