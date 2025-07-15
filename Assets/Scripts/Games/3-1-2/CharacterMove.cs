using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMove : MonoBehaviour
{
    public GameObject character;
    public GameObject target;
    public float duration = 1f;
    public bool isFade = false;

    public void MoveToTarget(){
        character.transform.DOMove(target.transform.position, duration).OnComplete(()=> {
            if(isFade){
                SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();
                renderer.DOFade(0, duration);
            }
        });
    }
}
