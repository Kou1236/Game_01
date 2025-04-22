using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoodManager : Singleton<FoodManager>
{
    public List<FoodList> foodList;
    public SpriteRenderer[] foodPrefabs;
    public GameObject chooseFood;
    public GameObject closeFood;
    public int currentIndex = 0;
    public int maxCount = 4;
    private Vector3 startPosition;
    public float duration = 3f;

    public int finalCount = 0;

    void Start(){
        startPosition = transform.position;
    }

    public bool IsMatch(int index){
        if(currentIndex < maxCount)
            return foodList[currentIndex].foodCount == index;
        else{
            finalCount = index;
            return true;
        }
    }

    public void MoveBack(){
        transform.DOMove(startPosition, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
            for(int i = 0; i < foodPrefabs.Length; i++){
                foodPrefabs[i].sprite = foodList[currentIndex].foodSprites[i];
            }
            this.gameObject.SetActive(false);
        });
        DOVirtual.DelayedCall(duration + 0.8f, ()=> {
            this.gameObject.SetActive(true);
        });
    }

    public void ChooseFood(){
        transform.DOMove(startPosition, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
            for(int i = 0; i < foodPrefabs.Length; i++){
                foodPrefabs[i].sprite = null;
            }
            this.gameObject.SetActive(false);
            closeFood.SetActive(false);
        });
        DOVirtual.DelayedCall(duration + 0.8f, ()=> {
            this.gameObject.SetActive(true);
            chooseFood.SetActive(true);
        });
    }

    public void Finish(){
        transform.DOMove(startPosition, duration).SetEase(Ease.InOutSine).OnComplete(()=> {
            this.gameObject.SetActive(false);
        });
    }
}
