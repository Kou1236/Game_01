using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSound : MonoBehaviour
{
    public void OnEnable(){
        SoundManager.Instance.StopAllBGM();
    }
}
