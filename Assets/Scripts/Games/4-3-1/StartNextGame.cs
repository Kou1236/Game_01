using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextGame : MonoBehaviour
{
    
    void OnEnable(){
        StartGame();
    }

    public virtual void StartGame(){
        StartCoroutine(NextGame());
    }

    IEnumerator NextGame(){
        Debug.Log("StartNextGame");
        yield return new WaitForSeconds(3.5f);
        GamePlayManager.Instance.ChangeCharacter();
        GamePlayManager.Instance.EndGame();
        GamePlayManager.Instance.index++;
        GamePlayManager.Instance.StartGame();
    }
}
