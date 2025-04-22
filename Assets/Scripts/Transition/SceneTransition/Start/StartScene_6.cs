using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene_6 : StartScene
{
    public SpriteRenderer rain;
    public Sprite[] rain_sprites;
    public float rain_duration = 0.5f;
    private bool isShowingSprite1 = true;

    public SpriteRenderer scene_1;
    public SpriteRenderer scene_2;
    public float duration = 2f;

    public GameObject target_1;
    public GameObject target_2;

    public GameObject target_3;
    public GameObject target_4;

    public GameObject character;
    // public GameObject posts;

    public float cameraX = -2.92f;
    public float cameraSize = 3.28f;
    public float moveXDuration = 4f;
    public float sizeDuration = 4f;
    
    protected override void StartSceneAction(){

        StartMovie();
    }

    void StartMovie(){
        rain.sprite = rain_sprites[0];
        Sequence switchSequence = DOTween.Sequence();
        switchSequence.AppendInterval(rain_duration);
        switchSequence.AppendCallback(() => ToggleSprite());
        // 设置无限循环
        switchSequence.SetLoops(-1);

        Sequence seq = DOTween.Sequence();
        seq.Append(rain.DOFade(0.7f, duration)).SetEase(Ease.InOutQuad);
        seq.Join(scene_1.gameObject.transform.DOMove(target_1.transform.position, duration)).SetEase(Ease.InOutQuad);
        seq.Join(scene_2.gameObject.transform.DOMove(target_2.transform.position, duration)).SetEase(Ease.InOutQuad);
        // seq.Join(scene_1.DOColor(Color.black, duration));
        // seq.Join(scene_2.DOColor(Color.black, duration));
        DOVirtual.DelayedCall(duration - 1f, ()=> {
            character.SetActive(true);
        });
        seq.OnComplete(()=> {
            scene_1.gameObject.transform.DOMove(target_3.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            scene_2.gameObject.transform.DOMove(target_4.transform.position, moveXDuration).SetEase(Ease.InOutQuad);
            CameraController.Instance.MoveX(cameraX, moveXDuration);
            CameraController.Instance.ChangeSize(cameraSize, sizeDuration);
            // posts.SetActive(true);
            // Destroy(rain.gameObject);
            
        });
    }

    void ToggleSprite()
    {
        if (isShowingSprite1)
        {
            rain.sprite = rain_sprites[1];
        }
        else
        {
            rain.sprite = rain_sprites[0];
        }
        isShowingSprite1 = !isShowingSprite1;
    }
}
