using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishTrigger : MonoBehaviour
{
    public GameObject nextLevel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("到达终点！");
            NextLevel();
            
        }
    }

    public void NextLevel(){
        nextLevel.SetActive(true);

    }

    
}
