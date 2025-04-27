using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGameManager : Singleton<TextGameManager>
{
    public List<Text> texts;
    public List<string> textList;
    public List<Sprite> spriteList;
    public SpriteRenderer spriteRenderer;
    public int currentIndex = 0;
    public List<bool> checkList;
    public Image[] buttonImages;
    public Sprite[] buttonOriginalSpriteList;
    public Sprite[] buttonSpriteList;

    public GameObject next;

    private bool isFinish = false;

    void Start(){
        for(int i = 0; i < checkList.Count; i++){
            checkList[i] = false;
        }
        for(int i = 0; i < buttonImages.Length; i++){
            buttonOriginalSpriteList[i] = buttonImages[i].sprite;
        }
    }

    void Update(){
        for(int i = 0; i < checkList.Count; i++){
            if(checkList.Contains(false))
                break;
            Debug.Log("All text are shown");
            FinishGame();
        }
    }


    public void ChangeText(int index){
        if(isFinish)
            return;
        checkList[currentIndex] = currentIndex == index;
        texts[currentIndex].text = textList[index];
        texts[currentIndex].gameObject.SetActive(true);
        currentIndex ++;
    }

    public void ResetText(){
        if(isFinish)
            return;
        if(currentIndex == textList.Count){
            currentIndex = 0;
            foreach(Text text in texts){
                text.gameObject.SetActive(false);
            }
            for(int i = 0; i < checkList.Count; i++){
                checkList[i] = false;
            }

        }
    }

    public void ChangeSprite(int index){
        if(isFinish)
            return;
        spriteRenderer.sprite = spriteList[index];
        buttonImages[index].sprite = buttonSpriteList[index];
        if(currentIndex == textList.Count){
            for(int i = 0; i < buttonImages.Length; i++){
                buttonImages[i].sprite = buttonOriginalSpriteList[i];
            }
        }
    }

    public void FinishGame(){
        isFinish = true;
        for(int j = 0; j < buttonImages.Length; j++){
            buttonImages[j].sprite = buttonSpriteList[j];
        }
        next.SetActive(true);

    }

    
}
