using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_7 : Pop
{
    public float fadenum = 0.7f;
    public bool isPop = false;
    public bool isClose = false;
    public GameObject popObj;
    public GameObject closeObj;

    protected override void OnEnable()
    {
        // 确保初始透明度为0
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        // 打开弹窗动画：缩放从 0 到 1，并淡入
        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(spriteRenderer.DOFade(fadenum, openDuration));
        openSeq.OnComplete(() => {
            if (isPop){
                popObj.SetActive(true);
            }
            if (isClose){
                closeObj.SetActive(false);
            }
        });

    }

    
}
