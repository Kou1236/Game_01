using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorBlock : MonoBehaviour
{
    private int originalEventMask;
    /// <summary>
    /// 全局开关：是否允许 Input.GetMouseButtonDown(0)
    /// </summary>
    public static bool AllowMouseButtonInput { get; private set; } = true;
    
    void OnEnable()
    {
        // 禁用所有 OnMouseXXX 事件
        originalEventMask = Camera.main.eventMask;
        Camera.main.eventMask = 0;
        AllowMouseButtonInput = false;
    }
    void OnDisable()
    {
        // 恢复 OnMouseXXX 事件
        Camera.main.eventMask = originalEventMask;
        AllowMouseButtonInput = true;
    }

}
