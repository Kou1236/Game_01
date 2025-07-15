using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TipManager : Singleton<TipManager>
{
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "State";

    public CanvasGroup canvasGroup;


    public void ShowTip(int index){
        if (index == -1)
            canvasGroup.DOFade(0f, 0.2f);
        else{
            canvasGroup.DOFade(1f, 0.2f);
            animator.SetInteger(boolParameterName, index);
            Debug.Log("ShowTip: " + index);
        }
    }

    public void HideTip(){
        canvasGroup.DOFade(0f, 0.2f);
        animator.SetInteger(boolParameterName, 4);
    }
}
