using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LaunchEffect : MonoBehaviour
{
    [Header("设置部分")]
    public Button launchButton;             // 按钮引用
    public GameObject projectile;           // 发射的物体
    public float launchHeight = 5f;         // 向上运动的高度
    public float moveDuration = 1f;         // 向上运动时长
    public float fadeDuration = 1f;         // 渐变消失时长

    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        // 获取 SpriteRenderer 组件
        spriteRenderer = projectile.GetComponent<SpriteRenderer>();

        // 绑定按钮点击事件
        if (launchButton != null)
            launchButton.onClick.AddListener(Launch);
    }

    void Launch()
    {
        // 重置物体颜色（确保不透明）
        Color startColor = spriteRenderer.color;
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // 如果需要，可以将物体的位置重置到初始位置
        // projectile.transform.position = new Vector3(initialX, initialY, initialZ);

        // 创建 DOTween 序列
        Sequence seq = DOTween.Sequence();
        // 第一阶段：向上运动
        seq.Append(projectile.transform.DOMoveY(projectile.transform.position.y + launchHeight, moveDuration)
                 .SetEase(Ease.OutQuad));
        // 第二阶段：渐变消失
        seq.Append(spriteRenderer.DOFade(0f, fadeDuration));

        // 可选：当动画完成后，可以销毁物体或执行其他逻辑
        seq.OnComplete(() => {
            // Destroy(projectile);
            Debug.Log("发射动画完成");
        });
    }
}
