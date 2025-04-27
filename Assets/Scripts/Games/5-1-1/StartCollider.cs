using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollider : MonoBehaviour
{
    public GameObject colliderObject;

    void OnEnable(){
        colliderObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
