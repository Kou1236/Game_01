using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManagers : Singleton<GameManagers>
{
    public SpriteRenderer characterImage;  // 主角图片
    public Button[] buttons;  // 3个按钮
    public GameObject objectPrefab;  // 物体Prefab
    public Transform spawnPoint;  // 物体生成位置
    public RectTransform progressBar;  // 进度条
    public Sprite[] characterSprites;  // 不同的主角图片
    public GameObject[] indicatorImages;  // 对应按键按下时生成的图像

    private float progress = 0f;  // 进度条
    private float progressSpeed = 0.01f;  // 进度增加的速度
    private float spawnInterval = 2f;  // 物体生成间隔时间
    public bool canPressButton = true;  // 控制按钮按下的状态

    void OnEnable()
    {
        // 初始化按钮事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPress(index));
        }

        // 启动生成物体的协程
        StartCoroutine(SpawnObjects());
    }

    // 生成物体
    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject obj = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
            float randomY = Random.Range(-1f, 1f);  // 正弦函数的随机偏移
            obj.GetComponent<ObjectController>().SetMovement(randomY);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 按钮按下事件
    public void OnButtonPress(int index)
    {
        if (canPressButton)
        {
            // 切换主角图片
            characterImage.sprite = characterSprites[index];

            // 增加进度条
            progress += progressSpeed;
            if (progress > 1f) progress = 1f;

            progressBar.localScale = new Vector3(progress, 1f, 1f);

            // 在主角左侧生成对应的图像
            Instantiate(indicatorImages[index], characterImage.transform.position + new Vector3(-100, 0, 0), Quaternion.identity);
        }
    }
}
