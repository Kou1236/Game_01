using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveDialogue : Singleton<MoveDialogue>
{

    [Header("第一次对话框")]
    public GameObject firstDialogue;
    public GameObject secondDialogue;
    // public float firstSpeed = 2f;
    public float speedDelta = 0.3f;

    [Header("N次对话框")]
    public float moveSpeed = 2f;
    public GameObject moveTarget;
    public GameObject Dialogues;
    public float firstMoveDistance = 1.5f;
    public float secondMoveDistance = 0.5f;
    public GameObject newDialogue;

    public GameObject DialoguePop;



    public int currentDialogueIndex = 0;
    public bool canPress = false;
    public int maxDialogueIndex = 5;
    // public GameObject[] buttons;
    public int buttonIndex = 0;


    public GameObject water_1;
    public GameObject water_2;

    public GameObject next;

    public Stack<GameObject> dialogueStack = new Stack<GameObject>();

    public List<ButtonList> buttonList;


    public virtual void ShowNextDialogue()
    {
        if(canPress){
            canPress = false;
            buttonList[buttonIndex].button.SetActive(false);
            if(currentDialogueIndex == 0)
            {
                dialogueStack.Push(newDialogue);
                // Debug.Log(dialogueStack.Pop());
                firstDialogue.SetActive(true);
                DOVirtual.DelayedCall(moveSpeed, () =>{
                    secondDialogue.SetActive(true);
                });
                DOVirtual.DelayedCall(2*moveSpeed, () =>{
                    Dialogues.transform.DOMoveY(Dialogues.transform.position.y + firstMoveDistance, moveSpeed);
                    //
                    DialoguePop = dialogueStack.Pop();
                    DialogueList list = DialoguePop.GetComponent<DialogueList>();
                    list.dialogueList[0].SetActive(true);
                    if(currentDialogueIndex == 0){
                        water_1.SetActive(true);
                    }
                    
                });
                DOVirtual.DelayedCall(3*moveSpeed, () =>{
                    moveSpeed = moveSpeed - speedDelta;
                    canPress = true;
                    currentDialogueIndex++;
                });
            }

            if(currentDialogueIndex > 0 && currentDialogueIndex < maxDialogueIndex){
                Dialogues.transform.DOMoveY(Dialogues.transform.position.y + secondMoveDistance, moveSpeed);
                //
                DialogueList list = DialoguePop.GetComponent<DialogueList>();
                list.dialogueList[1].SetActive(true);
                DOVirtual.DelayedCall(2*moveSpeed, () =>{
                    Dialogues.transform.DOMoveY(Dialogues.transform.position.y + secondMoveDistance, moveSpeed);
                    //
                    DialogueList list = DialoguePop.GetComponent<DialogueList>();
                    list.dialogueList[2].SetActive(true);

                });
                DOVirtual.DelayedCall(3*moveSpeed, () =>{
                    GameObject obj = Instantiate(newDialogue, moveTarget.transform.position, Quaternion.identity);
                    obj.transform.SetParent(Dialogues.transform);
                    // todo 加一个栈，存入生成的对话框列表，得以更改内部元素
                    dialogueStack.Push(obj);
                    DialoguePop = dialogueStack.Pop();
                    DialogueList list = DialoguePop.GetComponent<DialogueList>();
                    for(int i = 0; i < list.dialogueList.Count; i++){
                        list.dialogueList[i].SetActive(false);
                    }
                    if(currentDialogueIndex == 1){
                        water_2.SetActive(true);
                    }

                });


                DOVirtual.DelayedCall(4*moveSpeed, () =>{
                    Dialogues.transform.DOMoveY(Dialogues.transform.position.y + firstMoveDistance, moveSpeed);
                    //
                    DialogueList list = DialoguePop.GetComponent<DialogueList>();
                    list.dialogueList[0].SetActive(true);
                });

                DOVirtual.DelayedCall(5*moveSpeed, () =>{
                    moveSpeed = moveSpeed - speedDelta;
                    canPress = true;
                    currentDialogueIndex++;
                });
            }

            if(currentDialogueIndex == maxDialogueIndex){
                Dialogues.transform.DOMoveY(Dialogues.transform.position.y + secondMoveDistance, moveSpeed);
                //
                DialogueList list = DialoguePop.GetComponent<DialogueList>();
                list.dialogueList[1].SetActive(true);
                DOVirtual.DelayedCall(2*moveSpeed, () =>{
                    Dialogues.transform.DOMoveY(Dialogues.transform.position.y + secondMoveDistance, moveSpeed);
                    //
                    DialogueList list = DialoguePop.GetComponent<DialogueList>();
                    list.dialogueList[2].SetActive(true);
                });
                DOVirtual.DelayedCall(3*moveSpeed, () =>{
                    currentDialogueIndex++;
                    
                });
                DOVirtual.DelayedCall(3*moveSpeed + 1f, () =>{
                    next.SetActive(true);
                });
            }
            
        }
        
    }

    public void GetButtonIndex(int index){
        buttonIndex = index;
    }
    
}
