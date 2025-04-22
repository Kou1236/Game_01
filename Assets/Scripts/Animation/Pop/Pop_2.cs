using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_2 : Pop
{

    public float verticalSpeed = 4f; // 垂直移动的速度
    public float verticalAmplitude = 10f; // 垂直移动的幅度

    protected override void OnEnable()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - verticalAmplitude, transform.position.z); // 初始位置为屏幕下方
        // 打开弹窗动画：从下往上弹出
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMoveY(pos.y, verticalSpeed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => openSeq.Kill());

    }

}
