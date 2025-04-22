using UnityEngine;
using DG.Tweening;

public class DragObject_7 : DragObject
{

    public GameObject character;

    protected override void ExecuteFunction(){
        isFinished = true;
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        openSeq.Join(transform.DOScale(originalScale, openDuration).SetEase(Ease.OutBack));
        Debug.Log("finished！"); 
        this.gameObject.tag = "Untagged";      
        character.SetActive(true);
    }
    protected override void OnMouseDrag(){
        if (isDragging && !isFinished)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变
            mousePos.x = 0;  // 保持X轴不变

            transform.position = mousePos + offset;
            if (Vector3.Distance(transform.position, targetPosition) <= threshold)
            {
                // 如果物体位置在允许范围内，执行指定函数
                ObjectInclude();
            }
            else
                transform.DOScale(originalScale, openDuration).SetEase(Ease.OutBack);
        }
    }
    protected override void OnMouseDown()
    {
        if(!isFinished){
            isDragging = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变（2D中通常不改变）
            mousePos.x = 0;  // 保持X轴不变（2D中通常不改变）
            offset = transform.position - mousePos;
        }
    }

    protected override void ObjectInclude(){
        Sequence Seq = DOTween.Sequence();
        Seq.Append(transform.DOScale(0.5f*originalScale, openDuration).SetEase(Ease.OutBack));
        
    }
}
