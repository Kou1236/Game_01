using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ClickObject_13 : ClickObject
{

    public bool canClick = false;
    public Transform target;
    public float duration = 1f;
    public int index = 0;

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
            this.transform.DOMove(target.position, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
                ButtonClick.Instance.buttonStatus[index] = true;
                ButtonClick.Instance.rightButton.GetComponent<Button>().interactable = true;
            });
        }

    }

}