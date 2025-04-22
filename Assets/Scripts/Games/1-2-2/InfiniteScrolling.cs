using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfiniteScrolling : Singleton<InfiniteScrolling>
{
    // 背景移动的速度
    public float scrollSpeed = 2f;
    // 单个背景图的宽度
    public float backgroundWidth = 20f;

    public GameObject target;

    

    void OnEnable()
    {
        // 启动动画
        StartScrolling();
    }
    void StartScrolling()
    {
        float currentX = transform.position.x;
        // Tween：将物体在 scrollDuration 时间内沿 X 轴向左平移 backgroundWidth
        transform.DOMoveX(target.transform.position.x, Mathf.Abs(target.transform.position.x - currentX)/scrollSpeed)
                 .SetEase(Ease.Linear) // 匀速移动
                 .OnComplete(() =>
                 {
                     // 当背景对象移动完成后，
                     // 将其位置往右侧偏移 2 * backgroundWidth（假设场景中有2个拼接背景）
                     // 这样当前物体会切换到队列的最右侧，形成连续循环的效果
                     transform.position = new Vector3(target.transform.position.x + 2 * backgroundWidth,
                                                      transform.position.y,
                                                      transform.position.z);
                     // 重启动画，形成无限循环
                     StartScrolling();
                 });
    }

    public void Stop()
    {
        DOTween.KillAll();
    }


}
