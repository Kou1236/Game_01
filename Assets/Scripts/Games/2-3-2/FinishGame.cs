using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishGame : Singleton<FinishGame>
{
    public CanvasGroup fadeObject_1;
    public CanvasGroup fadeObject_2;
    public GameObject fadeObject_3;
    public GameObject fadeObject_4;

    public GameObject hand;
    public bool finish_1 = false;
    public bool finish_2 = false;


    void OnEnable(){
        EventHandler.FinishGameEvent += OnFinishGameEvent;
    }
    void OnDisable(){
        EventHandler.FinishGameEvent -= OnFinishGameEvent;
    }
    void OnFinishGameEvent(){
        if(finish_1 == true && finish_2 == true){
            Debug.Log("Game Finished");
            fadeObject_1.DOFade(0, 1f);
            fadeObject_2.DOFade(0, 1f);
            DOVirtual.DelayedCall(1f, () => {
                fadeObject_1.gameObject.SetActive(false);
                fadeObject_2.gameObject.SetActive(false);
            });
            fadeObject_3.SetActive(false);
            fadeObject_4.SetActive(true);
            hand.SetActive(true);
        }
    }
}
