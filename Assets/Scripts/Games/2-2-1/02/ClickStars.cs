using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickStars : Singleton<ClickStars>
{
    
    public int stars = 6;
    public int index = 0;
    private KeepShake_1 keepShake;
    private Collider2D collider;
    public SpriteRenderer spriteRenderer;
    public float openDuration = 1f;

    private void Start(){
        keepShake = GetComponent<KeepShake_1>();
        collider = GetComponent<Collider2D>();
    }
    
    public void AddStar(){
        index++;
    }
    public void FinishGame(){
        if(index == stars){
            keepShake.enabled = true;
            EventHandler.CallStartDragEvent();
            collider.enabled = true;
        }
        else{
            Debug.Log("You need to click " + index + " stars to finish the game.");
        }
    }

    public void PopColor(){
        spriteRenderer.DOFade(1/(float)stars * index, openDuration);
    }
}
