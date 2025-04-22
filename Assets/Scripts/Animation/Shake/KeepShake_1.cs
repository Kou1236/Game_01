using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeepShake_1 : KeepShake 
{

    private Tweener ShakeTweener;
    private bool isFinished = false;

    public float shakeAngle = 0f;

    IEnumerator RotateCortine(){
        while(!isFinished){
            ShakeTweener = this.transform.DORotate(new Vector3(0, shakeAngle, rotationAngle), duration)
                  .SetLoops(2, LoopType.Yoyo)
                  .SetEase(Ease.InOutSine);
            Debug.Log("hello");
            yield return new WaitForSeconds(2f);
        }
    }

    protected override void Rotate(){
        StartCoroutine(RotateCortine());
    } 

    protected override void OnEnable(){
        base.OnEnable();
        EventHandler.CloseEvent += CloseShake;
    } 

    void OnDisable(){
        EventHandler.CloseEvent -= CloseShake;
    }

    void CloseShake(){
        isFinished = true;
        StopCoroutine(RotateCortine());
        ShakeTweener.Kill();
        Debug.Log("CloseShake");
    }
}
