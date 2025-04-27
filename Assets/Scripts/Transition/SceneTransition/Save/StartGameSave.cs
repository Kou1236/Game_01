using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSave : MonoBehaviour
{
    public GameSave gameSave;
    public int saveIndex;   
    void OnEnable(){
        gameSave.SaveGame(saveIndex);
        GameSaveManager.Instance.SaveGame();
    }
}
