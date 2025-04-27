using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragUp : MonoBehaviour
{
    [Header("拖拽与目标设置")]    
    [Tooltip("拖拽距离到移动距离的倍率")]    
    public float speedMultiplier = 1f;

    [Tooltip("物体移动的目标世界坐标")]  
    public Transform target;  
    private Vector3 targetPosition;

    [Tooltip("最新一次拖拽的速度（单位/秒）")]    
    public float latestDragSpeed { get; private set; }

    private Vector3 dragStartWorld;      // 拖拽起始点的世界坐标
    private float dragStartTime;         // 拖拽起始时间
    private float maxY;                  // 记录物体的最高Y坐标

    private SpriteRenderer spriteRenderer;
    public GameObject popObj;
    public GameObject popObj2;
    
    void Start()
    {
        // 初始化最高Y为当前高度
        maxY = transform.position.y;
        targetPosition = target.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // 记录拖拽起始点和时间
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        dragStartWorld = Camera.main.ScreenToWorldPoint(screenPos);
        dragStartTime = Time.time;
    }

    void OnMouseUp()
    {
        // 记录拖拽结束点和时间
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 dragEndWorld = Camera.main.ScreenToWorldPoint(screenPos);
        float dragEndTime = Time.time;

        float deltaY = dragEndWorld.y - dragStartWorld.y;

        // 仅处理向下拖拽
        if (deltaY < 0f)
        {
            // 计算移动距离，应用倍率
            float moveAmount = -deltaY * speedMultiplier;

            // 计算拖拽速度
            float duration = Mathf.Max(0.0001f, dragEndTime - dragStartTime);
            latestDragSpeed = moveAmount / duration;

            // 计算新的Y坐标，并确保不低于历史最高点
            float newY = transform.position.y + moveAmount;
            float finalY = Mathf.Max(newY, maxY);

            // 如果超过目标位置，则限制到目标高度
            if (finalY >= targetPosition.y)
            {
                finalY = targetPosition.y;
            }

            // 更新物体位置
            transform.position = new Vector3(transform.position.x, finalY, transform.position.z);

            // 更新最高Y（确保不再下降）
            maxY = Mathf.Max(maxY, finalY);

            // 检查是否到达目标位置
            if (Mathf.Approximately(transform.position.y, targetPosition.y) || transform.position.y >= targetPosition.y)
            {
                OnTargetReached();
            }
        }
    }

    /// <summary>
    /// 当物体到达目标位置时调用此方法
    /// </summary>
    private void OnTargetReached()
    {
        Debug.Log("物体已到达目标位置，执行自定义逻辑！");
        this.GetComponent<Collider2D>().enabled = false;
        spriteRenderer.sortingOrder = 4;
        popObj.SetActive(true);
        popObj2.SetActive(true);

    }


}
