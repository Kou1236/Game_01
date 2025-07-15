using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCursorEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 鼠标悬浮时的缩放倍率，例如 1.1 表示放大 10%
    public float scaleMultiplier = 1.1f;
    // 存储按钮的原始缩放值
    private Vector3 originalScale;
    public bool isMusic = false;
    public int audioIndex = 0;

    private Texture2D cursorTexture1;
    private Texture2D cursorTexture2;
    private Texture2D cursorTexture3;

    private Button button;

    private bool isClick = false;

    public int tipIndex = 0;

    void Start()
    {
        // 记录原始缩放大小
        originalScale = transform.localScale;
        cursorTexture1 = CursorManager.Instance.cursorTexture[0];
        cursorTexture2 = CursorManager.Instance.cursorTexture[1];
        cursorTexture3 = CursorManager.Instance.cursorTexture[2];
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }


    public void OnButtonClick(){
        Cursor.SetCursor(cursorTexture1, Vector2.zero, CursorMode.ForceSoftware);
        TipManager.Instance.HideTip();
    }


    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isClick){
            transform.localScale = originalScale * scaleMultiplier;
            if (isMusic){
                Debug.Log("OnPointerEnter");
                AudioManager.Instance.PlayNext(audioIndex);
            }
            Cursor.SetCursor(cursorTexture2, Vector2.zero, CursorMode.ForceSoftware);
            TipManager.Instance.ShowTip(tipIndex);
        }
    }

    // 当鼠标离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isClick){
            transform.localScale = originalScale;
            if(isMusic){
                AudioManager.Instance.Stop();
            }
            Cursor.SetCursor(cursorTexture1, Vector2.zero, CursorMode.ForceSoftware);
            TipManager.Instance.HideTip();
        }
    }

}
