using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickMove : MonoBehaviour
{
    public Transform targetTransform;
    public float speed = 5f;              // 单位：世界单位/秒
    public float clickHoldDuration = 0.5f;// 点击后保持在目标的时间（秒）

    private Vector3 originalPos;
    private float clickTimer = 0f;
    private bool movingToTarget = false;
    public Transform finalTransform;
    public float duration = 1f;
    public GameObject dialogue;

    private bool canMove = true;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        // 点击检测
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);
            if (hit.collider != null && hit.collider.transform == transform)
            {
                clickTimer = clickHoldDuration;               // 重置缓冲时间
                movingToTarget = true;                        
                DOTween.Kill(transform);                      // 杀死旧 Tween :contentReference[oaicite:3]{index=3}
                float dist = Vector3.Distance(transform.position, targetTransform.position);
                transform.DOMove(targetTransform.position, dist / speed)
                         .SetEase(Ease.Linear)
                         .OnComplete(() => { 
                            movingToTarget = false; 
                            canMove = false;
                            FinishMove();
                            });               // 启动新 Tween :contentReference[oaicite:4]{index=4}
            }
        }

        // 缓冲时间倒计时
        if (clickTimer > 0f)
            clickTimer -= Time.deltaTime;
        else if (movingToTarget)
        {
            // 缓冲期结束：回原点
            movingToTarget = false;
            DOTween.Kill(transform);                          // 杀死旧 Tween
            float dist = Vector3.Distance(transform.position, originalPos);
            transform.DOMove(originalPos, dist / speed)
                     .SetEase(Ease.Linear);
        }
    }

    void FinishMove(){
        transform.DOMove(finalTransform.position, duration).SetEase(Ease.InOutCubic).OnComplete(() => {
            Destroy(gameObject);
            dialogue.SetActive(true);
        });
    }
}
