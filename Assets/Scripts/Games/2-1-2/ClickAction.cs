using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : Singleton<ClickAction>
{
    public GameObject[] targetObject;
    public bool canClick = false;
    public int index = 0;
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "Start";

    // Update 在每一帧调用一次
    public virtual void Update()
    {
        // 检测鼠标左键点击（0表示左键）
        if (Input.GetMouseButtonDown(0) && canClick && CursorBlock.AllowMouseButtonInput)
        {
            if(index == 0){
                animator.SetBool(boolParameterName, true);
            }
            
            if(index < targetObject.Length){
                targetObject[index].SetActive(true);
                index++;
            }
            if(index == targetObject.Length){
                canClick = false;
            }
        }
    }
}
