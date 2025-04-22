using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // 晃动持续时间
    public float shakeStrength = 0.1f;  // 晃动强度
    public int shakeVibrato = 10;      // 晃动的震动频率
    public float shakeRandomness = 90f; // 随机化的角度范围

    void Start()
    {
        if(transform.parent.gameObject.activeInHierarchy){
            Debug.Log("parent active");
            this.gameObject.SetActive(true);
        }
    }

    void OnEnable()
    {
        // 在每个图层的开始时，开始独立的晃动效果
        ShakeLayer();
    }

    public void ShakeLayer()
    {
        transform.DOShakePosition(shakeDuration, new Vector3(shakeStrength, 0, 0), shakeVibrato, shakeRandomness)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
