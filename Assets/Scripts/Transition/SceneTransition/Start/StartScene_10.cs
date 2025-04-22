using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_10 : StartScene
{
    
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "Start";

    public GameObject button;
    
    protected override void StartSceneAction(){
        animator.SetBool(boolParameterName, true);
        button.SetActive(true);
    }



    

}
