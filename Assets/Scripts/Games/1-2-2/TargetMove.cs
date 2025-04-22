using UnityEngine;
using DG.Tweening;

public class TargetMove : MonoBehaviour
{

    [Header("目标设置")]
    // 指定目标 Transform，物体将移动至该位置
    public Transform target; 
    public GameObject popObject;

    [Header("移动参数")]
    // 移动到目标所需时间
    public float moveDuration = 5f; 


    [Header("淡出参数")]
    // 淡出动画所需时间
    public float fadeDuration = 1f;
    public float fadenum = 0f;

    public bool isDestroy = true;
    public bool isPop = false;

    void OnEnable()
    {
        MoveToTarget();
    }

    public virtual void MoveToTarget(){
        if (target == null)
        {
            Debug.LogError("未设置目标 Transform!");
            return;
        }

        // 创建 DOTween 序列
        Sequence seq = DOTween.Sequence();

        // 1. 执行物体缓慢向目标移动
        // 通过 Join 同时启动两个 Tween，且它们持续时间均为 moveDuration
        seq.Append(transform.DOMove(target.position, moveDuration).SetEase(Ease.InOutSine));
        
        // 2. 到达目标后淡出：这里假设物体上有 SpriteRenderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            seq.Join(sr.DOFade(fadenum, fadeDuration).SetEase(Ease.InOutSine));
        }
        else
        {
            // 如果没有 SpriteRenderer，可添加其它方式（例如 CanvasGroup）
            seq.AppendCallback(() => Debug.LogWarning("未找到 SpriteRenderer，淡出动画将被跳过"));
        }

        // 3. 动画全部完成后，可以选择销毁该物体或执行其他逻辑
        seq.onComplete += (() =>
        {
            if (isDestroy)
                Destroy(gameObject);
            if (isPop)
                popObject.SetActive(true);
        });
        DOVirtual.DelayedCall(moveDuration, () =>
        {
            if(isPop)
                LightEffect.Instance.canLight = true;
        });
    }
}
