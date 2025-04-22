using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [Header("绑定组件")]
    public Button closeButton;         // 绑定弹窗的关闭按钮
    public CanvasGroup canvasGroup;    // 绑定 CanvasGroup，用于淡入淡出

    // 弹窗打开动画时长
    public float openDuration = 0.5f;
    // 弹窗关闭动画时长
    public float closeDuration = 0.1f;

    void Start()
    {
        // 确保弹窗初始状态为缩放 0 和透明
        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0;

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOScale(0.7f, openDuration).SetEase(Ease.OutBack));
        openSeq.Join(canvasGroup.DOFade(1f, openDuration));

        // 为关闭按钮绑定点击事件
        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePopup);
        else
            Debug.LogWarning("请在 Inspector 中绑定关闭按钮！");
    }

    // 关闭弹窗动画，并在动画结束后更新进度条，再销毁弹窗
    public void ClosePopup()
    {
        Sequence closeSeq = DOTween.Sequence();
        closeSeq.Append(canvasGroup.DOFade(0f, closeDuration));
        closeSeq.Join(transform.DOScale(0f, closeDuration));
        closeSeq.OnComplete(() =>
        {
            // 每关闭一个弹窗，调用进度管理器增加进度
            if (ProgressManager.Instance != null)
                ProgressManager.Instance.AddProgress();
            Destroy(gameObject);
        });
    }
}
