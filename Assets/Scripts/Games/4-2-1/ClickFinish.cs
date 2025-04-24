using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickFinish : Singleton<ClickFinish>
{
    public int index = 0;
    public int maxIndex = 4;
    
    public void AddIndex(){
        index++;
    }

    public void Finish(){
        if(index >= maxIndex){
            Debug.Log("You Win!");
            ButtonClick_2.Instance.buttonStatus[1] = true;
            ButtonClick_2.Instance.rightButton.GetComponent<Button>().interactable = true;
        }
    }
}
