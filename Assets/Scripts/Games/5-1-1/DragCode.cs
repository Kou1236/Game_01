using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragCode : DragPosition
{
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer changeSpriteRenderer;
    public Sprite changeSprite;
    public float duration = 0.5f;
    public GameObject closeObj;
    public GameObject popObj;

    void OnEnable(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Update(){
        float distance = Vector3.Distance(transform.position, maxXTransform.position);
        if(distance < distanceX){
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            changeSpriteRenderer.sprite = changeSprite;
            spriteRenderer.DOFade(0, duration).OnComplete(() => {
                this.gameObject.SetActive(false);
                closeObj.SetActive(false);
                popObj.SetActive(true);
            });
        }

    }
}
