using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStop_1 : AnimationStop
{


    
    
    // 动画事件调用该方法：动画播放完成后执行此回调
    public override void OnAnimationFinished()
    {
        Teleport teleport = this.GetComponent<Teleport>();
        if (teleport != null)
        {
            teleport.TeleportToScene();
        }
        else
        {
            Debug.LogWarning("未在目标对象上找到 Teleport 组件。");
        }
    }

    

    
}
