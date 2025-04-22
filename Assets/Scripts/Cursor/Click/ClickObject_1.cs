using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_1 : ClickObject
{
    public float fadeDuration = 0.5f;

    public float scaleDuration = 0.2f;

    public float scale = 0.1f;
    public override void Clicked()
    {
        this.transform.DOScale(scale, scaleDuration).SetEase(Ease.InOutSine);
        // 如果物体上有 SpriteRenderer，则执行淡出动画
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                // 淡出后销毁该物体
                Destroy(gameObject);
            });
        }
        else
        {
            // 如果没有 SpriteRenderer，直接销毁物体
            Debug.Log("No SpriteRenderer found on " + gameObject.name);
        }
    }
}
