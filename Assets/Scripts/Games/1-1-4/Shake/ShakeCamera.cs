using UnityEngine;
using DG.Tweening;

public class ShakeCamera : Singleton<ShakeCamera>
{
    private float shakeDuration = 2f;  // 晃动持续时间
    private float shakeStrength = 0.06f;  // 晃动强度
    private int shakeVibrato = 2;      // 晃动的震动频率
    private float shakeRandomness = 1f; // 随机化的角度范围
    private Tweener cameraShakeTweener;
    public GameObject cameraShakeObject;


    public void ShakeLayer()
    {
        if (cameraShakeTweener != null && cameraShakeTweener.IsPlaying())
        {
            cameraShakeTweener.Kill();  // 停止并销毁之前的动画
        }
        cameraShakeTweener = cameraShakeObject.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness)
                 .SetLoops(-1, LoopType.Yoyo);  // Yoyo代表来回晃动
    }

    public void StopShake()
    {
        // 停止相机的晃动
        if (cameraShakeTweener != null && cameraShakeTweener.IsPlaying())
        {
            cameraShakeTweener.Kill(); // 停止晃动动画
        }
    }
}
