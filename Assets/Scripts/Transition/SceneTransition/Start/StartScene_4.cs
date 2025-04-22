using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_4 : StartScene
{
    public GameObject scene_1;
    public GameObject scene_2;
    public GameObject scene_3;
    public GameObject target;
    public float duration = 2f;
    
    protected override void StartSceneAction(){
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine(){
        scene_1.SetActive(true);
        yield return new WaitForSeconds(2f);

        SpriteRenderer spriteRenderer = scene_1.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0f, 0.5f).SetEase(Ease.InOutSine);
        scene_2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scene_1.SetActive(false);
        yield return new WaitForSeconds(2f);
        scene_2.transform.DOMoveY(target.transform.position.y, duration).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(duration+1f);
        scene_3.SetActive(true);
        
    }
}
