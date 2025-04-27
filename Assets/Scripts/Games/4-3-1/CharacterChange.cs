using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChange : MonoBehaviour
{
    public SpriteRenderer CharacterSpriteRenderer;
    public SpriteRenderer CharacterSpriteRenderer2;
    public Sprite[] CharacterSprites;
    public Sprite[] CharacterSprites2;
    public int index = 0;

    public GameObject button;
    public GameObject scene_1;
    public GameObject scene_2;

     // 累计时间
    private float timer = 0f;
    // 触发间隔（秒）
    public float interval = 1f;

    public void AddIndex()
    {
        index++;
    }
    public void SubIndex()
    {
        if(index > 0)
            index--;
    }

    public void ChangeCharacter(){
        if(index >= 0 && index < 6){
            CharacterSpriteRenderer.sprite = CharacterSprites[0];
            CharacterSpriteRenderer2.sprite = CharacterSprites2[0];
        }
        else if(index >= 8 && index < 20){
            CharacterSpriteRenderer.sprite = CharacterSprites[1];
            CharacterSpriteRenderer2.sprite = CharacterSprites2[0];
        }
        else if(index >= 20){
            CharacterSpriteRenderer.sprite = CharacterSprites[2];
            CharacterSpriteRenderer2.sprite = CharacterSprites2[1];
            button.SetActive(false);
            StartCoroutine(NextScene());
        }
    }

    void Update()
    {
        ChangeCharacter();
        // 累加自上一帧以来的时间
        timer += Time.deltaTime;  // Time.deltaTime 表示两帧之间的时长 :contentReference[oaicite:2]{index=2}

        // 判断是否达到执行间隔
        if (timer >= interval)
        {
            // 调用一次目标函数
            SubIndex();
            // 减去间隔，保留多余时间以减小误差
            timer -= interval;      // 推荐使用减法而非归零来更准确地累计时间 :contentReference[oaicite:3]{index=3}
        }
    }

    IEnumerator NextScene(){
        index = 100;
        yield return new WaitForSeconds(2f);
        scene_1.SetActive(true);
        yield return new WaitForSeconds(5f);
        scene_2.SetActive(true);


    }
}
