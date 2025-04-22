using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_11 : ClickObject
{

    public List<Sprite> sprites;
    public SpriteRenderer spriteRenderer;
    public GameObject button;
    public int index = 0;
    public bool isClicked = false;
    

    public override void Clicked()
    {   
        index = (index + 1) % sprites.Count;
        spriteRenderer.sprite = sprites[index];
        if(!isClicked){
            isClicked = true;
            StartCoroutine(nextScene());
        }

    }

    IEnumerator nextScene(){
        yield return new WaitForSeconds(3f);
        button.SetActive(true);
    }

}
