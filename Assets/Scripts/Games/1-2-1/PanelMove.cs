using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelMove : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float speed = 1f;
    public float finalSpeed = 2f;
    public GameObject target;
    public GameObject finalTarget;
    public float openDuration = 2f;

    public GameObject targetObject;
    public GameObject fianlTargetObject;
    public float objectScale = 1f;
    public float movieScale = 0.5f;

    public SpriteRenderer targetSpriteRenderer;
    public SpriteRenderer originalSpriteRenderer;
    public SpriteRenderer fadeTarget;
    public SpriteRenderer fadeOriginal;
    public Sprite sprite;

    public GameObject movie;
    public float movieDuration = 3f;

    public GameObject apple;
    public GameObject block;
    public GameObject earphone;

    public GameObject people;
    public GameObject next;


    public void Move(){

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMoveX(finalTarget.transform.position.x, finalSpeed)).SetEase(Ease.OutBack);
        openSeq.Join(targetObject.transform.DOMoveX(target.transform.position.x, speed)).SetEase(Ease.OutBack);
        openSeq.Join(spriteRenderer.DOFade(0.9f, openDuration));
        openSeq.Join(targetObject.transform.DOScale(objectScale, speed));
        openSeq.Join(targetObject.transform.DOMoveY(fianlTargetObject.transform.position.y, speed));
        StartMovie();
        DOVirtual.DelayedCall(3f, Pop);
        DOVirtual.DelayedCall(7f, MoveCamera);
        movie.SetActive(true);
        movie.transform.DOScale(movieScale, movieDuration);
    }
    public void StartMovie(){
        Debug.Log("start movie!");
        fadeTarget.DOFade(0f, 3f);
        fadeOriginal.DOFade(0f, 3f);
        targetSpriteRenderer.sprite = sprite;
        originalSpriteRenderer.sprite = sprite;
    }

    public void Pop(){
        Debug.Log("pop!");
        earphone.SetActive(true);
        apple.SetActive(true);
        block.SetActive(true);
        people.SetActive(true);
    }
    public void MoveCamera(){
        Debug.Log("move camera!");
        next.SetActive(true);
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget(){
        yield return TransitionManager.Instance.BlackFade(1f);
        TransitionManager.Instance.CameraMove(20f, 0f);
        movie.SetActive(false);
        yield return TransitionManager.Instance.BlackFade(0f);
    }
}
