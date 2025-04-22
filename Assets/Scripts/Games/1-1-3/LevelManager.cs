using UnityEngine;
using System.Collections;

public class LevelManager : Singleton<LevelManager>
{
    
    [Header("UI设置")]
    public CountdownTimer timeBarController; // 时间条控制器引用
    public GameObject hands;
    public GameObject bubbles;
    public GameObject target;


    /// <summary>
    /// 当拾取物品后调用，检查场景中剩余物品是否为0
    /// </summary>
    public void ItemCollected()
    {
        int count = transform.childCount;
        Debug.Log("剩余物品数量：" + count);
        if (count < 1)
        {
            Debug.Log("通关！");
            if (timeBarController != null)
            {
                timeBarController.gameObject.SetActive(false);
            }
            if (hands != null)
            {
                hands.SetActive(false);
            }
            if (target != null)
            {
                target.SetActive(true);
                StartCoroutine(MoveCoroutine());
            }
            if (bubbles != null)
            {
                Destroy(bubbles);
            }
            // 这里还可添加其他通关逻辑（如播放音效、加载下一关等）
        }
    }

    IEnumerator MoveCoroutine(){
        yield return new WaitForSeconds(0.1f);
        var trans = target.GetComponent<Transition_4>();
        trans.Move();
    }
}
