using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_5 : ClickObject
{
    
    private SpriteRenderer spriteRenderer;
    public float openDuration = 0.1f;
    private Tweener tweener;
   

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

    }


    
    public override void Clicked()
    {   
        Debug.Log("Clicked");
        tweener = spriteRenderer.DOFade(1f, openDuration).OnComplete(() => {
            tweener.Kill();
        });
        ClickStars.Instance.AddStar();
        ClickStars.Instance.FinishGame();
        this.gameObject.tag = "Untagged";
        
    }
}
