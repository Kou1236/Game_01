using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCursorEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 鼠标悬浮时的缩放倍率，例如 1.1 表示放大 10%
    public float scaleMultiplier = 1.1f;
    // 存储按钮的原始缩放值
    private Vector3 originalScale;

    void Start()
    {
        // 记录原始缩放大小
        originalScale = transform.localScale;
    }

    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleMultiplier;
    }

    // 当鼠标离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
