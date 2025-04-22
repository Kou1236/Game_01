using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_5 : Pop
{

    protected override void OnEnable()
    {
        canvasGroup.alpha = 0;

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(canvasGroup.DOFade(1f, openDuration));

    }

    
}
