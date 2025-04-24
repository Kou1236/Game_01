using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotate : MonoBehaviour
{
    [Tooltip("参考物体：迷宫父对象")]
    public Transform referenceObject;
    [Tooltip("小球的 Rigidbody2D 引用")]
    public Rigidbody2D ballRb;
    [Tooltip("模拟的重力加速度大小 (约 9.81)")]
    public float gravityScale = 9.81f;

    void FixedUpdate()
    {
        if (referenceObject == null || ballRb == null) return;

        // 1. 读取迷宫的 Z 轴旋转角度（度）
        float zAngle = referenceObject.eulerAngles.z; 

        // 2. 转换为弧度
        float rad = zAngle * Mathf.Deg2Rad;

        // 3. 计算斜面方向向量 (sinθ, -cosθ)
        Vector2 slideDir = new Vector2(
            Mathf.Sin(rad),
           -Mathf.Cos(rad)
        );

        // 4. 施加力：F = m * a, a 取 gravityScale
        float forceMag = -gravityScale * ballRb.mass;
        ballRb.AddForce(slideDir * forceMag);
    }
}
