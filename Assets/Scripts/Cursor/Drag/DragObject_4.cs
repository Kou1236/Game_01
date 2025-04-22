using UnityEngine;
using DG.Tweening;

public class DragObject_4 : DragObject
{
    public float maxY = 10f;  // 限制y轴的最大值

    public GameObject button;  // 目标物体

    public GameObject music;  // 目标物体

    protected override void ExecuteFunction(){
        isFinished = true;
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => {
            openSeq.Kill();
            music.SetActive(true);
        });
        DOVirtual.DelayedCall(3f, () => {
            button.SetActive(true);
        });
        Debug.Log("finished！");       
    }
    protected override void OnMouseDrag(){
        if (isDragging && !isFinished)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变
            mousePos.x = 0;  // 保持X轴不变

            // 限制 y 值只能在初始值与 maxY 之间（不能往下拉，只能向上）
            // mousePos.y = Mathf.Clamp(mousePos.y, -3.5f, maxY);
            transform.position = mousePos + offset;
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
}
