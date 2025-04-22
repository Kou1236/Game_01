using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_8 : Pop
{
    public GameObject target;
    public float speed = 5f;
    public int isPopIndex = 0;
    public GameObject popObject;
    public GameObject tagObject;

    protected override void OnEnable()
    {

        Sequence openSeq = DOTween.Sequence();
        openSeq.Append(transform.DOMove(target.transform.position, speed).SetEase(Ease.InOutSine));
        openSeq.OnComplete(() =>{
            Debug.Log("hhh");
            switch (isPopIndex)
            {
                case 1:
                    // ClickObject_4.Instance.canClick = true;
                    EventHandler.CallStartClickEvent();
                    Debug.Log("pop finished");
                    break;
                case 2:
                    popObject.SetActive(true);
                    break;

                case 3:
                    tagObject.gameObject.tag = "Post";
                    break;
            }
        });

    }

    
}
