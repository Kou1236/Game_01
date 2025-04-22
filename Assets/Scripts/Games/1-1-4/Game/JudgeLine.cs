using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public Sprite[] hitEffectSprites; // 3种命中特效的Sprite

    public Sprite[] spawnerSprites; // 3种生成特效的Sprite
    
    public SpriteRenderer effectSpriteRenderer; // 已存在的SpriteRenderer物体

    [HideInInspector]
    public List<GameObject> currentObjects = new List<GameObject>();

    private Sprite originalSprite; // 原始Sprite

    void Awake(){
        originalSprite = effectSpriteRenderer.sprite; // 保存原始Sprite
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("MovableObject")) return;

        currentObjects.Add(other.gameObject);
        SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>();
        otherSpriteRenderer.sprite = spawnerSprites[other.GetComponent<ObjectType>().typeID]; // 修改物体的SpriteRenderer的sprite为对应的生成特效
        SpawnHitEffect(other.GetComponent<ObjectType>().typeID);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("MovableObject")) return;

        currentObjects.Remove(other.gameObject);
        ClearHitEffect(); // 物体退出时清除特效
    }

    void SpawnHitEffect(int typeID)
    {
        if (typeID >= 0 && typeID < hitEffectSprites.Length)
        {
            // 修改现有物体的SpriteRenderer的sprite为对应的命中特效
            effectSpriteRenderer.sprite = hitEffectSprites[typeID];
            effectSpriteRenderer.gameObject.SetActive(true); // 确保特效物体可见
        }
    }

    void ClearHitEffect()
    {
        // 清除现有的特效Sprite，将SpriteRenderer的sprite设置为空，或者禁用它
        effectSpriteRenderer.sprite = originalSprite;
    }
}
