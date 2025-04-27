using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour
{
    [Header("设置")]
    [Tooltip("鼠标 Y 方向位移到旋转角度的转换系数")]
    public float rotationSpeed = 0.2f;

    [Tooltip("可旋转的最小角度（以世界 Z 轴为准）")]
    public float minAngle = 0f;

    [Tooltip("可旋转的最大角度（以世界 Z 轴为准）")]
    public float maxAngle = 90f;

    [Tooltip("松开拖拽后，返回原始角度的速度（度/秒）")]
    public float returnSpeed = 180f;

    // 内部状态
    public float _initialAngle;       // 拖拽开始时记录的原始角度
    public Vector3 _prevMousePos;     // 上一帧鼠标位置
    public float _currentAngle;       // 当前累积角度
    public bool _isDragging = false;  // 是否正在拖拽

    public virtual void Start()
    {
        // 启动时，记录初始角度
        _initialAngle = transform.eulerAngles.z;
        _currentAngle = _initialAngle;
    }

    void OnEnable()
    {
        // 启用时，重置状态
        _isDragging = false;
        _currentAngle = _initialAngle;
    }

    public virtual void Update()
    {
        // 如果不在拖拽中，平滑地将物体转回初始角度
        if (!_isDragging)
        {
            float z = transform.eulerAngles.z;
            float newZ = Mathf.MoveTowardsAngle(z, _initialAngle, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);
            // 同步 _currentAngle，以免再次拖拽时产生突然跳变
            _currentAngle = newZ;
        }
        float angle = transform.eulerAngles.z;  
        if (angle >= 3f && angle <= 6f)  
        {  
            ExecuteAction();  
        }
        else{
            EventHandler.CallCloseClickEvent();
        }
    }

    void OnMouseDown()
    {
        _isDragging = true;
        // 再次记录拖拽起始时的角度（如果想始终回到最初 Start 的角度，可移除此行）
        //_initialAngle = transform.eulerAngles.z;
        _prevMousePos = Input.mousePosition;
    }

    public virtual void OnMouseDrag()
    {
        // 计算鼠标 Y 位移并转为角度增量
        Vector3 mousePos = Input.mousePosition;
        float deltaY = mousePos.y - _prevMousePos.y;
        _prevMousePos = mousePos;

        float deltaAngle = deltaY * rotationSpeed;
        _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
    }

    public virtual void OnMouseUp()
    {
        _isDragging = false;
    }

    void ExecuteAction()
    {
        EventHandler.CallStartClickEvent();
    }
}
