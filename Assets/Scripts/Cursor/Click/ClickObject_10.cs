using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_10 : ClickObject
{
    public GameObject dialogue;

    public bool canClick = false;

    public int index;

    public GameObject light;

    public GameObject glass;

    public GameObject target;

    public SpriteRenderer glassPop;

    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
    }

    private void OnStartClicKEvent(){
        StartCoroutine(WaitToClick());
    }

    public override void Clicked()
    {   
        if(canClick){
            Debug.Log("Clicked");
            bool isMatch = FoodManager.Instance.IsMatch(index);
            if(isMatch){
                Debug.Log("Match");
                canClick = false;
                if(FoodManager.Instance.currentIndex == FoodManager.Instance.maxCount){
                    FinalLevel();
                }
                else
                    NextLevel();
            }
            else{
                Debug.Log("No Match");
                Pop_12 pop = dialogue.GetComponent<Pop_12>();
                pop.PopObject();
            }
        }

    }

    public void NextLevel(){
        light.SetActive(false);
        // dialogue.SetActive(false);
        FoodManager.Instance.currentIndex ++;
        if(FoodManager.Instance.currentIndex == FoodManager.Instance.maxCount){
            FoodManager.Instance.ChooseFood();
        }
        else{
            FoodManager.Instance.MoveBack();
        }

        SpriteRenderer dialogueSpriteRenderer = dialogue.GetComponent<SpriteRenderer>();
        dialogueSpriteRenderer.DOFade(0f, 1f).OnComplete(() => {
            dialogue.SetActive(false);
            dialogueSpriteRenderer.color = Color.white;
            dialogueSpriteRenderer.sprite = FoodManager.Instance.foodList[FoodManager.Instance.currentIndex].dialogSprite;
        });

        Pop_12 pop = glass.GetComponent<Pop_12>();

        SpriteRenderer glassSpriteRenderer = glass.GetComponent<SpriteRenderer>();

        glassSpriteRenderer.sprite = FoodManager.Instance.foodList[FoodManager.Instance.currentIndex].glassesSprite;
        pop.PopObject();
    }

    public void FinalLevel(){

        light.SetActive(false);
        FoodManager.Instance.Finish();
        SpriteRenderer dialogueSpriteRenderer = dialogue.GetComponent<SpriteRenderer>();
        dialogueSpriteRenderer.DOFade(0f, 1f).OnComplete(() => {
            dialogue.SetActive(false);
            dialogueSpriteRenderer.color = Color.white;
        });
        Pop_12 pop = glass.GetComponent<Pop_12>();

        glassPop.sprite = FoodManager.Instance.foodList[FoodManager.Instance.currentIndex].foodSprites[index];
        glassPop.gameObject.SetActive(true);
        pop.PopObject();

        StartCoroutine(WaitToMove());

    }



    IEnumerator WaitToClick()
    {
        yield return new WaitForSeconds(1f);
        canClick = true;
    }

    IEnumerator WaitToMove(){
        yield return new WaitForSeconds(2f);
        target.SetActive(true);
        Transition_12 transition = target.GetComponent<Transition_12>();
        transition.Move();
    }
}
