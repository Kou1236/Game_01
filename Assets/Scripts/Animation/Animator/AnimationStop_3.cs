using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationStop_3 : MonoBehaviour
{
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "State";
    public CanvasGroup canvasGroup;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 动画事件调用该方法：动画播放完成后执行此回调
    public virtual void OnAnimationFinished()
    {
        Debug.Log("动画播放完成");
        if(animator != null)
        {
            animator.SetInteger(boolParameterName, 4);
            canvasGroup.DOFade(0, 0.2f);
        }
    }
    
}
