using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveDialogue_1 : MoveDialogue
{

    [Header("Dialogue 1")]
    public float moveDistance = 0.5f;
    public float popTime = 1f;
    private float index;
    // 引用目标物体上挂载的 Animator 组件
    public Animator animator;
    // 在 Animator Controller 中设置的 Bool 参数名称
    public string boolParameterName = "Start";
    public GameObject bubbles;

    public override void ShowNextDialogue()
    {
        if(canPress){

            DOVirtual.DelayedCall(popTime, () =>{
                animator.SetBool(boolParameterName, true);
            });

            canPress = false;
            index = buttonIndex/2;
            buttonList[(int)Mathf.Floor(index)*2].button.SetActive(false);
            Debug.Log(index);
            buttonList[(int)Mathf.Floor(index)*2+1].button.SetActive(false);

            // if(currentDialogueIndex == 0){
            //     dialogueStack.Push(newDialogue);
            //     DialoguePop = dialogueStack.Pop();
            // }
            

            if(currentDialogueIndex < maxDialogueIndex){

                if(currentDialogueIndex == 0){
                    dialogueStack.Push(newDialogue);
                    DialoguePop = dialogueStack.Pop();
                }

                Dialogues.transform.DOMoveY(Dialogues.transform.position.y + moveDistance, moveSpeed);
                //
                DialogueList list = DialoguePop.GetComponent<DialogueList>();

                SpriteRenderer sr_right = list.dialogueList[0].GetComponent<SpriteRenderer>();
                SpriteRenderer sr_left = list.dialogueList[1].GetComponent<SpriteRenderer>();
                
                sr_right.sprite = buttonList[buttonIndex].Image_right;
                sr_left.sprite = buttonList[buttonIndex].Image_left;

                list.dialogueList[0].SetActive(true);
                list.dialogueList[1].SetActive(true);
                // DOVirtual.DelayedCall(popTime, () =>{
                //     list.dialogueList[1].SetActive(true);
                // });
                
                GameObject obj = Instantiate(newDialogue, moveTarget.transform.position, Quaternion.identity);
                obj.transform.SetParent(Dialogues.transform);
                obj.transform.localScale = new Vector3(1, 1, 1);
                // todo 加一个栈，存入生成的对话框列表，得以更改内部元素
                dialogueStack.Push(obj);
                DialoguePop = dialogueStack.Pop();
                list = DialoguePop.GetComponent<DialogueList>();
                for(int i = 0; i < list.dialogueList.Count; i++){
                    list.dialogueList[i].SetActive(false);
                }

                moveSpeed = moveSpeed - speedDelta;
                canPress = true;
                currentDialogueIndex++;

            }

            if(currentDialogueIndex == maxDialogueIndex){
                Dialogues.transform.DOMoveY(Dialogues.transform.position.y + moveDistance, moveSpeed);
                //
                DialogueList list = DialoguePop.GetComponent<DialogueList>();

                SpriteRenderer sr_right = list.dialogueList[0].GetComponent<SpriteRenderer>();
                SpriteRenderer sr_left = list.dialogueList[1].GetComponent<SpriteRenderer>();
                
                sr_right.sprite = buttonList[buttonIndex].Image_right;
                sr_left.sprite = buttonList[buttonIndex].Image_left;

                list.dialogueList[0].SetActive(true);
                list.dialogueList[1].SetActive(true);
                // DOVirtual.DelayedCall(popTime, () =>{
                //     list.dialogueList[1].SetActive(true);
                // });

                DOVirtual.DelayedCall(moveSpeed + 2f, () => {
                    bubbles.SetActive(true);
                });
            }
            
        }
        
    }

    


    
}
