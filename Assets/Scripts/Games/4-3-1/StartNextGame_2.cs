using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartNextGame_2 : StartNextGame
{
    public Transform obj_1;
    public Transform obj_2;
    public Transform target;
    public float duration = 2f;

    public override void StartGame(){
        Sequence seq = DOTween.Sequence();
        seq.Append(obj_1.DOMove(target.position, duration)).SetEase(Ease.InOutSine);
        seq.Join(obj_2.DOMove(target.position, duration)).SetEase(Ease.InOutSine);
        DOVirtual.DelayedCall(duration + 3f, () => {
            GamePlayManager.Instance.ChangeCharacter();
            GamePlayManager.Instance.EndGame();
            GamePlayManager.Instance.index++;
            GamePlayManager.Instance.StartGame();
        });

    }

    
}
