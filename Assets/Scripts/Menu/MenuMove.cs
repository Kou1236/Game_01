using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuMove : MonoBehaviour
{
    public GameObject target;
    private Vector3 startPos;
    public float speed = 1f;
    private CanvasGroup canvasGroup;
    public float alpha = 0.5f;
    public GameObject popObj;
    public GameObject closeObj;

    void Start()
    {
        startPos = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void MoveTo(){
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(target.transform.position, speed).SetEase(Ease.InOutSine));
        openSeq.Join(canvasGroup.DOFade(alpha, speed));
        openSeq.OnComplete(() => {
            popObj.SetActive(true);
        });
    }

    public void MoveBack(){
        Sequence closeSeq = DOTween.Sequence();
        closeSeq.Append(transform.DOMove(startPos, speed).SetEase(Ease.InOutSine));
        closeSeq.Join(canvasGroup.DOFade(0f, speed));
        closeSeq.OnComplete(() => {
            closeObj.SetActive(false);
        });
    }
}
