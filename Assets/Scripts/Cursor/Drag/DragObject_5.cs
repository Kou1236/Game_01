using UnityEngine;
using DG.Tweening;

public class DragObject_5 : DragObject
{

    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 0.5f;

    public void Start(){
        isFinished = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnEnable(){
        EventHandler.StartDragEvent += OnStartDragEvent;
    }
    public void OnDisable(){
        EventHandler.StartDragEvent -= OnStartDragEvent;
    }

    
    protected override void ExecuteFunction(){
        isFinished = true;
        transform.DOScale(originalScale, openDuration).SetEase(Ease.OutBack);
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => openSeq.Kill());
        Debug.Log("finished！"); 
        EventHandler.CallCloseEvent();
        FinishDrag();
        
    }
    protected override void ObjectInclude(){
        Sequence Seq = DOTween.Sequence();
        Seq.Append(transform.DOScale(0.5f*originalScale, openDuration).SetEase(Ease.OutBack));
        
    }

    public void OnStartDragEvent(){
        isFinished = false;
    }

    public void FinishDrag(){
        GamePlayManager.Instance.ChangeCharacter();
        spriteRenderer.DOFade(0f, fadeDuration);
        DOVirtual.DelayedCall(fadeDuration, () => {
            GamePlayManager.Instance.EndGame();
            GamePlayManager.Instance.index++;
            GamePlayManager.Instance.StartGame();
        });

    }
}
