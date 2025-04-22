using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_8 : ClickObject
{
    
    public float duration = 1f;
    private Tweener tweener;
    private SpriteRenderer fadeSpriteRenderer;
    public SpriteRenderer popSpriteRenderer;

    public float openDuration = 0.5f;
    public bool canClick = false;

    private Animator animator;
    public string boolParameterName = "isPop";

    private int index;
    private int clickCount;

    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
    }

    private void OnStartClicKEvent(){
        canClick = true;
    }

    private void Start()
    {
        fadeSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        clickCount = ClickStars.Instance.stars;
    }
   



    
    public override void Clicked()
    {   
        if(canClick){
            Debug.Log("Clicked");
            ClickStars.Instance.AddStar();
            index = ClickStars.Instance.index;
            ClickStars.Instance.FinishGame();
            animator.SetBool(boolParameterName, true);
            Sequence openSeq = DOTween.Sequence();
            openSeq.Append(fadeSpriteRenderer.DOFade(1-1/(float)clickCount*index, duration));
            openSeq.Join(popSpriteRenderer.DOFade(1/(float)clickCount*index, duration));
            if(index == clickCount){
                this.gameObject.SetActive(false);
            }

        }
        
    }
}
