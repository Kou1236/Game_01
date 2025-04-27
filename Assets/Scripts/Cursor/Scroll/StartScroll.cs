using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScroll : MonoBehaviour
{
    void OnEnable(){
        EventHandler.CallStartScrollEvent();
    }
    void OnDisable(){
        EventHandler.CallEndScrollEvent();
    }
}
