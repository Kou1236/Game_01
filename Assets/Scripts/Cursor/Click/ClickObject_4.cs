using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_4 : ClickObject
{
    
    public List<Sprite> sprites;
    public int currentSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;
    private KeepShake_1 keepShake;
    public bool canClick = false;
    // private DragObject_5 dragObject;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        keepShake = GetComponent<KeepShake_1>();
        // dragObject = GetComponent<DragObject_5>();

    }

    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
    }

    private void OnStartClicKEvent(){
        canClick = true;
    }


    
    public override void Clicked()
    {   
        if(canClick){
            if(currentSpriteIndex < sprites.Count){
                spriteRenderer.sprite = sprites[currentSpriteIndex];
            }
            if(currentSpriteIndex == sprites.Count){
                keepShake.enabled = true;
                // dragObject.enabled = true;
                this.gameObject.tag = "Untagged";
                EventHandler.CallStartDragEvent();
                Debug.Log("Drag");
            }
            currentSpriteIndex ++;
        }
        
    }

    
}
