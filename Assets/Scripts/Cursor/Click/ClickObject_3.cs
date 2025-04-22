using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_3 : ClickObject
{
    // 存储初始的 y 轴缩放值
    private float initY;
    public float duration = 1f;
    public int num = 5;
    private int i = 1;

    public int index;

    public void Start(){
        // 获取物体初始的 y 轴 scale
        initY = transform.localScale.y;
    }
    
    public override void Clicked()
    {   
        float targetY = initY * (1 - (1/(float)num) * i);
        if(i < num){
            // transform.localScale = new Vector3(transform.localScale.x, targetY, transform.localScale.z);
            transform.DOScaleY(targetY, duration).SetEase(Ease.InOutSine);
            this.gameObject.tag = "Untagged";
            i++;
        }
        if(i == num){
            this.gameObject.tag = "Untagged";
            PostManager.Instance.isFinished[index] = true;
            PostManager.Instance.postNum --;
            transform.DOScaleY(targetY, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
                this.gameObject.SetActive(false);
            });
        }
        
    }
}
