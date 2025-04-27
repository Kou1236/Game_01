using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public float time = 6f;
    public GameObject popObj;

    void OnEnable(){
        DOVirtual.DelayedCall(5f + time, () => {
            popObj.SetActive(true);
            CameraController.Instance.StopShaking();
        });
    }
}
