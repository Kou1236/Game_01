using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScale : MonoBehaviour
{
     [Header("参数设置")]
    [Tooltip("每像素对应的缩放变化量")]
    public float scaleSpeed = 0.005f;      // 每像素缩放变化系数

    [Tooltip("X 轴最小缩放值")]
    public float minScaleX = 0f;           // 缩放下限（不可小于 0）

    private float originalScaleX;          // 记录初始的 X 轴缩放
    private float initialMouseX;           // 记录拖拽起始时的鼠标 X 坐标
    private float currentScaleX;         // 当前物体的 X

    public bool isPopup = false;           

    void Start()
    {
        // 启动时记录原始缩放
        originalScaleX = transform.localScale.x;
    }

    void Update(){
        if (isPopup){
            if(transform.localScale.x < 0.6f){
                EventHandler.CallStartClickEvent();
            }
            else{
                EventHandler.CallCloseClickEvent();
            }
        }
    }

    void OnMouseDown()
    {
        // 拖拽开始时记录鼠标初始 X 坐标
        initialMouseX = Input.mousePosition.x;
        currentScaleX = transform.localScale.x;
    }

    void OnMouseDrag()
    {
        // 当前鼠标 X 坐标
        float currentMouseX = Input.mousePosition.x;

        // 计算与初始坐标的差值
        float deltaX = currentMouseX - initialMouseX;

        // 将差值转换为缩放变化量，向右拖动缩短（负增量）
        float scaleChange = -deltaX * scaleSpeed;

        // 计算新的 X 轴缩放并限制范围
        float newScaleX = Mathf.Clamp(
            currentScaleX + scaleChange,
            minScaleX,
            originalScaleX
        );

        // 应用到物体
        Vector3 s = transform.localScale;
        s.x = newScaleX;
        transform.localScale = s;
    }
}
