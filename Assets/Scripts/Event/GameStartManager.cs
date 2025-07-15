using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public SceneName sceneName;
    public GameObject button;
    public Texture2D cursorTexture;
    
    void Start(){
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        GameSaveManager.Instance.LoadGame();
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Additive);
        StartCoroutine(LoadButton());

    }

    IEnumerator LoadButton(){
        yield return new WaitForSeconds(1f);
        button.SetActive(true);
    }

}
