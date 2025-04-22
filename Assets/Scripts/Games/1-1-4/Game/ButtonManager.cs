using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    [Header("References")]
    public JudgeLine judgeLine;
    public Slider progressBar;
    public SpriteRenderer characterSprite;

    [Header("Settings")]
    public float progressIncrement = 0.1f;
    public float maxProgress = 1f;
    public Sprite[] buttonSprites; // 0:A, 1:B, 2:C

    [Header("Punch Animation")]
    public float punchScale = 1.2f; // 弹跳时放大的倍数
    public float punchDuration = 0.2f; // 弹跳动画时长
    public RectTransform targetRect;

    [Header("Finish")]
    public GameObject line;
    public GameObject button;
    public GameObject slider;
    public GameObject spawner;
    public GameObject[] objectsToFade;
    public float fadeDuration = 3f;    // 淡出持续时间
    public float interval = 1f;         // 每个物体之间的间隔时间
    public GameObject finalButton;

    private Sprite originalSprite;

    private bool canPress = true;

    void Awake() {
        originalSprite = characterSprite.sprite;
    }

    void Start() => progressBar.maxValue = maxProgress;

    public void OnButtonPressed(int buttonID)
    {
        if (canPress)
        {
            // 更新角色精灵
            if (buttonID >= 0 && buttonID < buttonSprites.Length)
            {
                characterSprite.sprite = buttonSprites[buttonID];
                StartCoroutine(ResetProgress());
            }

            // 检查匹配物体
            foreach (var obj in judgeLine.currentObjects.ToArray())
            {
                ObjectType objType = obj.GetComponent<ObjectType>();
                if (objType != null && objType.typeID == buttonID)
                {
                    AddProgress(progressIncrement);
                    Destroy(obj);
                    judgeLine.currentObjects.Remove(obj);
                    break;
                }
            }
        }
    }

    void AddProgress(float amount)
    {
        progressBar.value = Mathf.Clamp(
            progressBar.value + amount, 
            0f, 
            maxProgress
        );
        ProgressBarBouncy();
        if (Mathf.Approximately(progressBar.value, 1f))
        {
            Debug.Log("进度条已满！");
            StartCoroutine(ExitScene());
        }
    }
    public void ProgressBarBouncy()
    {
        // 播放弹跳动画：先放大再恢复到原始大小
        targetRect.DOScale(punchScale, punchDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    targetRect.DOScale(1f, punchDuration * 0.5f);
                });
    }

    IEnumerator ResetProgress(){
        canPress = false;
        yield return new WaitForSeconds(0.8f);
        characterSprite.sprite = originalSprite;
        canPress = true;
    }

    IEnumerator ExitScene(){
        yield return new WaitForSeconds(1f);
        EventHandler.CallCloseEvent();
        line.SetActive(false);
        spawner.SetActive(false);
        // yield return new WaitForSeconds(1f);
        Debug.Log("游戏结束！");
        Fade();
    }

    void Fade(){
        Sequence sequence = DOTween.Sequence(); // 创建一个序列
        for (int i = 0; i < objectsToFade.Length; i++)
        {
            int index = i; // 捕获当前索引
            // 获取物体的 Renderer 组件
            Renderer renderer = objectsToFade[index].GetComponent<Renderer>();
            if (renderer != null)
            {
                float time = interval + index; // 设置动画延迟
                if(time >= 4f){
                    time = 4f;
                }
                // 获取材质的初始颜色
                Color initialColor = renderer.material.color;
                // 设置目标颜色为透明
                Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
                // 创建淡出动画，并添加到序列中
                sequence.Append(renderer.material.DOColor(targetColor, fadeDuration)
                    .SetDelay(time) // 设置延迟
                    .OnKill(() => OnFadeOutComplete())); // 动画结束时调用的回调函数
            }
            else
            {
                Debug.LogWarning(objectsToFade[index].name + " 没有 Renderer 组件！");
            }
        }
        sequence.Play(); // 播放序列
    }
    void OnFadeOutComplete(){
        CameraNotShake();
        for(int i = 0; i < objectsToFade.Length; i++){
            Destroy(objectsToFade[i]);
        }
        finalButton.SetActive(true);
    }

    void CameraNotShake(){
        CameraController.Instance.isShaking = false;
        CameraController.Instance.StopShaking();
    }

}