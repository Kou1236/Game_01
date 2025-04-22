using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    private bool isFaded;

    public GameObject popObject;

    public void FadeInImmediately(){
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn(){
        yield return Fade(1f);
        Debug.Log("Fade in complete");
        popObject.SetActive(true);
        yield return Fade(0f);

    }
    public IEnumerator Fade(float targetAlpha){
        isFaded = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(targetAlpha - fadeCanvasGroup.alpha) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha)){
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFaded = false;
    }
}
