using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCollider : MonoBehaviour
{
    public GameObject colliderObject;

    void OnEnable(){
        colliderObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
