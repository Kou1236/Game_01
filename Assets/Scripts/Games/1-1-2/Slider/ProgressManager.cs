using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ProgressManager : Singleton<ProgressManager>
{

    [Header("进度条 UI")]
    public Slider progressSlider;         // 引用场景中的 Slider

    [Header("弹窗数量")]
    public int totalPopups;           // 预计总共会关闭的弹窗数量

    [Header("动画设置")]
    public float punchScale = 1.2f; // 弹跳时放大的倍数
    public float punchDuration = 0.2f; // 弹跳动画时长

    [Header("结束场景")]
    public GameObject obj;
    public GameObject background;
    public float duration = 2f;
    public GameObject finalButton;
    public float finalScale = 1f;

    private Vector3 targetScale;

    private float progressIncrement;      // 每个弹窗关闭增加的进度值
    
    public RectTransform targetRect;


    override protected void Awake()
    {
        base.Awake();
        totalPopups = Random.Range(18, 24);
        targetScale = new Vector3(finalScale, finalScale, 1f);
    }

    void Start()
    {
        
        if (progressSlider != null)
        {
            progressSlider.value = 0;
        }
        // 每关闭一个弹窗，进度条增加的比例
        progressIncrement = 1f / totalPopups;
    }

    // 供弹窗关闭时调用，增加进度
    public void AddProgress()
    {
        if (progressSlider != null)
        {
            progressSlider.value += progressIncrement;
            ProgressBarBouncy();
            if (Mathf.Approximately(progressSlider.value, 1f))
            {
                Debug.Log("进度条已满！");
                StartCoroutine(CloseSlider());
            }
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

    IEnumerator CloseSlider(){
        yield return new WaitForSeconds(0.6f);
        progressSlider.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(true);
    }

    public void SmallBackground(){
        StartCoroutine(SmallBackgroundCoroutine());
    }

    IEnumerator SmallBackgroundCoroutine(){
        background.transform.DOScale(targetScale, duration);
        yield return new WaitForSeconds(2.5f);
        finalButton.SetActive(true);
    }

}
