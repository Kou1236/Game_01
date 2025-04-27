using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScale : MonoBehaviour
{
    public Vector3 startScale;
    public float scaleTime = 1f;
    public Transform target;
    public GameObject popObj;
    public GameObject popObj_2;
    public GameObject popObj_3;
    public GameObject closeObj;
    public Transform moveObj;
    public Transform moveTarget;

    void OnEnable(){
        popObj_2.SetActive(true);
        moveObj.DOMove(moveTarget.position, scaleTime).SetEase(Ease.InOutSine);
        target.DOScale(startScale, scaleTime).SetEase(Ease.InOutSine).OnComplete(()=> {
            SpriteRenderer spriteRenderer = closeObj.GetComponent<SpriteRenderer>();
            spriteRenderer.DOFade(0, scaleTime);
        });
        DOVirtual.DelayedCall(scaleTime*2, ()=> {
            popObj.SetActive(true);
            closeObj.SetActive(false);
            popObj_3.SetActive(true);
        });
    }
}
