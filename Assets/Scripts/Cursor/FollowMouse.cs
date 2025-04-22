using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        // 1. 获取鼠标的屏幕坐标 (x, y, 0)
        Vector3 mouseScreenPos = Input.mousePosition; 
        
        // 2. 设定一下 Z 值，使其对应到摄像机前方的 2D 平面上
        //    若摄像机是默认的 z = -10，可设 z = 10；或直接用摄像机与物体的相对深度
        mouseScreenPos.z = -Camera.main.transform.position.z;
        
        // 3. 转换为世界坐标
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        // 4. 直接将物体位置替换为鼠标对应的世界坐标
        transform.position = mouseWorldPos;
    }
}
