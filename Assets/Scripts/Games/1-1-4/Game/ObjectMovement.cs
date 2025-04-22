using UnityEngine;
using DG.Tweening;

public class ObjectMovement : MonoBehaviour
{

    public float moveDuration = 5f; // 水平移动的持续时间
    public float verticalAmplitude = 0.5f; // 垂直移动的幅度（上下最大偏移量）
    public float verticalSpeed = 1f; // 垂直移动的速度

    void Start()
    {


        // 从右到左的水平移动
        Vector3 endPosition = new Vector3(-10f, transform.position.y, transform.parent.position.z);  // 目标位置（例如屏幕左边）

        // 创建水平移动的动画
        Tween horizontalMove = transform.DOMoveX(endPosition.x, moveDuration).SetEase(Ease.Linear);

        // 创建缓慢上下移动的动画
        Tween verticalMove = transform.DOMoveY(-transform.position.y + verticalAmplitude, verticalSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        // 使用Sequence将这两个动画并行执行
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(horizontalMove);
        mySequence.Join(verticalMove); // 同时执行上下缓慢移动和水平方向的移动
    }
}
