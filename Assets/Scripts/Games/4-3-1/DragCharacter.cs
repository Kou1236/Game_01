using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragCharacter : DragPosition
{
    public GameObject next;
    public float duration = 1f;

    

    public virtual void Update(){
        float distance = Vector3.Distance(transform.position, maxXTransform.position);
        if(distance < distanceX){
            next.SetActive(true);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.DOFade(0, duration);
        }
        
    }

}