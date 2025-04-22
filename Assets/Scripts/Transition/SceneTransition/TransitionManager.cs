using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionManager : Singleton<TransitionManager>
{   
    public CanvasGroup fadeCanvasGroup;
    public CanvasGroup blackCanvasGroup;
    public float fadeDuration = 4f;

    public float blackDuration = 0.5f;
    private bool isFaded;

    public GameObject camera;


    public void Transition(string from, string to){
        if(!isFaded)
            StartCoroutine(TransitionToScene(from, to));
        DOTween.KillAll(true);
    }
    

    private IEnumerator TransitionToScene(string from, string to){
        // FadeIn();
        // yield return new WaitForSeconds(1f);
        yield return Fade(1f);
        if(from != string.Empty){
            yield return SceneManager.UnloadSceneAsync(from); 
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(currentScene);
        CameraMove(0f, 0f);
        yield return Fade(0f);
        // FadeOut();
    }


    // // 使用 DOTween 实现淡入效果
    // public void FadeIn()
    // {   
    //     Debug.Log("FadeIn");
    //     isFaded = true;
    //     fadeCanvasGroup.blocksRaycasts = true;
    //     fadeCanvasGroup.DOFade(1f, fadeDuration).OnStart(() =>
    //     {
    //         Debug.Log("FadeIn Start");
    //         fadeCanvasGroup.alpha = 0f; // 确保从透明开始
    //     });
    // }

    // // 使用 DOTween 实现淡出效果
    // public void FadeOut()
    // {
    //     Debug.Log("FadeOut");
    //     CameraMove(0f);
    //     fadeCanvasGroup.blocksRaycasts = true;
    //     fadeCanvasGroup.DOFade(0f, fadeDuration).OnKill(() =>
    //     {
    //         fadeCanvasGroup.blocksRaycasts = false;
    //     });
    //     isFaded = false;
    // }

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

    public IEnumerator BlackFade(float targetAlpha){
        isFaded = true;
        blackCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(targetAlpha - blackCanvasGroup.alpha) / blackDuration;
        while(!Mathf.Approximately(blackCanvasGroup.alpha, targetAlpha)){
            blackCanvasGroup.alpha = Mathf.MoveTowards(blackCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        blackCanvasGroup.blocksRaycasts = false;
        isFaded = false;
    }


    public void CameraMove(float x, float y){
        camera.transform.position = new Vector3(x, 0, -10);
    }
}
