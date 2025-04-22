using DG.Tweening;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float amplitude = 2f;  // 正弦波的振幅
    private float frequency = 1f;  // 正弦波的频率
    private float randomYOffset;  // 正弦波的随机偏移量
    private float duration = 5f;  // 物体移动的总时长

    void Start()
    {
        // 初始位置设置，启动物体的动画
        MoveObject();
    }

    // 设置随机Y偏移
    public void SetMovement(float offset)
    {
        randomYOffset = offset;
    }

    void MoveObject()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(-10, startPos.y, startPos.z);  // 目标位置，沿X轴移动

        // 使用 DOTween 来平滑的移动物体，同时沿着正弦波动
        Sequence sequence = DOTween.Sequence();

        // X轴线性移动
        sequence.Append(transform.DOMoveX(endPos.x, duration).SetEase(Ease.Linear));

        // Y轴的正弦轨迹
        sequence.Join(DOTween.To(() => transform.position.y, y => transform.position = new Vector3(transform.position.x, y, transform.position.z),
            Mathf.Sin(Time.time * frequency + randomYOffset) * amplitude, duration).SetEase(Ease.Linear));

        // 当动画完成时销毁物体
        sequence.OnKill(() => Destroy(gameObject));
    }

}
