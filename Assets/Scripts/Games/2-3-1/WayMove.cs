using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WayMove : Singleton<WayMove>
{
    public Transform[] points;
    public GameObject character;
    public float duration = 1f;
    public int currentIndex = 0;

    public GameObject closeScene;
    public GameObject openScene;

    


    public void MoveTo(int index){
        Sequence openSeq = DOTween.Sequence();
        for(int i = currentIndex; i < index; i++){
            Vector3 targetPos = points[i].position;
            openSeq.Append(character.transform.DOMove(targetPos, duration).SetEase(Ease.InOutSine));
        }
        openSeq.OnComplete(()=> {
            currentIndex = index;
            if(currentIndex < points.Length)
                PictureList.Instance.PopPicture(currentIndex - 1);
            else if(currentIndex == points.Length){
                NextScene();
            }
        });
    }

    public void NextScene(){
        openScene.SetActive(true);
        DOVirtual.DelayedCall(2f, ()=> {
            closeScene.SetActive(false);
        });
    }
}
