using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_16 : ClickObject
{

   
    public GameObject target;
    public GameObject moveTarget;
    public float duration = 3f;
    public GameObject book;
    public GameObject game;

    public override void Clicked()
    {   
    
        target.SetActive(true);
        Transition_20 transition = target.GetComponent<Transition_20>();
        transition.Move();
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        book.transform.tag = "Untagged";
        game.transform.tag = "Untagged";


        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(this.transform.DOMove(moveTarget.transform.position, duration).SetEase(Ease.Linear));
        mySequence.Join(spriteRenderer.DOFade(0, duration*2).SetEase(Ease.InOutSine));

    }


}
