using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour, IPointerUpHandler
{
    [Header("Animator 控件，负责控制动画播放")]
    public Animator animator;
    
    [Header("进度条，控制动画播放进度")]
    public Slider progressSlider;
    
    // 动画状态名称，确保与 Animator Controller 中的一致
    public string animationStateName = "Block";

    public float startValue = 0.8f;

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = 1;
            progressSlider.value = startValue;
            animator.Play(animationStateName, 0, startValue);
            // 立即更新动画状态
            animator.Update(0);
            // 注册 Slider 数值变化事件
            progressSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        // 冻结动画自动播放
        if (animator != null)
            animator.speed = 0;
    }

    void Update(){
        if(progressSlider.value >= 0.3f && progressSlider.value <= 0.35f){
            FinishGame.Instance.finish_1 = true;
        }
        else{
            FinishGame.Instance.finish_1 = false;
        }
    }
     // 鼠标放开（或触摸结束）时调用，实现“鼠标放开判定”
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("鼠标放开进度条");
        // 此处可添加需要在拖动结束时执行的逻辑，比如播放动画到具体帧等
        EventHandler.CallFinishGameEvent();
    }

    // Slider 数值变化时调用
    void OnSliderValueChanged(float value)
    {
        if (animator != null)
        {
            // 使用 Play 方法传入 normalizedTime 实现动画进度控制
            // 第三个参数 value 代表动画播放的归一化时间
            animator.Play(animationStateName, 0, value);
            // 立即更新动画状态
            animator.Update(0);
        }
    }
}
