using UnityEngine;

public class HitLine : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            // 物体进入判定线，生成图像等
            int buttonIndex = Random.Range(0, GameManagers.Instance.buttons.Length);
            GameManagers.Instance.canPressButton = true;

            // 调用按钮的点击事件
            GameManagers.Instance.OnButtonPress(buttonIndex);
        }
    }
}
