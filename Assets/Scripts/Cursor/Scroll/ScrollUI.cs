using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollUI : MonoBehaviour
{
    [Tooltip("滚轮移动速度系数")]
    public float scrollSpeed = -5f;
    
    [Tooltip("最小 Y 值限制")]
    public Transform minTarget;
    private float minY;
    
    [Tooltip("最大 Y 值限制")]
    public Transform maxTarget;
    private float maxY;

    [Tooltip("Tween 持续时间（秒）")]
    public float tweenDuration = 0.5f;

    // 是否开始响应滚动事件
    public bool isStart = false;

    void OnEnable()
    {
        EventHandler.StartScrollEvent += OnStartScrollEvent;
        EventHandler.EndScrollEvent += OnEndScrollEvent;
    }

    void OnDisable()
    {
        EventHandler.StartScrollEvent -= OnStartScrollEvent;
        EventHandler.EndScrollEvent -= OnEndScrollEvent;
    }

    void OnStartScrollEvent()
    {
        isStart = true;
    }
    void OnEndScrollEvent()
    {
        isStart = false;
    }


    void Start()
    {
        // 记录限制范围
        if (minTarget != null) minY = minTarget.position.y;
        if (maxTarget != null) maxY = maxTarget.position.y;
    }

    void Update()
    {
        if (!isStart){
            transform.DOMoveY(minY, 1f);
            return;
        }

        // 读取滚轮输入
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0f)
        {
            // 计算新的 Y 坐标并限制在 [minY, maxY] 之间
            float currentY = transform.position.y;
            float targetY = Mathf.Clamp(currentY + scroll * scrollSpeed, minY, maxY);

            // 终止上一次 Tween（如果还在运动中）
            transform.DOKill();

            // 启动新的弹性缓动 Tween
            transform
                .DOMoveY(targetY, tweenDuration)
                .SetEase(Ease.OutElastic)    // 弹性回弹效果
                .SetUpdate(true);            // 即使在 Time.timeScale = 0 时也能运行
        }
    }

    
}
