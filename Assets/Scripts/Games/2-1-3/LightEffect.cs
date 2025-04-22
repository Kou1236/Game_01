using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightEffect : Singleton<LightEffect>
{
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    public float initialHeight; // 物体原始高度
    public GameObject lightPrefab; // 光源
    public float PrefabScale = 3f; // 光源缩放比例
    private bool isLightOn = false; // 光源开关

    public bool canLight = false; // 是否可以点亮

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = null;
        
        // initialHeight = spriteRenderer.bounds.size.y;
    }

    void Update()
    {
        if(!canLight){
            spriteRenderer.sprite = null;
            isLightOn = false;
            lightPrefab.SetActive(false);
        }
        // Input.GetMouseButtonDown(0) && 
        if (canLight)
        {
            spriteRenderer.sprite = originalSprite;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            
            if(!isLightOn){
                // 实例化光源
                lightPrefab.SetActive(true);
                isLightOn = true;
            }

            lightPrefab.transform.position = mouseWorldPos;

            

            Vector3 direction = mouseWorldPos - transform.position;
            float targetDistance = direction.magnitude;

            // 计算需要额外伸长的长度
            float extraLength = targetDistance - initialHeight;
            extraLength = Mathf.Max(0, extraLength); // 防止缩放为负数

            // 计算新的 y 轴缩放比例
            float newScaleY = 1 + (extraLength / initialHeight);
            Vector3 newScale = transform.localScale;
            newScale.y = newScaleY;
            transform.localScale = newScale;

            // 设置旋转角度，使物体指向鼠标点击位置
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            // transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.DOLocalRotate(new Vector3(0, 0, angle), 0.1f).SetEase(Ease.OutCubic);

            // DOVirtual.DelayedCall(0.5f, () =>
            // {
            //     spriteRenderer.sprite = null;
            // });

        }
    }
}
