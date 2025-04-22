using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_2 : ClickObject
{
    public GameObject background;
    public GameObject foreground;
    
    public override void Clicked()
    {
        // Teleport teleport = this.GetComponent<Teleport>();
        // if (teleport != null)
        // {
        //     teleport.TeleportToScene();
        // }
        // else
        // {
        //     Debug.LogWarning("未在目标对象上找到 Teleport 组件。");
        // }
        background.SetActive(false);
        foreground.SetActive(true);
    }
}
