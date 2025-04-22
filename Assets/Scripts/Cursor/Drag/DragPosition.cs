using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPosition : MonoBehaviour
{
    [Header("最大 X 位置")]
    public Transform maxXTransform;  // 最大 X 位置的物体
    public float maxPosX;

    private float originalPosX;        // 启动时物体的原始 X
    private float initialMouseX;       // 鼠标按下时的屏幕 X
    private float currentPosX;         // 当前物体的 X

    public float moveSpeed = 0.005f;      // 每像素缩放变化系数

    public bool isPopup = false;         // 是否弹出
    public float distanceX = 0.5f;        // 弹出距离
    public bool isLeft = false;            // 是否左侧弹出
    public bool isFood = false;            // 是否食物弹出
    public GameObject popup;             // 弹出物体
    public GameObject food;              // 食物

    void Start()
    {
        // 记录最初的世界坐标 X
        originalPosX = transform.position.x;
        maxPosX = maxXTransform.position.x;
    }

    void Update(){
        if(isPopup){
            float distance = Vector3.Distance(transform.position, maxXTransform.position);
            if(distance < distanceX){
                EventHandler.CallStartClickEvent();
                ClickObject_13 clickObject = popup.GetComponent<ClickObject_13>();
                clickObject.Clicked();
                isPopup = false;

            }
            else{
                EventHandler.CallCloseClickEvent();
            }
        }
        else if(isFood){
            float distance = Vector3.Distance(transform.position, maxXTransform.position);
            if(distance < distanceX){
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                food.SetActive(true);
            }
        }



    }

    void OnMouseDown()
    {
        // 当鼠标按下时记录屏幕 X
        initialMouseX = Input.mousePosition.x;
        currentPosX = transform.position.x;
    }

    void OnMouseDrag()
    {
        // 当前鼠标屏幕 X
        float currentMouseX = Input.mousePosition.x;

        // 计算与初始坐标的差值
        float deltaX = currentMouseX - initialMouseX;

        // 将差值转换为缩放变化量，向右拖动缩短（负增量）
        float scaleChange = -deltaX * moveSpeed;

        // 限制在 originalPosX 与 maxPosX 之间
        float clampedX = Mathf.Clamp(currentPosX + scaleChange, originalPosX, maxPosX);
        if(isLeft){
            clampedX = Mathf.Clamp(currentPosX + scaleChange, maxPosX, originalPosX);
        }

        // 应用到物体位置，只修改 X
        Vector3 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
    }
}
