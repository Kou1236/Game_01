using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallScale : MonoBehaviour
{
    public Vector3 startScale;
    public float scaleTime = 5f;
    public GameObject popObj;
    public GameObject closeObj;
    public Transform target;

    void OnEnable(){
        popObj.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(target.DOScale(startScale, scaleTime).SetEase(Ease.InOutSine)).OnComplete(() => {
            closeObj.SetActive(false);
        });

    }
}
