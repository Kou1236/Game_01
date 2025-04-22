using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickObject_9 : ClickObject
{
    
    public bool canClick = false;

    private Animator animator;
    public string boolParameterName = "Start";
    private int currentNum = 1;
    private int maxNum;



    private void OnEnable(){
        EventHandler.StartClickEvent += OnStartClicKEvent;
    }
    private void OnDisable(){
        EventHandler.StartClickEvent -= OnStartClicKEvent;
    }

    private void OnStartClicKEvent(){
        canClick = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        maxNum = DiceState.Instance.maxDiceIndex;
    }
   



    
    public override void Clicked()
    {   
        if(canClick){
            canClick = false;
            
            Debug.Log("Clicked");
            animator.SetBool(boolParameterName, true);
            if(currentNum < maxNum - 4){
                ChangeNum();
            }
            else if(currentNum >= maxNum - 4 && currentNum < maxNum - 1){
                Debug.Log("hhhhhh");
                NextChangeNum();
            }
            else if(currentNum == maxNum - 1){
                Debug.Log("Final");
                FinalChangeNum();
                canClick = false;
            }
        }
        
    }

    public void ChangeNum(){
        int randomNum = Random.Range(0, 4);
        DiceState.Instance.SetDice(randomNum);
        DiceState.Instance.ChangeStateAnimation(randomNum);
        currentNum = DiceState.Instance.currentDiceIndex;
        Debug.Log(currentNum);
    }

    public void NextChangeNum(){
        DiceState.Instance.SetDice(maxNum -currentNum - 2);
        DiceState.Instance.ChangeStateAnimation(maxNum -currentNum - 2);
        currentNum = DiceState.Instance.currentDiceIndex;
        Debug.Log(currentNum);
    }

    public void FinalChangeNum(){
        DiceState.Instance.SetDice(0);
        DiceState.Instance.ChangeStateAnimation(0);
        currentNum = DiceState.Instance.currentDiceIndex;
        Debug.Log(currentNum);
    }

    public void MoveCharacter(){
        WayMove.Instance.MoveTo(currentNum - 1);
    }



    public void SetCanClick(){
        canClick = false;
    }
}
