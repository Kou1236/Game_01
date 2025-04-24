using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonClick_2 : Singleton<ButtonClick_2>
{
    public int currentIndex = 0;
    public int maxIndex = 3;
    public GameObject leftButton;
    public GameObject rightButton;
    public bool[] buttonStatus;

    public GameObject[] scenes;
    public Sprite[] sceneSprites;
    public SpriteRenderer sceneRenderer;

    public GameObject book;
    public Transform bookTransform;
    public GameObject game;
    public GameObject note;
    public float duration = 2.2f;

    public GameObject target;

    public GameObject finalButton;

    public void AddIndex(){
        currentIndex++;
    }

    public void MinusIndex(){
        currentIndex--;
    }

    public void ResetIndex(){
        currentIndex = 0;
    }

    public void CheckIndex(){
        if(currentIndex == maxIndex){
            rightButton.SetActive(false);
            leftButton.SetActive(false);
            StartCoroutine(MoveBack());

        }
        if(currentIndex == 0){
            leftButton.SetActive(false);
        }

    }

    public void CheckStatus(){
        if(buttonStatus[currentIndex] == true){
            rightButton.GetComponent<Button>().interactable = true;
        }
        else{
            rightButton.GetComponent<Button>().interactable = false;
        }
    }

    public void SetScene(){
        scenes[currentIndex].SetActive(true);
        sceneRenderer.sprite = sceneSprites[currentIndex];
        for(int i = 0; i < scenes.Length; i++){
            if(i!= currentIndex){
                scenes[i].SetActive(false);
            }
        }
    }

    IEnumerator MoveBack(){
        yield return new WaitForSeconds(2f);
        sceneRenderer.sprite = sceneSprites[0];
        sceneRenderer.gameObject.SetActive(false);
        ResetIndex();
        rightButton.GetComponent<Button>().interactable = true;
        
        target.SetActive(true);
        Transition_18 transition = target.GetComponent<Transition_18>();
        transition.Move();
        Sequence s = DOTween.Sequence();
        s.Append(note.transform.DOMove(bookTransform.position, duration).SetEase(Ease.Linear));
        s.Join(note.GetComponent<SpriteRenderer>().DOFade(1f, duration).SetEase(Ease.InOutSine));
        s.OnComplete(() => {
            note.GetComponent<Collider2D>().enabled = true;
            game.transform.tag = "Game";
            book.transform.tag = "Book";
            finalButton.SetActive(true);
        });
    }


}
