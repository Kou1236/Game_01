using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMove : MonoBehaviour
{
    public GameObject character;
    public GameObject target;
    public float duration = 1f;

    public void MoveToTarget(){
        character.transform.DOMove(target.transform.position, duration);
    }
}
