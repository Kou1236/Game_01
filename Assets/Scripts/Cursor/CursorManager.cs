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
            case "TV":
                var tv = gameObject.GetComponent<ClickObject_11>();
                tv?.Clicked();
                break;
            case "Book":
                var book = gameObject.GetComponent<ClickObject_12>();
                book?.Clicked();
                break;
            case "Neko":
                var neko = gameObject.GetComponent<ClickObject_13>();
                neko?.Clicked();
                break;
            case "Can":
                var can = gameObject.GetComponent<ClickObject_14>();
                can?.Clicked();
                break;
            case "Game":
                var game = gameObject.GetComponent<ClickObject_15>();
                game?.Clicked();
                break;
            case "Note":
                var note = gameObject.GetComponent<ClickObject_16>();
                note?.Clicked();
                break;
            case "Sticker":
                var sticker = gameObject.GetComponent<ClickObject_17>();
                sticker?.Clicked();
                break;
            case "Draw":
                var draw = gameObject.GetComponent<ClickObject_18>();
                draw?.Clicked();
                break;

        }
    }


    private Collider2D ObjectAtMousePosition(){
        return Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
