using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSize : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        // 缓存 Image 引用
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        // 组件启用时，调整到 Native Size
        _image.SetNativeSize();
    }
}
