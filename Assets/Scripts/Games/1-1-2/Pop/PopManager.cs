using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopManager : Singleton<PopManager>
{
    [Header("弹窗设置")]
    public GameObject popupWindows;      // 弹窗预制体
    public GameObject popupContent;     // 弹窗内容预制体
    public Vector2 positionRange = new Vector2(80, 20); // 位置偏移范围

    [Header("父物体引用")]
    public RectTransform parentCanvas;  // 用于挂载弹窗的 Canvas 或 UI 父物体

    private int popCount;

    private int prepopCount = 3;

    private int popContentCount;

    override protected void Awake()
    {
        base.Awake();
        popCount = ProgressManager.Instance.totalPopups;
        popContentCount = Random.Range(3, 5);
    }


    public void StartPop()
    {   
        for(int i = 0; i < prepopCount; i++){
            StartCoroutine(SpawnPopupWindowsCoroutine(1));
        }
        StartCoroutine(SpawnPopupWindowsCoroutine(popCount - prepopCount - popContentCount));
        StartCoroutine(SpawnPopupContentCoroutine(popContentCount));
    }

    IEnumerator SpawnPopupWindowsCoroutine(int count)
    {   
        for(int i = 0; i < count; i++){
            SpawnWindowsPopup();
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

    IEnumerator SpawnPopupContentCoroutine(int count){
        for(int i = 0; i < count; i++){
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            SpawnContentPopup();
        }
    }

    void SpawnWindowsPopup()
    {
        // 在 Canvas 下实例化弹窗预制体
        GameObject popup = Instantiate(popupWindows, parentCanvas);
        // 获取弹窗的 RectTransform 并随机定位（以 Canvas 中心为原点）
        RectTransform rt = popup.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(
            Random.Range(-positionRange.x, positionRange.x),
            Random.Range(-100, 10)
        );
    }

    void SpawnContentPopup(){
        GameObject popup = Instantiate(popupContent, parentCanvas);
    }
}
