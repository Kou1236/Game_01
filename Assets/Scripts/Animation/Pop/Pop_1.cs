using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_1 : Pop
{

    protected override void OnEnable()
    {
        canvasGroup.alpha = 0;

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOScale(1f, openDuration).SetEase(Ease.OutBack));
        openSeq.Join(canvasGroup.DOFade(1f, openDuration));
        openSeq.OnComplete(() => openSeq.Kill());
        EventHandler.CloseEvent += Close;

    }
    protected override void OnDisable()
    {
        EventHandler.CloseEvent -= Close;
    }

    public void Close() {
        Sequence closeSeq = DOTween.Sequence();
        closeSeq.Append(canvasGroup.DOFade(0f, closeDuration));
        closeSeq.OnComplete(() => 
        {
            Destroy(gameObject);
            closeSeq.Kill();
        });
    }
}
