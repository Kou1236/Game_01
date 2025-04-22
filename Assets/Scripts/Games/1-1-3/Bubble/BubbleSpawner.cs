using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BubbleSpawner : MonoBehaviour
{
    [Header("气泡预制体")]
    public GameObject bubblePrefab;       // UI气泡预制体，需要有 RectTransform 与 Image 组件

    [Header("生成设置")]
    public int maxBubbleCount = 10;         // 同时存在的最大气泡数量
    public float spawnInterval = 1f;      // 生成气泡的时间间隔（秒）
    // 生成区域坐标（以容器的 RectTransform 为参考）
    public Vector2 spawnAreaMin = new Vector2(200, -200);
    public Vector2 spawnAreaMax = new Vector2(500, 200);

    [Header("动画设置")]
    public float scaleFrom = 0.5f;          // 气泡初始缩放
    public float scaleTo = 1f;              // 气泡放大后的缩放
    public float scaleDuration = 0.5f;      // 缩放动画时长（秒）
    public float moveUpDistance = 100f;     // 向上移动的距离（UI单位）
    public float moveDuration = 3f;         // 向上移动的时长（秒）
    public float fadeDuration = 3f;         // 渐渐淡出的时长（秒）

    void OnEnable()
    {
        // 定时生成气泡
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        // 控制同时存在的气泡数量，避免过多
        if(transform.childCount >= maxBubbleCount)
            return;

        // 在指定区域内随机生成位置（以当前容器的 RectTransform 为参考）
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPos = new Vector2(randomX, randomY);

        // 实例化气泡，并将其设为当前对象（容器）的子物体
        GameObject bubble = Instantiate(bubblePrefab, transform);
        // 获取气泡的 RectTransform，设置其 anchoredPosition
        RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
        bubbleRect.anchoredPosition = spawnPos;

        // 初始缩放设置，并用 DOTween 实现弹性放大效果
        bubbleRect.localScale = Vector3.one * scaleFrom;
        bubbleRect.DOScale(scaleTo, scaleDuration).SetEase(Ease.OutBack);

        // 向上移动动画：调整 anchoredPosition.y
        bubbleRect.DOAnchorPosY(bubbleRect.anchoredPosition.y + moveUpDistance, moveDuration)
                  .SetEase(Ease.InOutSine);

        // 淡出动画：利用 Image 组件将透明度从1淡到0
        Image img = bubble.GetComponent<Image>();
        if(img != null)
        {
            img.DOFade(0f, fadeDuration).SetEase(Ease.InOutSine)
               .OnComplete(() => Destroy(bubble));
        }
        else
        {
            Destroy(bubble, moveDuration);
        }
    }
}
