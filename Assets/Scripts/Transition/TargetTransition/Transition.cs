using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transition : MonoBehaviour
{

    private void OnEnable()
    {
        // 当场景加载后，主动将目标对象传递给主场景中的相机控制器
        EventHandler.SceneTransitionEvent += OnSceneTransitionEvent;
        if (CameraController.Instance != null)
        {
            CameraController.Instance.SetTarget(this.gameObject.transform);
        }
        else
        {
            Debug.LogWarning("未找到 CameraController 实例！");
        }
    }
    private void OnDisable() {
        EventHandler.SceneTransitionEvent -= OnSceneTransitionEvent;
    }

    public void Move(){
        Debug.Log("Move");
        CameraController.Instance.MoveToTarget();
    }

    protected virtual void OnSceneTransitionEvent(){

    }
}
