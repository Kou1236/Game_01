using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_11 : Pop
{
    public GameObject target;
    public GameObject button;
    public float speed = 5f;
    protected override void OnEnable()
    {

        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(target.transform.position, speed));
        openSeq.OnComplete(() => {
            button.SetActive(true);
        });

    }


    
}
