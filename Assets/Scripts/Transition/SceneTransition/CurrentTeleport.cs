using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentTeleport : MonoBehaviour
{
    public SceneName scenceTo;

    public void TeleportToScene(){
        Scene current = SceneManager.GetActiveScene();
        Debug.Log("Current Scene: " + current.name);
        TransitionManager.Instance.Transition(current.name, scenceTo.ToString());
    }
}
