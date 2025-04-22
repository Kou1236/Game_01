using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatePhone : RotateObject
{
    public List<Sprite> phones;
    public List<GameObject> flowers;
    public GameObject rotateObject;
    public SpriteRenderer spriteRenderer;

    public bool canRotate = true;


    void Start(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        canRotate = true;
    }

    void Update(){
        if(canRotate){
            if(accumulatedRotationY > 0 && accumulatedRotationY < 0.3f){
                Debug.Log("01");
                spriteRenderer.sprite = phones[0];
                for(int i = 0; i < flowers.Count; i++){
                    flowers[i].SetActive(false);
                }
            }
            else if(accumulatedRotationY >= 0.3f && accumulatedRotationY <0.5f){
                Debug.Log("02");
                spriteRenderer.sprite = phones[1];
                flowers[0].SetActive(true);
                flowers[1].SetActive(false);
                flowers[2].SetActive(false);
                flowers[3].SetActive(false);
            }
            else if(accumulatedRotationY >= 0.5f && accumulatedRotationY <0.7f){
                Debug.Log("03");
                spriteRenderer.sprite = phones[2];
                flowers[0].SetActive(true);
                flowers[1].SetActive(true);
                flowers[2].SetActive(false);
                flowers[3].SetActive(false);
            }
            else if(accumulatedRotationY >= 0.7f && accumulatedRotationY <0.8f){
                Debug.Log("04");
                spriteRenderer.sprite = phones[3];
            }
            else if(accumulatedRotationY >= 0.8f && accumulatedRotationY <0.9f){
                Debug.Log("05");
                spriteRenderer.sprite = phones[4];
                flowers[0].SetActive(true);
                flowers[1].SetActive(true);
                flowers[2].SetActive(true);
                flowers[3].SetActive(false);
            }
            else if(accumulatedRotationY >= 0.9f && accumulatedRotationY <1.0f){
                Debug.Log("06");
                spriteRenderer.sprite = phones[5];
                flowers[0].SetActive(true);
                flowers[1].SetActive(true);
                flowers[2].SetActive(true);
                flowers[3].SetActive(true);
                canRotate = false;
            }
        }
        
    }

    public override void OnMouseDrag(){
        // 计算鼠标位移
        Vector3 delta = Input.mousePosition - lastMousePosition;
        // 利用水平位移（delta.x）来控制 Y 轴旋转
        float deltaRotationX = delta.y * rotationSensitivity;

        // 只改变 Y 轴旋转（可选择 Space.Self 或 Space.World，根据物体初始旋转状态）
        rotateObject.transform.Rotate(deltaRotationX, 0, 0, Space.World);

        // 累计旋转角度（取绝对值避免反向拖拽时抵消）
        accumulatedRotationY = Mathf.Abs(rotateObject.transform.rotation.x);

        Debug.Log("累计旋转角度：" + accumulatedRotationY);

        // 更新上一次鼠标位置
        lastMousePosition = Input.mousePosition;

        
    }

    public override void OnMouseUp(){
         // 当累计旋转达到目标角度时，执行函数（仅触发一次）
        if (accumulatedRotationY >= targetRotationY - 0.12f)
        {
            ExecuteAfterRotation();
        }
        
    }
    public override void ExecuteAfterRotation(){
        Debug.Log("旋转累计达到180度，执行函数！");
        rotateObject.transform.DORotate(new Vector3(180f, 0, 0), duration).SetEase(Ease.InOutSine);
        spriteRenderer.sprite = phones[6];
        canRotate = false;
        StartCoroutine(RotatePhoneCoroutine());
    }

    IEnumerator RotatePhoneCoroutine(){
        yield return new WaitForSeconds(1f);
        Teleport teleport = this.gameObject.GetComponent<Teleport>();
        teleport.TeleportToScene();
    }


}
