using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChooseKey : MonoBehaviour
{
    public List<GameObject> keys;

    public int currentIndex = 0;

    public float scaleDelta = 1.2f;
    public float duration = 0.2f;
    public float originalScale;

    public List<int> keyIndex = new List<int>();
    public List<Text> keyTexts = new List<Text>();
    public List<int> correctKeyIndex = new List<int>();
    public List<bool> isCorrect = new List<bool>();

    public GameObject nextScene;

    void Start(){
        originalScale = keys[1].transform.localScale.x;
    }

    void Update(){
        for(int i = 0; i < keys.Count; i++){
            isCorrect[i] = keyIndex[i] == correctKeyIndex[i];
        }
        for(int i = 0; i < keys.Count; i++){
            if(isCorrect.Contains(false))
                break;
            Debug.Log("You Win!");
            Finish();
        }
    }



    public void AddIndex(){
        currentIndex = (currentIndex + 1) % keys.Count;
    }
    public void SubIndex(){
        currentIndex = (currentIndex - 1 + keys.Count) % keys.Count;
    }

    public void ChooseSize(){
        keys[currentIndex].transform.DOScale(keys[currentIndex].transform.localScale * scaleDelta, duration).SetEase(Ease.OutBounce);
        for(int i = 0; i < keys.Count; i++){
            if(i != currentIndex){
                keys[i].transform.DOScale(originalScale, duration).SetEase(Ease.OutBounce);
            }
        }
    }

    public void AddKey(){
        keyIndex[currentIndex] = (keyIndex[currentIndex] + 1) % (keys.Count + 1);
        keyTexts[currentIndex].text = keyIndex[currentIndex].ToString();
    }
    public void SubKey(){
        keyIndex[currentIndex] = (keyIndex[currentIndex] - 1 + keys.Count + 1) % (keys.Count + 1);
        keyTexts[currentIndex].text = keyIndex[currentIndex].ToString();
    }

    public void Finish(){
        nextScene.SetActive(true);
        StartCoroutine(WaitAndFinish());
    }

    IEnumerator WaitAndFinish(){
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
