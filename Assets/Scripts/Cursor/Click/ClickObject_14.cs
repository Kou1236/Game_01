using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ClickObject_14 : ClickObject
{

    public bool canClick = false;
    public int index = 0;
    public Transform neko;
    public Transform nekoPos;
    public float duration = 6f;

    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
        EventHandler.CloseClickEvent += OnCloseClickEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
        EventHandler.CloseClickEvent -= OnCloseClickEvent;
    }

    private void OnStartClicKEvent(){
        canClick = true;
    }
    private void OnCloseClickEvent(){
        canClick = false;
    }

    public override void Clicked()
    {   
        if(canClick){
            Debug.Log("Clicked");
            canClick = false;
            neko.DOMove(nekoPos.position, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
                ButtonClick.Instance.buttonStatus[index] = true;
                ButtonClick.Instance.rightButton.GetComponent<Button>().interactable = true;
            });
        }

    }

}