using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStart : MonoBehaviour
{
    public int index;
    public bool isLoop;
    void OnEnable(){
        if(isLoop)
            SoundManager.Instance.PlayBGM(index);
        else
            SoundManager.Instance.PlaySFX(index);
    }
}
