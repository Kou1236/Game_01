using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_13 : Pop
{

    public float scale = 1.4f;
    public float scaleDuration = 0.5f;
    public GameObject popObject_1;
    public GameObject popObject_2;
    public bool  isPop = true;
    
    protected override void OnEnable()
    {

        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(spriteRenderer.DOFade(1f, openDuration)).SetEase(Ease.InOutSine);
        openSeq.Join(transform.DOScale(scale, scaleDuration).SetEase(Ease.InOutSine));
        if (isPop){
            DOVirtual.DelayedCall(scaleDuration - 0.5f, () => {
                Debug.Log("Distortion");
                DistortionController.Instance.targetMaterial.SetFloat("_DistortionAmount", 0.8f);
            });
            openSeq.OnComplete(() => {
                popObject_1.SetActive(true);
                popObject_2.SetActive(true);
        });
        }
        
    }


    
}
