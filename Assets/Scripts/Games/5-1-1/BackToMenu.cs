using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    void OnEnable(){
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame(){
        yield return new WaitForSeconds(3f);
        CurrentTeleport teleport = this.gameObject.GetComponent<CurrentTeleport>();
        teleport?.TeleportToScene();
    }
}
