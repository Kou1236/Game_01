using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScaleY : MonoBehaviour
{
    [Tooltip("物体可缩小的最小 Y 缩放值")]
    public float minScaleY = 0.5f;

    private Camera cam;
    private float originalScaleY;
    private Vector3 lastMouseWorldPos;
    public float sensitivity = 1f;
    public GameObject next;

    public Transform targetObj;

    void Start()
    {
        cam = Camera.main;                                    // 获取主摄像机引用 :contentReference[oaicite:0]{index=0}
        originalScaleY = targetObj.transform.localScale.y;              // 缓存初始 Y 轴缩放值 :contentReference[oaicite:1]{index=1}
    }

    void OnMouseDown()
    {
        // 记录鼠标按下时的世界坐标（z 由相机与物体的深度决定）
        lastMouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);  // Input.mousePosition + 转换 :contentReference[oaicite:2]{index=2}
    }

    void OnMouseDrag()
    {
        // 获取当前鼠标世界坐标
        Vector3 currentMouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        float deltaY = currentMouseWorldPos.y - lastMouseWorldPos.y;     // 计算 Y 方向增量

        // 仅当鼠标向下拖动（deltaY < 0）时，才缩小 Y 轴缩放
        if (deltaY < 0f)
        {
            // 根据增量调整缩放，乘以一个灵敏度系数（可自定义）
            float newScaleY = targetObj.transform.localScale.y + deltaY * sensitivity;

            // 限制在 [minScaleY, originalScaleY] 范围内，防止过度缩小或放大
            newScaleY = Mathf.Clamp(newScaleY, minScaleY, originalScaleY);  // 限制函数 :contentReference[oaicite:3]{index=3}

            // 应用新的缩放值
            Vector3 scale = targetObj.transform.localScale;
            scale.y = newScaleY;
            targetObj.transform.localScale = scale;
        }

        // 更新上次鼠标位置
        lastMouseWorldPos = currentMouseWorldPos;
    }

    void Update(){
        if(targetObj.transform.localScale.y <= 0.03f){
            next.SetActive(true);
        }
    }
}
