using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeepShake_2 : KeepShake 
{

    // 控制旋转灵敏度，根据需求调整
    public float rotationSensitivity = 0.5f;
    // 累计旋转角度（绝对值累加）
    private float accumulatedRotationY = 0f;
    // 旋转目标角度（达到此角度后触发函数）
    public float targetRotationY = 1f;

    private Vector3 lastMousePosition;

    public float shakeDuration = 1f;

    private bool canShake = false;

    private bool canRotate = true;

    public GameObject closeTarget;
    public GameObject openTarget;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    private void Start()
    {
        spriteRenderer = closeTarget.GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    // 鼠标按下时记录初始鼠标位置
    void OnMouseDown()
    {
        if(canShake)
            lastMousePosition = Input.mousePosition;
    }

    // 鼠标拖拽过程中只更新 Y 轴旋转
    void OnMouseDrag()
    {
        if(canShake){
            canRotate = false;
            // 计算鼠标位移
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // 利用水平位移（delta.x）来控制 Y 轴旋转
            float deltaRotationY = delta.x * rotationSensitivity;

            // 只改变 Y 轴旋转（可选择 Space.Self 或 Space.World，根据物体初始旋转状态）
            transform.Rotate(0, deltaRotationY, 0, Space.World);

            // 累计旋转角度（取绝对值避免反向拖拽时抵消）
            accumulatedRotationY = Mathf.Abs(transform.rotation.y);

            Debug.Log("累计旋转角度：" + accumulatedRotationY);

            // 更新上一次鼠标位置
            lastMousePosition = Input.mousePosition;
            if (accumulatedRotationY >= targetRotationY - 0.3f)
            {
                spriteRenderer.sprite = null;
            }
            else{
                spriteRenderer.sprite = originalSprite;
            }
        }

    }

    void OnMouseUp()
    {
        if(canShake){
            canRotate = false;
            // 旋转达到180度后调用的函数
            if (accumulatedRotationY >= targetRotationY - 0.3f)
            {
                ExecuteAfterRotation();
            }
            else{
                BackToStart();
            }
        }
    }
        

    // 旋转达到180度后调用的函数
    void ExecuteAfterRotation()
    {
        canRotate = false;
        canShake = false;
        Debug.Log("旋转累计达到180度，执行函数！");
        this.transform.DORotate(new Vector3(0, 180f, 0), shakeDuration).SetEase(Ease.InOutSine).OnComplete(()=>
        {
            Debug.Log("执行完毕！");
            closeTarget.SetActive(false);
            openTarget.SetActive(true);
        });

        
    }

    void BackToStart(){
        this.transform.DORotate(new Vector3(0, 0, 0), shakeDuration).SetEase(Ease.InOutSine).OnComplete(()=>
        {
            Debug.Log("回到初始状态！");
            canRotate = true;
            StartCoroutine(RotateCortine());
        });
    }





    IEnumerator RotateCortine(){
        while(canRotate){
            this.transform.DORotate(new Vector3(0, rotationAngle, 0), duration)
                  .SetLoops(2, LoopType.Yoyo)
                  .SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void OnEnable(){
        EventHandler.StartShakeEvent += Rotate;
    } 

    void OnDisable(){
        EventHandler.StartShakeEvent -= Rotate;
    }

    protected override void Rotate(){
        Debug.Log("hello");
        canShake = true;
        StartCoroutine(RotateCortine());
    }

}
