using UnityEngine;
using DG.Tweening;  // 如果需要结合 DOTween 做额外动画，可引入

public class RotateObject : MonoBehaviour
{
    // 控制旋转灵敏度，根据需求调整
    public float rotationSensitivity = 0.5f;
    // 累计旋转角度（绝对值累加）
    public float accumulatedRotationY = 0f;
    // 旋转目标角度（达到此角度后触发函数）
    public float targetRotationY = 1f;

    public Vector3 lastMousePosition;

    public float duration = 1f;

    // 鼠标按下时记录初始鼠标位置
    void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
    }

    // 鼠标拖拽过程中只更新 Y 轴旋转
    public virtual void OnMouseDrag()
    {
        // 计算鼠标位移
        Vector3 delta = Input.mousePosition - lastMousePosition;
        // 利用水平位移（delta.x）来控制 Y 轴旋转
        float deltaRotationY = delta.x * rotationSensitivity;

        // 只改变 Y 轴旋转（可选择 Space.Self 或 Space.World，根据物体初始旋转状态）
        transform.Rotate(0, deltaRotationY, 0, Space.World);

        // 累计旋转角度（取绝对值避免反向拖拽时抵消）
        accumulatedRotationY = Mathf.Abs(transform.rotation.y);

        Debug.Log("累计旋转角度：" + accumulatedRotationY);

        // 更新上一次鼠标位置
        lastMousePosition = Input.mousePosition;

    }

    public virtual void OnMouseUp()
    {
        // 当累计旋转达到目标角度时，执行函数（仅触发一次）
        if (accumulatedRotationY >= targetRotationY - 0.2f)
        {
            ExecuteAfterRotation();
        }
        else{
            BackToStart();
        }
    }

    // 旋转达到180度后调用的函数
    public virtual void ExecuteAfterRotation()
    {
        Debug.Log("旋转累计达到180度，执行函数！");
        this.transform.DORotate(new Vector3(0, 180f, 0), duration).SetEase(Ease.InOutSine);
        
    }

    public virtual void BackToStart(){
        this.transform.DORotate(new Vector3(0, 0, 0), duration).SetEase(Ease.InOutSine);
    }
}

