using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PostManager : Singleton<PostManager>
{
    public List<PostList> postLists;
    public int postNum = 3;
    public float time = 1f;

    private int ans;
    private bool isStart = false;

    public bool[] isFinished = new bool[3];

    public float moveXDuration = 4f;
    public float sizeDuration = 4f;

    [Header("第一次设置")]
    public GameObject character;
    public GameObject characterTarget_1;
    public GameObject characterTarget_2;
    public float characterDuration = 4f;

    public GameObject movieBlock_1;
    public GameObject movieBlock_2;
    public GameObject movieTarget_1;
    public GameObject movieTarget_2;
    public GameObject movieTarget_3;
    public GameObject movieTarget_4;

    public GameObject postTarget_1;

    public GameObject rain;

    public float cameraX_1 = 2.56f;
    public float cameraY_1 = 1.5f;
    public float cameraSize_1 = 3.51f;

    [Header("第二次设置")]
    public GameObject characterTarget_3;
    public GameObject characterTarget_4;

    public GameObject posts;
    public GameObject postTarget_2;

    public GameObject movieTarget_5;
    public GameObject movieTarget_6;
    public GameObject movieTarget_7;
    public GameObject movieTarget_8;

    public GameObject postTarget_3;

    public float cameraX_2 = 0f;
    public float cameraY_2 = 2.56f;

    [Header("第三次设置")]
    public GameObject characterTarget_5;



    void OnEnable(){
        // for(int i = 0; i < 3; i++){
        //     SelectLight(i);
        // }
        SelectLight(0);
    }


    public void SelectLight(int sceneIndex){
        StartCoroutine(Light(sceneIndex));
    }

    IEnumerator Light(int index){
        Debug.Log("Light " + index + " is on");
        while(postNum > 0){
            int randomInt = Random.Range(0, 3);
            // if(isStart && postNum == 3){
            //     if(randomInt == ans){
            //         continue;
            //     }
            // }
            
            if(isFinished[randomInt]){
                continue;
            }
            postLists[index].lights[randomInt].SetActive(true);
            postLists[index].posts[randomInt].tag = "Post";
            yield return new WaitForSeconds(1.5f);
            postLists[index].lights[randomInt].SetActive(false);
            postLists[index].posts[randomInt].tag = "Untagged";
            yield return new WaitForSeconds(time);
            ans = randomInt;
            isStart = true;
        }
        if(postNum == 0){
            SetInitial();
        }
        if(index < 3)
            // MovieManager.Instance.PlayMovie(1);
            PlayMovie(index+1);
    }

    void SetInitial(){
        isFinished = new bool[3];
        ans = 0;
        isStart = false;
        postNum = 3;
    }

    public void PlayMovie(int index){
        if(index == 1){
            Debug.Log("Playing Movie 1");
            CameraController.Instance.MoveX(0f, moveXDuration);
            CameraController.Instance.ChangeSize(5f, sizeDuration);
            movieBlock_1.transform.DOMove(movieTarget_1.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            movieBlock_2.transform.DOMove(movieTarget_2.transform.position, moveXDuration).SetEase(Ease.InOutQuad);

            LightEffect.Instance.canLight = false;

            character.transform.DOMove(characterTarget_1.transform.position, characterDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                character.transform.DOMove(characterTarget_2.transform.position, characterDuration*1.8f).SetEase(Ease.InOutSine);
            });
            

            DOVirtual.DelayedCall(characterDuration, () => {

                // character.transform.DOMove(characterTarget_2.transform.position, characterDuration*2).SetEase(Ease.InOutSine);

                CameraController.Instance.MoveX(cameraX_1, moveXDuration*1.8f);
                CameraController.Instance.MoveY(cameraY_1, moveXDuration*1.8f);
                CameraController.Instance.ChangeSize(cameraSize_1, sizeDuration*1.8f);
                rain.transform.DOMoveY(cameraY_1, moveXDuration*1.8f).SetEase(Ease.InOutSine);

                posts.transform.DOMove(postTarget_1.transform.position, moveXDuration*1.8f).SetEase(Ease.InOutQuad);

                movieBlock_1.transform.DOMove(movieTarget_3.transform.position, moveXDuration*1.8f).SetEase(Ease.InOutQuad);
                movieBlock_2.transform.DOMove(movieTarget_4.transform.position, moveXDuration*1.8f).SetEase(Ease.InOutQuad);

            });
            DOVirtual.DelayedCall(characterDuration + moveXDuration*1.8f, () => {
                LightEffect.Instance.canLight = true;
                SelectLight(1);
            });
        }
        if(index == 2){
            Debug.Log("Playing Movie 2");
            LightEffect.Instance.canLight = false;

            CameraController.Instance.MoveY(cameraY_1 + 1f, moveXDuration);
            rain.transform.DOMoveY(cameraY_1 + 1f, moveXDuration).SetEase(Ease.InOutSine);
            movieBlock_1.transform.DOMove(movieTarget_7.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            movieBlock_2.transform.DOMove(movieTarget_8.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            posts.transform.DOMove(postTarget_3.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            CameraController.Instance.ChangeSize(cameraSize_1*0.9f, sizeDuration);

            character.transform.DOMove(characterTarget_3.transform.position, characterDuration).SetEase(Ease.InOutSine).OnComplete(() => {
                character.transform.DOMove(characterTarget_4.transform.position, characterDuration*1.8f).SetEase(Ease.InOutSine);
            });

            DOVirtual.DelayedCall(characterDuration*1.2f, () => {
                CameraController.Instance.MoveX(cameraX_2, moveXDuration*0.8f);
                CameraController.Instance.MoveY(cameraY_2, moveXDuration*0.8f);
                CameraController.Instance.ChangeSize(5f, sizeDuration*0.8f);
                rain.transform.DOMoveY(cameraY_2, moveXDuration*0.8f).SetEase(Ease.InOutSine);

                posts.transform.DOMove(postTarget_2.transform.position, moveXDuration*0.8f).SetEase(Ease.InOutQuad);
                movieBlock_1.transform.DOMove(movieTarget_5.transform.position, moveXDuration*0.8f).SetEase(Ease.InOutQuad);
                movieBlock_2.transform.DOMove(movieTarget_6.transform.position, moveXDuration*0.8f).SetEase(Ease.InOutQuad);
            });
            DOVirtual.DelayedCall(2.8f*characterDuration, () => {
                LightEffect.Instance.canLight = true;
                SelectLight(2);
            });
            
        }
        if(index == 3){
            Debug.Log("Playing Movie 3");
            LightEffect.Instance.canLight = false;
            character.transform.DOMove(characterTarget_5.transform.position, characterDuration).SetEase(Ease.InOutSine);
            Teleport teleport = this.gameObject.GetComponent<Teleport>();
            DOVirtual.DelayedCall(characterDuration, () => {
                teleport.TeleportToScene();
            });
        }


    }
}
