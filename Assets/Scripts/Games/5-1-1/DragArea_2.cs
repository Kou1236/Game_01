using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragArea_2 : DragArea
{
    
    private SpriteRenderer spriteRenderer;
    public Vector3 targetPosition;
    public GameObject target;
    public float threshold = 0.5f;
    public bool canFinish = false;
    public GameObject closeObject;
    public GameObject openObject;


    public override void Start(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        targetPosition = target.transform.position;
        base.Start();
    }

    public override void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = Input.mousePosition;
            curScreenPoint.z = 10f; // 适当的z值
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            transform.localScale = originDragSize * dragScale;
            if (isInTargetArea)
            {
                Debug.Log("拖拽成功，执行函数！");
                ExecuteFunction();
            }
        }
    }

    public override void OnMouseUp()
    {
        isDragging = false;
        transform.localScale = originDragSize;
        if(canFinish)
            FinishDrag();
    }

    public override void ExecuteFunction()
    {
        // 你要执行的逻辑
        Debug.Log("函数被执行了！");
        spriteRenderer.sortingOrder = 4;
        canFinish = true;
        Destroy(closeObject);
    }


    void FinishDrag(){
        transform.DOScale(originDragSize, 0.5f).SetEase(Ease.OutBack);
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, 2f).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => {
            openSeq.Kill();
        });
        DOVirtual.DelayedCall(3f, () => {
            openObject.SetActive(true);
        });
        Debug.Log("finished！"); 
    }

}
