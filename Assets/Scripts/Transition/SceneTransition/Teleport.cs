using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public SceneName scenceFrom;
    public SceneName scenceTo;

    public void TeleportToScene(){
        TransitionManager.Instance.Transition(scenceFrom.ToString(), scenceTo.ToString());
    }

}
