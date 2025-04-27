using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMove : MonoBehaviour
{
    public Transform door_1;
    public Transform door_2;
    public Transform target_1;
    public Transform target_2;
    public float duration = 1f;
    public GameObject popObj;

    void OnEnable(){
        MoveDoor();
    }
    
    public void MoveDoor(){
        door_1.DOMove(target_1.localPosition, duration).SetEase(Ease.InOutSine);
        door_2.DOMove(target_2.localPosition, duration).SetEase(Ease.InOutSine);
        DOVirtual.DelayedCall(duration, () => {
            popObj.SetActive(true);
            this.gameObject.SetActive(false);
        });

    }
}
