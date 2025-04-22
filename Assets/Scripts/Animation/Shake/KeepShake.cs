using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeepShake : MonoBehaviour
{
    [Header("旋转设置")]
    public float rotationAngle = 80f;      // 旋转的最大角度（正负5度左右）
    public float duration = 0.3f;         // 每个半周期的时长


    protected virtual void OnEnable()
    {
        Rotate();
    }


    protected virtual void Rotate(){
        
    } 
}
