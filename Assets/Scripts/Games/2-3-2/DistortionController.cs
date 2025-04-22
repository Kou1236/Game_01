using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistortionController : Singleton<DistortionController>, IPointerUpHandler
{
    [Header("引用使用扭曲Shader的材质")]
    public Material targetMaterial;
    
    [Header("控制扭曲效果（左右偏离中点增大）的Slider")]
    public Slider distortionSlider;

    public float startValue = 0.3f;
    public float endValue = 0.7f;



    void Start()
    {
        if (distortionSlider != null)
        // 设置初始值为 1/3
            distortionSlider.value = startValue;
            endValue = Random.Range(startValue + 0.07f, 0.9f);
            
            distortionSlider.onValueChanged.AddListener(UpdateDistortionAmount);
            targetMaterial.SetFloat("_DistortionAmount", 0.8f);

    }
    void Update(){
        if(distortionSlider.value >= endValue-0.01f && distortionSlider.value <= endValue+0.01f){
            FinishGame.Instance.finish_2 = true;
        }
        else{
            FinishGame.Instance.finish_2 = false;
        }
    }

    // 鼠标放开（或触摸结束）时调用，实现“鼠标放开判定”
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("鼠标放开进度条");
        // 此处可添加需要在拖动结束时执行的逻辑，比如播放动画到具体帧等
        EventHandler.CallFinishGameEvent();
    }

    // 将 Slider 的值转换后更新 _DistortionAmount 参数
    void UpdateDistortionAmount(float value)
    {
        float adjustedValue = 0f;

        if (value < startValue)
        {
            // 左侧：value 越小，扭曲减小，但最小为 0.2
            adjustedValue = Mathf.Lerp(0.1f, 1.0f, value / startValue);
        }
        else if (value <= endValue){
            adjustedValue = Mathf.Lerp(1.0f, 0f, (value - startValue) / (endValue - startValue));
        }
        else
        {
            // 右侧：value 超过 0.7 后，扭曲从 0 开始增大
            adjustedValue = Mathf.Lerp(0f, 1.0f, (value - endValue) / (1f - endValue));
        }
        if (targetMaterial != null)
            targetMaterial.SetFloat("_DistortionAmount", adjustedValue);
    }

    // 直接将 Slider 值更新 _DistortionSpeed 参数
    void UpdateDistortionSpeed(float value)
    {
        if (targetMaterial != null)
            targetMaterial.SetFloat("_DistortionSpeed", value);
    }
}
