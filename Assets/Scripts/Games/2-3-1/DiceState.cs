using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceState : Singleton<DiceState>
{
    public int currentDiceIndex = 1;
    public int maxDiceIndex = 10;

    public  string intParameterName = "State";
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDice(int index){
        currentDiceIndex += (index + 1);
    }

    public void ChangeStateAnimation(int index){
        if(animator != null)
        {
            animator.SetInteger(intParameterName, index);
        }
    }


    
}
