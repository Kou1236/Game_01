using UnityEngine;
using DG.Tweening;

public class DragObject_2 : DragObject
{
    public GameObject openTarget;
    protected override void ExecuteFunction(){
        isFinished = true;
        transform.DOScale(1f, openDuration).SetEase(Ease.OutBack);
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => openSeq.Kill());
        Debug.Log("finished！"); 
        EventHandler.CallCloseEvent();
        openTarget.SetActive(true);

        
    }
}
