using UnityEngine;
using DG.Tweening;

public class DragObject : MonoBehaviour
{
    // 指定目标位置
    public GameObject target;
    // 检测目标位置的误差范围
    public float threshold = 0.5f;

    public Vector3 offset;
    public bool isDragging = false;

    public Vector3 targetPosition;
    
    public bool isFinished = false;

    public float speed = 0.5f;

    public float openDuration = 1f;

    public Vector3 originalScale;

    


    void Awake() {
        targetPosition = target.transform.position;
        originalScale = transform.localScale;
    }

    // 当鼠标按下时，记录偏移量
    protected virtual void OnMouseDown()
    {
        if(!isFinished){
            isDragging = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变（2D中通常不改变）
            offset = transform.position - mousePos;
        }
    }

    // 拖拽过程中，根据鼠标位置更新物体位置
    protected virtual void OnMouseDrag()
    {
        if (isDragging && !isFinished)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变（2D中通常不改变）
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

    // 鼠标释放时，停止拖拽并检测是否到达目标位置
    void OnMouseUp()
    {
        if(isDragging && !isFinished){
            isDragging = false;
            if (Vector3.Distance(transform.position, targetPosition) <= threshold)
            {
                // 如果物体位置在允许范围内，执行指定函数
                ExecuteFunction();
            }
        }
    }

    // 要执行的函数方法
    protected virtual void ExecuteFunction()
    {
        
    }

    protected virtual void ObjectInclude(){
        Sequence Seq = DOTween.Sequence();
        Seq.Append(transform.DOScale(1.2f*originalScale, openDuration).SetEase(Ease.OutBack));
        
    }
}
