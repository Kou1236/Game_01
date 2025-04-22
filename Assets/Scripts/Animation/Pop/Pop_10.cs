using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_10 : Pop
{
    Tweener tweener;
    public float openScale = 0.8f;
    public float originalScale = 1f;
    public bool isStart = false;

    
    protected override void OnEnable()
    {

        // 打开弹窗动画：缩放从 0 到 1
        tweener = transform.DOScale(openScale, openDuration).SetEase(Ease.OutBack);
        DOVirtual.DelayedCall(openDuration, () =>{
            transform.DOScale(originalScale, openDuration).SetEase(Ease.OutBack);
        });
        Debug.Log("hello");
        DOVirtual.DelayedCall(2*openDuration, () =>{
            if(isStart){
                Debug.Log("can press");
                MoveDialogue.Instance.canPress = true;
                
            }
        });
    }


}
