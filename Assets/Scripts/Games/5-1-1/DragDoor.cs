using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragDoor : DragPosition
{
    public GameObject closeObj;
    public GameObject popObj;

    public override void Update(){
        float distance = Vector3.Distance(transform.position, maxXTransform.position);
        if(distance < distanceX){
            closeObj.SetActive(false);
            popObj.SetActive(true);
            CameraController.Instance.Shake();
        }

    }
}
