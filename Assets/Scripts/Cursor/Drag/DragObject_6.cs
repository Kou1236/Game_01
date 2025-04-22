using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DragObject_6 : DragObject
{
    public Transform minYObject;  // 限制y轴的最小值
    private float minY;

    public Transform target_1;
    public Transform target_2;

    public Transform other;           // 另一个物体的 Transform

    private float initialAY, initialBY;

    public GameObject moveTarget;

    public void Start(){
        minY = minYObject.position.y;
    }

    protected override void ExecuteFunction(){
        isFinished = true;
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(target_1.position, speed).SetEase(Ease.InOutSine));
        openSeq.Join(other.transform.DOMove(target_2.position, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => {
            openSeq.Kill();
            moveTarget.SetActive(true);
            StartCoroutine(MoveCoroutine());
        });
        Debug.Log("finished！");       
    }

    IEnumerator MoveCoroutine(){
        yield return new WaitForSeconds(1f);
        Debug.Log("开始移动物体！");
        var trans = moveTarget.GetComponent<Transition_9>();
        trans.Move();
    }

    protected override void OnMouseDrag(){
        if (isDragging && !isFinished)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            float rawY = worldPos.y;
            // 限制 rawY 不低于 minY
            float newY = Mathf.Clamp(rawY, minY, float.MaxValue);
            // 只改变 Y
            transform.position = new Vector3(
                transform.position.x, newY, transform.position.z
            );
            // 计算位移并让另一个物体反向同步
            float deltaY = newY - initialAY;
            other.position = new Vector3(
                other.position.x, initialBY - deltaY, other.position.z
            );


            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mousePos.z = 0;  // 保持Z轴不变
            // mousePos.x = 0;  // 保持X轴不变

            // // 限制y轴的最大值
            // mousePos.y = Mathf.Max(mousePos.y, minY);
            // transform.position = mousePos + offset;
            // letter.transform.position = mousePos + letterOffset;
        }
    }
    protected override void OnMouseDown()
    {
        if(!isFinished){
            isDragging = true;
            initialAY = transform.position.y;          
            initialBY = other.position.y;              
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = worldPos - transform.position; 
            // isDragging = true;
            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mousePos.z = 0;  // 保持Z轴不变（2D中通常不改变）
            // mousePos.x = 0;  // 保持X轴不变（2D中通常不改变）
            // offset = transform.position - mousePos;
            // letterOffset = letter.transform.position - mousePos;
        }
    }
}
