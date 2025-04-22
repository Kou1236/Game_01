using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool onClick;
    public Transform indicator;
   

    private void Update(){
        onClick = ObjectAtMousePosition();
        if(onClick && Input.GetMouseButtonDown(0)){
            ClickAction(ObjectAtMousePosition().gameObject);
        }
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            indicator.position = mousePosition;
            indicator.GetComponent<ParticleSystem>().Play();
        }
    }


    private void ClickAction(GameObject gameObject){
        switch(gameObject.tag){
            case "Bird":
                var bird = gameObject.GetComponent<ClickObject_1>();
                bird?.Clicked();
                Debug.Log("Hello");
                break;
            case "Card":
                var card = gameObject.GetComponent<ClickObject_2>();
                card?.Clicked();
                break;
            case "Post":
                var post = gameObject.GetComponent<ClickObject_3>();
                post?.Clicked();
                break;
            case "Ear":
                var ear = gameObject.GetComponent<ClickObject_4>();
                ear?.Clicked();
                break;
            case "Star":
                var star = gameObject.GetComponent<ClickObject_5>();
                star?.Clicked();
                break;
            case "Flower":
                var flower = gameObject.GetComponent<ClickObject_6>();
                flower?.Clicked();
                break;
            case "Water":
                var water = gameObject.GetComponent<ClickObject_7>();
                water?.Clicked();
                break;
            case "Fish":
                var fish = gameObject.GetComponent<ClickObject_8>();
                fish?.Clicked();
                break;
            case "Dice":
                var dice = gameObject.GetComponent<ClickObject_9>();
                dice?.Clicked();
                break;
            case "Food":
                var food = gameObject.GetComponent<ClickObject_10>();
                food?.Clicked();
                break;
        }
    }


    private Collider2D ObjectAtMousePosition(){
        return Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
