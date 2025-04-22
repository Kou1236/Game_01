using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PianoManager : Singleton<PianoManager>
{
    public List<int> noteList = new List<int>();
    public SpriteRenderer noteSpriteRenderer;
    public Sprite[] noteSprites;
    private int currentNoteIndex = 0;

    void Start(){
        // DontDestroyOnLoad(this.gameObject);
        Debug.Log("PianoManager Start");
    }

    public void GetNoteIndex(int noteIndex){
        noteList.Add(noteIndex);
        currentNoteIndex ++;
    }

    public void ChangeNoteSprite(){
        noteSpriteRenderer.sprite = noteSprites[currentNoteIndex];
    }


}
