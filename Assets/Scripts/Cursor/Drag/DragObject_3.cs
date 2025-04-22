using UnityEngine;
using DG.Tweening;

public class DragObject_3 : DragObject
{
    public float maxY = 1f;
    public GameObject cd;
    public GameObject cd2;

    public float rotateSpeed = 10f;
    public float rotateToSpeed = 1f;
    public float angle = -30f;

    private Tweener cdRotationTween;

    public GameObject background;


    public GameObject train;
    public GameObject trainFront;

    public float frontScale = 0.23f;

    public float trainScale = 0.09f;

    public float trainSpeed = 2f;

    public GameObject black;

    public float blackScale = 0.8f;

    public float blackSpeed = 4f;

    public GameObject text;

    public float musicTime = 5f;

    public GameObject bird;

    

    protected override void ExecuteFunction(){
        isFinished = true;
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => {
            openSeq.Kill();
            cdRotate();
            bird.SetActive(true);
        });
        Debug.Log("finished！"); 
        
    }

    protected override void OnMouseDrag(){
        if (isDragging && !isFinished)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;  // 保持Z轴不变
            mousePos.x = 0;  // 保持X轴不变

            // 限制y轴的最大值
            mousePos.y = Mathf.Min(mousePos.y, maxY);

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

    public void cdRotate(){
        cd2.transform.DORotate(new Vector3(0, 0, angle), rotateToSpeed, RotateMode.Fast)
                 .SetEase(Ease.OutQuad); // 缓出效果

        cdRotationTween = cd.transform.DORotate(new Vector3(0, 0, -360), rotateSpeed, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) // 匀速旋转
                 .SetLoops(-1, LoopType.Incremental); // 无限循环，递增角度

        DOVirtual.DelayedCall(musicTime, () =>
        {
            // 停止 cd 的无限旋转
            if (cdRotationTween.IsActive())
            {
                cdRotationTween.Kill();
            }
            // 调用旋转结束后需要执行的函数
            AfterRotationComplete();
        });
    }

    public void AfterRotationComplete(){
        InfiniteScrolling.Instance.Stop();
        var position = background.transform.position;
        Debug.Log("position: " + position);
        Debug.Log("旋转结束！");

        Destroy(bird);
        StartTrain();


        DOVirtual.DelayedCall(2f, () => {
            CameraController.Instance.MoveY(10f, 4f);
        });
        DOVirtual.DelayedCall(4f, () => {
            text.SetActive(true);
        });
        Teleport teleport = this.GetComponent<Teleport>();
        DOVirtual.DelayedCall(8f, () => {
            // 确保目标组件存在，然后调用其方法
            if (teleport != null)
            {
                Debug.Log("teleport to scene");
                teleport.TeleportToScene();
            }
            else
            {
                Debug.LogWarning("未在目标对象上找到 Teleport 组件。");
            }
        });
    }


    public void StartTrain(){
        train.transform.DOScale(trainScale, trainSpeed).SetEase(Ease.InOutSine);
        trainFront.transform.DOScale(frontScale, trainSpeed).SetEase(Ease.InOutSine);
        black.transform.DOScale(blackScale, blackSpeed).SetEase(Ease.InOutSine).OnComplete(() => {
            black.SetActive(false);
        });
        this.transform.DOMoveY(targetPosition.y - 8f, speed*2).SetEase(Ease.InOutSine);
    }

    

}
