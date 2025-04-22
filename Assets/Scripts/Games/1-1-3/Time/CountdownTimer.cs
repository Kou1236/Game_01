using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountdownTimer : Singleton<CountdownTimer>
{
    [Header("UI 设置")]
    public Text countdownText; // 用于显示倒计时的 UI 文本

    // 初始倒计时数值，从 10 开始
    public float countdownValue = 10f;
    private Sequence countdownSeq;
    public RectTransform rt;

    void OnEnable()
    {
        // 创建 DOTween 序列
        countdownSeq = DOTween.Sequence();
        
        // 阶段一：从 10 到 1，持续 9 秒，线性变化
        countdownSeq.Append(
            DOTween.To(() => countdownValue, x => countdownValue = x, 1f, 9f)
                   .SetEase(Ease.Linear)
        );
        countdownSeq.Append(
            DOTween.To(() => countdownValue, x => countdownValue = x, 0.9f, 1f)
                   .SetEase(Ease.Linear)
        );

        // 阶段二：从 1 到 0.1，持续 9 秒，线性变化
        countdownSeq.Append(
            DOTween.To(() => countdownValue, x => countdownValue = x, 0.1f, 9f)
                   .SetEase(Ease.Linear)
        );


        // 阶段三：从 1 到 0.01，持续 1000 秒，采用缓出指数（Ease.OutExpo）缓动
        // 这样数值会越来越慢地减小，永远不会真正到 0
        countdownSeq.Append(
            DOTween.To(() => countdownValue, x => countdownValue = x, 0.01f, 100f)
                   .SetEase(Ease.OutExpo)
        );

        countdownSeq.Insert(5f,
            rt.DORotate(new Vector3(0, 0, 10f), 0.2f)
              .SetLoops(26, LoopType.Yoyo) // 4 秒内完成 8 次往返（每次 0.5 秒）
              .SetEase(Ease.InOutSine)
        );
        
        // 每一帧更新倒计时显示
        countdownSeq.OnUpdate(UpdateCountdownText);

        
    }

    /// <summary>
    /// 根据当前倒计时数值更新 UI 文本：
    /// - 大于等于 1 时，显示整数（向上取整）；
    /// - 小于 1 时，显示两位小数。
    /// </summary>
    void UpdateCountdownText()
    {
        if (countdownValue >= 0.9f)
        {
            countdownText.text = Mathf.CeilToInt(countdownValue).ToString();
        }
        else if(countdownValue >= 0.1f && countdownValue < 1f)
        {
            countdownText.text = countdownValue.ToString("F2");
        }
        else
        {
            countdownText.text = countdownValue.ToString("F3");
        }
        
        // 倒计时结束后，停止动画并清除文本
        if (countdownValue <= 0.011f)
        {
            countdownSeq.Kill();
            this.gameObject.SetActive(false);
        }

    }


}
