using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_6 : ClickObject
{
    
    
    public bool canClick = false;
    private int clickCount = 0;
    public float angle = 2f;
    public float finalAngle = 40f;
    public int maxClickCount = 3;
    public GameObject dropPoint;
    public float duration = 0.2f;
    private KeepShake_1 keepShake;

    private void Start()
    {
        keepShake = GetComponent<KeepShake_1>();

    }

    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
    }

    private void OnStartClicKEvent(){
        canClick = true;
    }


    
    public override void Clicked()
    {   
        if(canClick){
            clickCount++;
            if(clickCount < maxClickCount)
                RotateObject();
            if(clickCount == maxClickCount){
                canClick = false;
                Sequence openSeq = DOTween.Sequence();
                openSeq.Append(this.transform.DORotate(new Vector3(0, 0, finalAngle), 0.8f).SetEase(Ease.InOutSine));
                openSeq.Join(this.transform.DOMove(dropPoint.transform.position, 0.8f).SetEase(Ease.InOutSine)).OnComplete(()=> {
                    keepShake.enabled = true;
                    EventHandler.CallStartDragEvent();
                    Debug.Log("Drag");
                });
                this.gameObject.tag = "Untagged";
            }

        }
        
    }

    public void RotateObject(){

        this.transform.DORotate(new Vector3(0, 0, angle), duration).SetEase(Ease.InOutSine).OnComplete(()=> {
            this.transform.DORotate(new Vector3(0, 0, 0), duration).SetEase(Ease.InOutSine);
        });
    }

    
}
