using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMenu : MonoBehaviour
{
    public GameSave gameSave;
    public bool isMenuOpen = false;
    public GameObject menu;
    public GameObject block;
    public List<Button> buttons;

    public void Check()
    {
        isMenuOpen = gameSave.CheckGameStart();
        if(isMenuOpen){
            menu.SetActive(true);
            block.SetActive(true);
        }
        else{
            Teleport();
        }

    }

    public void SetButton(){
        for(int i = 0; i < buttons.Count; i++){
            buttons[i].interactable = gameSave.CheckGame(i);
        }
    }

    public void Reset(){
        gameSave.ResetGame();
    }

    public void Teleport(){
        Teleport teleport = this.gameObject.GetComponent<Teleport>();
        teleport?.TeleportToScene();
    }

    public void QuitGame(){
        StartCoroutine(QuitDelay());
    }

    IEnumerator QuitDelay(){
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

    
}
