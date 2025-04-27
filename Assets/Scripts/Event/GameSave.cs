using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Save", menuName = "Game Save/New Game Save")]
public class GameSave : ScriptableObject
{
    public List<bool> gameCompleted = new List<bool>();

    public void SaveGame(int index){
        gameCompleted[index] = true;
    }

    public void ResetGame(){
        for(int i = 0; i < gameCompleted.Count; i++){
            gameCompleted[i] = false;
        }
    }

    public bool CheckGameStart(){
        if(gameCompleted[0]){
            return true;
        }
        else{
            return false;
        }
        
    }

    public bool CheckGame(int index){
        return gameCompleted[index];
    }
}

