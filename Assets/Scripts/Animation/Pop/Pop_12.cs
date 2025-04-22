using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_12 : Pop
{
    public GameObject target;
    private Vector3 originalScale;
    public float scale = 1.4f;
    public float scaleDuration = 0.5f;
    
    private void Start()
    {
        originalScale = transform.localScale;

    }
    protected override void OnEnable()
    {

        PopObject();
        
    }

    public void PopObject(){
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(spriteRenderer.DOFade(1f, openDuration)).SetEase(Ease.OutBack);
        openSeq.Join(transform.DOScale(scale, scaleDuration).SetEase(Ease.OutBack));
        DOVirtual.DelayedCall(openDuration, () =>{
            openSeq.Append(transform.DOScale(originalScale, closeDuration).SetEase(Ease.OutBack));
            openSeq.Join(transform.DOMove(target.transform.position, closeDuration).SetEase(Ease.OutBack));
        });
        DOVirtual.DelayedCall(openDuration + closeDuration, () => {
            spriteRenderer.sortingOrder = 0;
            EventHandler.CallStartClickEvent();

        });
    }


    
}
