using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
    public List<bool> draws = new List<bool>();

    public void CanDraw(int index){
        draws[index] = true;
    }
    public void CannotDraw(int index){
        draws[index] = false;
    }

    public void SetAllFalse(){
        for(int i = 0; i < draws.Count; i++){
            draws[i] = false;
        }
    }
}
