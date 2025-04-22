using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_4 : Pop
{
    Tweener tweener;
    public float openScale = 0.8f;
    public float closeScale = 0.6f;
    protected override void OnEnable()
    {

        // 打开弹窗动画：缩放从 0 到 1
        tweener = transform.DOScale(openScale, openDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack);
    }

    protected override void OnDisable(){
        base.OnDisable();
        tweener.Kill();
        this.transform.localScale = new Vector3(closeScale, closeScale, closeScale);
    }




}
