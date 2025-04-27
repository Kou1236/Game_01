using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArea : MonoBehaviour
{
    public Vector3 offset;
    public bool isDragging = false;
    public bool isInTargetArea = false;
    public float dragScale = 0.5f;
    public Vector3 originDragSize;
    public int index = 0;

    public virtual void Start()
    {
        originDragSize = transform.localScale;
    }


    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    public virtual void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = Input.mousePosition;
            curScreenPoint.z = 10f; // 适当的z值
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            transform.localScale = originDragSize * dragScale;
        }
    }

    public virtual void OnMouseUp()
    {
        isDragging = false;

        if (isInTargetArea)
        {
            Debug.Log("拖拽成功，执行函数！");
            ExecuteFunction();
        }
    }

    public virtual void ExecuteFunction()
    {
        // 你要执行的逻辑
        Debug.Log("函数被执行了！");
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        DragManager.Instance.isFinished[index] = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("进入了触发器！");
        if (other.CompareTag("Area"))
        {
            isInTargetArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area"))
        {
            isInTargetArea = false;
        }
    }
}
