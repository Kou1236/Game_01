using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour
{
    [Header("对象引用")]
    public Transform hand;            // 手对象（Sprite Pivot设置在固定端）
    public Transform pivotPoint;      // 旋转中心，通常为一个空物体，手作为其子物体

    [Header("旋转设置")]
    public float rotationSpeed = 60f;   // 每秒旋转角度
    public float minAngle = -60f;       // 最小旋转角度
    public float maxAngle = 60f;        // 最大旋转角度
    private float currentAngle;         // 当前旋转角度
    private int angleDirection = 1;     // 旋转方向：1 表示递增，-1 表示递减

    [Header("伸缩设置")]
    public float extendSpeed = 2f;      // 拉长速度（缩放因子/秒）
    public float targetScaleX = 3f;     // 目标 x 轴缩放值（原始一般为1，只向一侧拉长）
    public float retractSpeed = 2f;     // 缩回基础速度（缩放因子/秒）
    private Vector3 originalScale;      // 手的初始缩放

    // 状态管理
    private enum State { Rotating, Extending, Retracting }
    private State currentState = State.Rotating;
    private bool isAnimating = false;   // 标识伸缩动画是否正在执行
    private Tween currentTween;

    // 抓取物品相关
    private GameObject grabbedItem;
    private float currentItemWeight = 1f;  // 默认物品重量

    private bool isCutdown_1 = false;
    private bool isCutdown_2 = false;


    void Start()
    {
        // 记录初始缩放值和初始旋转角度
        originalScale = hand.localScale;
        currentAngle = minAngle;
        pivotPoint.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void Update()
    {
        if(CountdownTimer.Instance.countdownValue < 5f && isCutdown_1 == false){
            rotationSpeed = rotationSpeed/1.3f;
            extendSpeed = extendSpeed/1.3f;
            retractSpeed = retractSpeed/1.3f;
            isCutdown_1 = true;
        }
        if(CountdownTimer.Instance.countdownValue < 1f && isCutdown_2 == false){
            rotationSpeed = rotationSpeed/1.6f;
            extendSpeed = extendSpeed/1.6f;
            retractSpeed = retractSpeed/1.6f;
            isCutdown_2 = true;
        }
        // 只有在旋转状态下允许点击和旋转更新
        if (currentState == State.Rotating)
        {
            UpdateRotation();
            if (Input.GetMouseButtonDown(0) && CursorBlock.AllowMouseButtonInput)
            {
                StartExtend();
            }
        }
    }

    /// <summary>
    /// 按照当前角度按增量更新旋转，实现连续旋转
    /// </summary>
    void UpdateRotation()
    {
        currentAngle += angleDirection * rotationSpeed * Time.deltaTime;
        // 到达极限角度时反转方向
        if (currentAngle >= maxAngle)
        {
            currentAngle = maxAngle;
            angleDirection = -1;
        }
        else if (currentAngle <= minAngle)
        {
            currentAngle = minAngle;
            angleDirection = 1;
        }
        pivotPoint.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    /// <summary>
    /// 开始拉长动画（只改变 x 轴缩放）
    /// </summary>
    void StartExtend()
    {
        if (isAnimating) return;  // 防止重复触发
        isAnimating = true;
        currentState = State.Extending;
        // 计算伸展差值与动画时长
        float scaleDiff = targetScaleX - hand.localScale.x;
        float duration = scaleDiff / extendSpeed;
        currentTween = hand.DOScaleX(targetScaleX, duration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 如果伸展完毕且未抓取物品，则启动缩回
                StartRetract();
            });
    }

    /// <summary>
    /// 开始缩回动画，若抓取了物品，则缩回速度受物品重量影响
    /// </summary>
    void StartRetract()
    {
        currentState = State.Retracting;
        float scaleDiff = hand.localScale.x - originalScale.x;
        // 如果抓取了物品，缩回速度按重量衰减（重量越大，缩回越慢）
        float actualRetractSpeed = grabbedItem != null ? retractSpeed / currentItemWeight : retractSpeed;
        float duration = scaleDiff / actualRetractSpeed;
        Debug.Log("Start retract: " + duration);
        currentTween = hand.DOScaleX(originalScale.x, duration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 缩回到原始状态后，处理抓取的物品（如通知关卡管理器、销毁物品等）
                if (grabbedItem != null)
                {
                    Destroy(grabbedItem);
                    grabbedItem = null;
                    currentItemWeight = 1f;
                    LevelManager.Instance.ItemCollected();
                }
                // 恢复状态，继续旋转
                currentState = State.Rotating;
                isAnimating = false;
            });
    }



    /// <summary>
    /// 伸展过程中检测碰撞，抓取物品
    /// </summary>
    /// <param name="other">碰撞体</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello");
        if (currentState == State.Extending && other.CompareTag("Item"))
        {
            Debug.Log("Grabbed item: " + other.gameObject.name);
            grabbedItem = other.gameObject;
            // 尝试获取物品的 Item 脚本，读取重量属性
            Item itemScript = grabbedItem.GetComponent<Item>();
            currentItemWeight = itemScript != null ? itemScript.weight : 1f;
            Debug.Log("Item weight: " + currentItemWeight);
            // 将物品设置为手的子对象，使其随手一起运动
            grabbedItem.transform.SetParent(hand);
            // 若正在执行伸展动画，则终止并启动缩回
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill();
            }
            Debug.Log("Start retract");
            StartRetract();
            Debug.Log("Retracted");
        }
    }
}
