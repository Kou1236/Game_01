using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMouse : MonoBehaviour
{
    void OnEnable(){
        Cursor.visible = false;
    }
    void OnDisable(){
        Cursor.visible = true;
    }
}
