using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : Singleton<DragManager>
{
    public List<bool> isFinished;
    public GameObject dragObject;
    public GameObject closeObject;

    void Update(){
        CheckAllFinished();
    }
    public void CheckAllFinished(){
        for(int i = 0; i < isFinished.Count; i++){
            if(isFinished.Contains(false))
                break;
            NextLevel();
            
        }
    }

    public void NextLevel(){
        dragObject.GetComponent<Collider2D>().enabled = true;
        closeObject.GetComponent<Collider2D>().enabled = false;
        for(int i = 0; i < isFinished.Count; i++){
            isFinished[i] = false;
        }
    }
}
