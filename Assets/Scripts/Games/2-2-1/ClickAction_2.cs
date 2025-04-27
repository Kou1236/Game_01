using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction_2 : ClickAction
{
    public GameObject button;


    public void OnEnable(){
        canClick = true;
    }
    public override void Update(){
        // 检测鼠标左键点击（0表示左键）
        if (Input.GetMouseButtonDown(0) && canClick && CursorBlock.AllowMouseButtonInput)
        {
            
            if(index < targetObject.Length){
                targetObject[index].SetActive(true);
                index++;
            }
            if(index == targetObject.Length){
                canClick = false;
                StartCoroutine(PopButton());
            }
        }
    }

    IEnumerator PopButton()
    {
        yield return new WaitForSeconds(1f);
        button.SetActive(true);
    }
}
