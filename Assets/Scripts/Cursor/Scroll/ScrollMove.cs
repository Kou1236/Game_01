using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollMove : MonoBehaviour
{
    [Tooltip("滚轮移动速度系数")]
    public float scrollSpeed = -5f;
    
    [Tooltip("最小 Y 值限制")]
    public float minY = -5f;
    
    [Tooltip("最大 Y 值限制")]
    public float maxY = 30f;

    // 指定目标位置
    public GameObject target;
    private Vector3 targetPosition;
    [Tooltip("距离目标位置的阈值")]
    public float threshold = 2f;

    public bool isFinished = true;

    public float speed = 0.5f;

    void OnEnable(){
        EventHandler.StartScrollEvent += OnStartScrollEvent;
    }
    void OnDisable(){
        EventHandler.StartScrollEvent -= OnStartScrollEvent;
    }

    void OnStartScrollEvent(){
        isFinished = false;
    }
        

    void Start()
    {
        // 记录目标位置
        targetPosition = target.transform.position;
    }

    void Update()
    {
        if (isFinished) return;
        // 获取滚轮输入
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // 旧输入系统
        // float scroll = Input.mouseScrollDelta.y;        // 新输入系统
        if (Vector3.Distance(transform.position, targetPosition) <= threshold)
        {
            // 如果物体位置在允许范围内，执行指定函数
            ExecuteFunction();
        }
        
        if (scroll != 0f)
        {
            // 计算新的 Y 位置并限制范围
            float newY = Mathf.Clamp(transform.position.y + scroll * scrollSpeed, minY, maxY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    void ExecuteFunction(){
        isFinished = true;
        Debug.Log("物体已拖拽到目标位置，执行函数！");
        // 在这里加入需要执行的逻辑
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(targetPosition, speed).SetEase(Ease.InOutSine));
        Debug.Log("finished！");
    }
}
