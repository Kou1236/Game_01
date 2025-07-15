using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorEnter : MonoBehaviour
{
    // 鼠标悬浮时的缩放倍率，例如 1.1 表示放大 10%
    public float scaleMultiplier = 1.1f;
    // 存储按钮的原始缩放值
    private Vector3 originalScale;

    public TagName tag;

    private Texture2D cursorTexture1;
    private Texture2D cursorTexture2;
    private Texture2D cursorTexture3;

    private bool isClick;

    public int tipIndex = 0;

    void Start()
    {
        // 记录原始缩放大小
        originalScale = transform.localScale;
        cursorTexture1 = CursorManager.Instance.cursorTexture[0];
        cursorTexture2 = CursorManager.Instance.cursorTexture[1];
        cursorTexture3 = CursorManager.Instance.cursorTexture[2];
    }

    void Update(){
        isClick = CursorManager.Instance.isClick;
    }



    // 当鼠标悬停进入时调用
    public void OnMouseOver()
    {
        if(this.gameObject.tag == tag.ToString()){
            if(!isClick){
                Cursor.SetCursor(cursorTexture2, Vector2.zero, CursorMode.ForceSoftware);
                transform.localScale = originalScale * scaleMultiplier;
                TipManager.Instance.ShowTip(tipIndex);
            }
            if(Input.GetMouseButtonDown(0)){
                Cursor.SetCursor(cursorTexture3, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
    }

    // 当鼠标离开时调用
    public void OnMouseExit()
    {
        if(!isClick){
            Cursor.SetCursor(cursorTexture1, Vector2.zero, CursorMode.ForceSoftware);
            transform.localScale = originalScale;
            TipManager.Instance.HideTip();
        }
    }
}
