using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStop : MonoBehaviour
{
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "Start";

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
            animator.SetBool(boolParameterName, false);
        }
    }
    
}
