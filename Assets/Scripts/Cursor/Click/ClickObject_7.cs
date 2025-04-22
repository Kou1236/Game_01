using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_7 : ClickObject
{
    
    public GameObject firstPath;
    public GameObject finalPath;
    public float duration = 1f;
    private Tweener tweener;
    private SpriteRenderer spriteRenderer;
    public float openDuration = 0.5f;
    public bool canClick = false;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   



    
    public override void Clicked()
    {   
        if(canClick){
            Debug.Log("Clicked");
            tweener = this.transform.DOMove(firstPath.transform.position, duration).SetEase(Ease.InOutSine).OnComplete(() => {
                this.transform.DOMove(finalPath.transform.position, duration).SetEase(Ease.InOutSine);
            });
            DOVirtual.DelayedCall(2*duration, () => {
                tweener.Kill();
                ClickStars.Instance.AddStar();
                ClickStars.Instance.PopColor();
                ClickStars.Instance.FinishGame();
                spriteRenderer.DOFade(0f, openDuration);
            });
            DOVirtual.DelayedCall(2*duration + openDuration, () => {
                Destroy(this.gameObject);
            });
        }
        
    }
}
