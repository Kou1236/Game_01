using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop : MonoBehaviour
{
    public CanvasGroup canvasGroup;    // 绑定 CanvasGroup，用于淡入淡出
    public SpriteRenderer spriteRenderer;    // 绑定 SpriteRenderer，用于动画效果

    // 弹窗打开动画时长
    public float openDuration = 0.5f;
    public float closeDuration = 0.3f;

    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {

    }

    public void CloseCanvas(){
        Debug.Log("Close");
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(canvasGroup.DOFade(0f, closeDuration)).OnComplete(() => {
            this.gameObject.SetActive(false);
        });
    }
    public void CloseSprite(){
        Debug.Log("Close");
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(spriteRenderer.DOFade(0f, closeDuration)).OnComplete(() => {
            this.gameObject.SetActive(false);
        });
    }


}
