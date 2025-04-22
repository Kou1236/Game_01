using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_6 : Transition
{
    // public Animator animator;
    // // 在 Animator Controller 中设置的 Bool 参数名称
    // public string boolParameterName = "Start";

    protected override void OnSceneTransitionEvent(){
        StartScene();
        this.gameObject.SetActive(false);
    }

    void StartScene(){
        // animator.SetBool(boolParameterName, true);
        ClickAction.Instance.canClick = true;
    }

    
}
