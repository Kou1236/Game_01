using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RotateBook : RotateObject
{
    public List<Sprite> books;
    // public List<GameObject> character;
    public GameObject rotateObject;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer closeObject_1;
    public SpriteRenderer closeObject_2;
    public GameObject musicObject;

    public bool canRotate = false;

    private bool isSecond = false;
    public GameObject transitionObject;


    void Start(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        canRotate = false;
    }
    void OnEnable(){
        EventHandler.StartClickEvent += OnStartClickEvent;
    }
    void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClickEvent;
    }

    void OnStartClickEvent(){
        canRotate = true;
    }

    void Update(){
        if(canRotate){
            if(accumulatedRotationY > 0 && accumulatedRotationY < 0.3f){
                if(!isSecond){
                    Debug.Log("01");
                    spriteRenderer.sprite = books[0];
                }
                else{
                    Debug.Log("06");
                    spriteRenderer.sprite = books[4];
                }
            }
            else if(accumulatedRotationY >= 0.3f && accumulatedRotationY <0.5f){
                if(!isSecond){
                    Debug.Log("02");
                    spriteRenderer.sprite = books[1];
                }
                else{
                    Debug.Log("07");
                    spriteRenderer.sprite = books[5];
                }
            }
            else if(accumulatedRotationY >= 0.5f && accumulatedRotationY <0.7f){
                if(!isSecond){
                    Debug.Log("03");
                    spriteRenderer.sprite = books[2];
                }
                else{
                    Debug.Log("08");
                    spriteRenderer.sprite = books[6];
                }
            }
            else if(accumulatedRotationY >= 0.7f && accumulatedRotationY <0.8f){
                if(!isSecond){
                    Debug.Log("04");
                    spriteRenderer.sprite = books[3];
                }
                else{
                    Debug.Log("09");
                    spriteRenderer.sprite = books[7];
                }
            }
            else if(accumulatedRotationY >= 0.8f && accumulatedRotationY <0.9f){
                if(!isSecond){
                    Debug.Log("05");
                    spriteRenderer.sprite = books[4];
                    rotateObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    musicObject.SetActive(true);
                    isSecond = true;
                }
                else{
                    Debug.Log("10");
                    spriteRenderer.sprite = books[8];
                }
            }
            else if(accumulatedRotationY >= 0.9f && accumulatedRotationY <1.0f){
                canRotate = false;
            }
        }
        
    }

    public override void OnMouseDrag(){
        if(canRotate){
            closeObject_1.DOFade(0, 1f);
            closeObject_2.DOFade(0, 1f);
            // 计算鼠标位移
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // 利用水平位移（delta.x）来控制 Y 轴旋转
            float deltaRotationY = delta.x * rotationSensitivity;

            // 只改变 Y 轴旋转（可选择 Space.Self 或 Space.World，根据物体初始旋转状态）
            rotateObject.transform.Rotate(0, deltaRotationY, 0, Space.World);

            // 累计旋转角度（取绝对值避免反向拖拽时抵消）
            accumulatedRotationY = Mathf.Abs(rotateObject.transform.rotation.y);

            Debug.Log("累计旋转角度：" + accumulatedRotationY);

            // 更新上一次鼠标位置
            lastMousePosition = Input.mousePosition;
        }

        
    }

    public override void OnMouseUp(){
         // 当累计旋转达到目标角度时，执行函数（仅触发一次）
        if (accumulatedRotationY >= targetRotationY - 0.2f)
        {
            ExecuteAfterRotation();
        }
        
    }
    public override void ExecuteAfterRotation(){
        Debug.Log("旋转累计达到180度，执行函数！");
        rotateObject.transform.DORotate(new Vector3(0, 180f, 0), duration).SetEase(Ease.InOutSine);
        spriteRenderer.sprite = books[8];
        canRotate = false;
        musicObject.SetActive(false);
        transitionObject.SetActive(true);
        StartCoroutine(MoveCoroutine());
        
    }

    IEnumerator MoveCoroutine(){
        yield return new WaitForSeconds(1f);
        var trans = transitionObject.GetComponent<Transition_10>();
        trans.Move();
    }



}
