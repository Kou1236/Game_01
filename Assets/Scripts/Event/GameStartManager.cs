using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public SceneName sceneName;
    void Start(){
        GameSaveManager.Instance.LoadGame();
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Additive);

    }

}
