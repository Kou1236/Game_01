using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public List<GameObject> games;
    public GameObject character;
    public List<Sprite> characters;
    public int index = 0;

    public void StartGame(){
        if(index < games.Count && index >= 0){
            games[index].SetActive(true);
        }
    }

    public void EndGame(){
        if(index < games.Count && index >= 0){
            games[index].SetActive(false);
            if(index == games.Count - 1){
                StartCoroutine(NextGame());
            }
        }
    }

    public void ChangeCharacter(){
        if(index < characters.Count && index >= 0){
            character.GetComponent<SpriteRenderer>().sprite = characters[index];
        }
    }

    IEnumerator NextGame(){
        yield return new WaitForSeconds(1f);
        Teleport teleport = GetComponent<Teleport>();
        teleport?.TeleportToScene();
    }
}
