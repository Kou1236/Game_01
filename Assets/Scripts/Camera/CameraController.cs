using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraController : Singleton<CameraController>
{
    [Header("平移动画设置")]
    public Transform target;         // 初始目标，可为空
    public float moveDuration = 2f;

    public float moveYDuration = 4f;

    [Header("虚化动画设置")]
    public float maxBlur = 5f;        // 中间时达到的最大虚化值
    // 初始和目标虚化均为 0，因此我们只用一个当前虚化变量
    private float currentBlur = 0f;
    [Header("材质")]
    public Material blurMaterial;     // 使用自定义 BlurShader 的材质


    public bool isShaking = false;  // 是否处于抖动状态


    void Start(){
        if (blurMaterial == null)
        {
            Debug.LogError("请在 Inspector 中指定 Blur Material！");
            return;
        }
        // 初始化虚化参数为 0
        blurMaterial.SetFloat("_BlurAmount", 0f);
    }


    // 提供一个接口供其他场景设置目标对象
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // 执行平滑移动和旋转到目标位置
    // public void MoveToTarget()
    // {
    //     if (target != null)
    //     {
    //         transform.DOMove(target.position, moveDuration).SetEase(Ease.InOutQuad);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("目标对象未设置！");
    //     }
    // }


    public void MoveToTarget(){
        Sequence mainSeq = DOTween.Sequence();

        // 创建虚化动画序列：0 -> maxBlur -> 0
        Sequence blurSeq = DOTween.Sequence();
        blurSeq.Append(DOTween.To(() => currentBlur, x => {
            currentBlur = x;
            blurMaterial.SetFloat("_BlurAmount", x);
        }, maxBlur, moveDuration / 2f));
        blurSeq.Append(DOTween.To(() => currentBlur, x => {
            currentBlur = x;
            blurMaterial.SetFloat("_BlurAmount", x);
        }, 0, moveDuration / 2f));

        // 平移动画：摄像机从当前移动到 targetPosition
        mainSeq.Append(transform.DOMove(target.position, moveDuration));

        // 同时执行虚化动画
        mainSeq.Join(blurSeq);

        mainSeq.OnComplete(() => {
            FinishMove();
        });

        mainSeq.Play();

    }

    // 摄像机后处理函数，将渲染结果通过 BlurMaterial 处理
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (blurMaterial != null)
            Graphics.Blit(src, dest, blurMaterial);
        else
            Graphics.Blit(src, dest);
    }

    void FinishMove(){
        StartCoroutine(FinishCoroutine());
    }


    IEnumerator FinishCoroutine()
    {   
        yield return new WaitForSeconds(0.2f);
        EventHandler.CallSceneTransitionEvent();
        if(isShaking)
            Shake();
        else
            StopShaking();
    }

    public void Shake(){
        Debug.Log("开始抖动");
        ShakeCamera.Instance.ShakeLayer();
    }

    public void StopShaking(){
        Debug.Log("停止抖动");
        ShakeCamera.Instance.StopShake();
    }

    public void MoveY(float y, float duration){
        transform.DOMoveY(y, duration).SetEase(Ease.InOutQuad);
    }

    public void MoveX(float x, float duration){
        transform.DOMoveX(x, duration).SetEase(Ease.InOutQuad);
    }

    public void ChangeSize(float size, float duration){
        Camera mainCam = Camera.main;
        if (mainCam.orthographic)
        {
            mainCam.DOOrthoSize(size, duration).SetEase(Ease.InOutSine);
        }
        else
        {
            Debug.LogWarning("当前相机不是正交模式，请检查相机设置！");
        }
    }

}
