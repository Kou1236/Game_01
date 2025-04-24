using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextGame : MonoBehaviour
{
    public GameObject target;
    public GameObject closeObj_1;
    public GameObject closeObj_2;
    public GameObject ball;
    public GameObject originPosition;

    public GameObject game;
    public GameObject gamePosition;
    public Transform final;

    public GameObject note;
    public GameObject book;

    public GameObject next;
    public GameObject dialog;

    public float time = 3f;

    void OnEnable(){
        BackToStart();
    }

    public void BackToStart(){
        target.SetActive(true);
        Transition_18 toStart = target.GetComponent<Transition_18>();
        toStart.Move();
        closeObj_1.SetActive(false);
        closeObj_2.SetActive(false);
        next.SetActive(false);
        dialog.SetActive(false);
        ball.transform.position = originPosition.transform.position;
        ball.SetActive(false);
        final.rotation = Quaternion.identity;

        Sequence seq = DOTween.Sequence();
        SpriteRenderer spriteRenderer = game.GetComponent<SpriteRenderer>();
        seq.Append(spriteRenderer.DOFade(1f, time));
        seq.Join(game.transform.DOMove(gamePosition.transform.position, time)).SetEase(Ease.Linear);
        seq.OnComplete(() => {
            game.GetComponent<Collider2D>().enabled = true;
            note.GetComponent<Collider2D>().enabled = true;
            note.transform.tag = "Note";
            book.transform.tag = "Book";
            this.gameObject.SetActive(false);
        });

    }

    public void ExitGame(){
        target.SetActive(true);
        Transition_18 toStart = target.GetComponent<Transition_18>();
        toStart.Move();
        closeObj_1.SetActive(false);
        closeObj_2.SetActive(false);
        next.SetActive(false);
        dialog.SetActive(false);
        ball.transform.position = originPosition.transform.position;
        ball.SetActive(false);
        final.rotation = Quaternion.identity;

        Sequence seq = DOTween.Sequence();
        SpriteRenderer spriteRenderer = game.GetComponent<SpriteRenderer>();
        seq.Append(spriteRenderer.DOFade(1f, time));
        seq.Join(game.transform.DOMove(gamePosition.transform.position, time)).SetEase(Ease.Linear);
        seq.OnComplete(() => {
            game.GetComponent<Collider2D>().enabled = true;
            book.transform.tag = "Book";
            this.gameObject.SetActive(false);
        });
    }
}
