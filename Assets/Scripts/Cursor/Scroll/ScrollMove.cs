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

    [Tooltip("滚动缓动时间")]
    public float scrollTweenDuration = 0.2f;

    // 指定目标位置
    public GameObject target;
    private Vector3 targetPosition;
    [Tooltip("距离目标位置的阈值")]
    public float threshold = 2f;

    public bool isFinished = true;
    [Tooltip("到目标点的缓动时间")]
    public float arriveTweenDuration = 0.5f;

    void OnEnable()
    {
        EventHandler.StartScrollEvent += OnStartScrollEvent;
    }

    void OnDisable()
    {
        EventHandler.StartScrollEvent -= OnStartScrollEvent;
    }

    void OnStartScrollEvent()
    {
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

        // 检查是否已到达目标点附近
        if (Vector3.Distance(transform.position, targetPosition) <= threshold)
        {
            ExecuteFunction();
            return;
        }

        // 获取滚轮输入
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // 旧输入系统
        // float scroll = Input.mouseScrollDelta.y;        // 新输入系统

        if (Mathf.Abs(scroll) > Mathf.Epsilon)
        {
            // 计算新的 Y 位置并限制范围
            float newY = Mathf.Clamp(transform.position.y + scroll * scrollSpeed, minY, maxY);

            // 先杀掉当前可能存在的同类型 Tween，避免冲突
            transform.DOKill();

            // 使用 DOTween 做 Y 轴缓动
            transform.DOMoveY(newY, scrollTweenDuration)
                     .SetEase(Ease.OutSine);
        }
    }

    void ExecuteFunction()
    {
        isFinished = true;
        Debug.Log("物体已接近目标位置，开始执行到目标点的缓动。");

        // 杀掉之前的 Tween，确保从当前位置开始新运动
        transform.DOKill();

        // 平滑移动到目标点
        transform.DOMove(targetPosition, arriveTweenDuration)
                 .SetEase(Ease.InOutSine)
                 .OnComplete(() =>
                 {
                     Debug.Log("到达目标位置，执行后续逻辑");
                     // 在这里加入需要执行的逻辑
                 });
    }
}
