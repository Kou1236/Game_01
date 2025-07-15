using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Pop_14 : Pop
{
    public GameObject target;
    public float speed = 3f;
    public bool isShake = true;

    protected override void OnEnable()
    {

        transform.DOMove(target.transform.position, speed).SetEase(Ease.InOutSine).OnComplete(() =>{
            Debug.Log("www");
            if (isShake){
                Shake shake = this.GetComponent<Shake>();
                shake.enabled = true;
            }
        });
        

    }

    
}
