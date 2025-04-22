using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_6 : Pop
{

    public GameObject target;

    public float speed = 1f;
    protected override void OnEnable()
    {
        // 确保初始透明度为0
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(spriteRenderer.DOFade(1f, openDuration));
        openSeq.Join(transform.DOMove(target.transform.position, speed)).SetEase(Ease.OutBack);

    }

    
}
