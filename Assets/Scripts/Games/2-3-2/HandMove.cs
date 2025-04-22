using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandMove : MonoBehaviour
{
   // 目标抬升后的本地 Y 坐标
    public Transform targetY;
    public Transform originalY;

    public float duration = 0.5f;

    private Vector3 initialPos;
    private Tween currentTween;
    // private bool canClick = true;

    public float reachThreshold = 0.1f;
    private bool reached = false;

    public GameObject dialogueObject;
    public GameObject button;

    void Start()
    {
        // 记录初始本地坐标
        initialPos = originalY.transform.localPosition;
    }

    void Update(){
        if (!reached && Mathf.Abs(transform.localPosition.y - targetY.localPosition.y) <= reachThreshold)
        {
            reached = true;
            Debug.Log("Hand reached target");
            GetComponent<Collider2D>().enabled = false;
            // 精准对齐到 exact targetY
            currentTween = transform.DOLocalMoveY(targetY.localPosition.y, duration * 0.5f).SetEase(Ease.OutCubic).OnComplete(() => {
                StartCoroutine(NextScene());
            });
        }
    }

    // 鼠标按下时触发
    void OnMouseDown()
    {
        // 停掉可能正在运行的返回 Tween
        currentTween?.Kill();
        // 计算目标位置
        Vector3 upPos = new Vector3(initialPos.x, targetY.localPosition.y, initialPos.z);
        // Tween 到目标位置
        currentTween = transform.DOLocalMove(upPos, duration/2f).SetEase(Ease.OutCubic);
        // DOVirtual.DelayedCall(duration/3f, MoveBack);

    }

    // 鼠标抬起时触发
    void OnMouseUp()
    {
        if (!reached)
            MoveBack();
    }

    void MoveBack(){
        // 停掉可能正在运行的向上 Tween
        currentTween?.Kill();
        // Tween 回到初始位置
        currentTween = transform.DOLocalMove(initialPos, duration).SetEase(Ease.OutCubic);
    }

    IEnumerator NextScene(){
        dialogueObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }
}
