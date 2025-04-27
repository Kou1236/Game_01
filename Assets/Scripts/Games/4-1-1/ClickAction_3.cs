using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction_3 : ClickAction
{
    public GameObject popObj;
    public GameObject nextScene;

    void OnEnable(){
        canClick = true;
    }

    public override void Update()
    {
        // 检测鼠标左键点击（0表示左键）
        if (Input.GetMouseButtonDown(0) && canClick && CursorBlock.AllowMouseButtonInput)
        {
            if(index < targetObject.Length){
                targetObject[index].SetActive(true);
                index++;
            }
            if(index == targetObject.Length){
                canClick = false;
                StartCoroutine(Pop());
            }
        }
    }

    IEnumerator Pop()
    {
        yield return new WaitForSeconds(5f);
        popObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        nextScene.SetActive(true);
    }
}
