using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSound : MonoBehaviour
{
    private bool isPlay = false;
    public int index;
    void OnMouseDrag(){
        if(!isPlay){
            isPlay = true;
            SoundManager.Instance.PlayBGM(index);
        }
    }
    void OnMouseUp(){
        SoundManager.Instance.StopAllBGM();
        isPlay = false;
    }
    void OnDisable(){
        SoundManager.Instance.StopAllBGM();
    }

}
