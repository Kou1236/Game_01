using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Pop_9 : Pop
{

    public float verticalSpeed = 4f; // 垂直移动的速度
    public float verticalAmplitude = 10f; // 垂直移动的幅度
    public GameObject firstDialogue;
    public GameObject buttons;
    public GameObject water;

    protected override void OnEnable()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - verticalAmplitude, transform.position.z); // 初始位置为屏幕下方
        // 打开弹窗动画：从下往上弹出
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMoveY(pos.y, verticalSpeed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() => {
            openSeq.Kill();
            StartCoroutine(StartScene());
        });

    }
    IEnumerator StartScene(){
        yield return new WaitForSeconds(1f);
        firstDialogue.SetActive(true);
        buttons.SetActive(true);
        water.SetActive(true);
    }

}
