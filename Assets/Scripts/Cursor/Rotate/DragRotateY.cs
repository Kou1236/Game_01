using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragRotateY : DragRotate
{

    private bool canDrag = true;
    public float duration = 2f;
    public GameObject obj_1;
    public GameObject obj_2;
    public GameObject obj_3;
    public GameObject obj_4;


    public override void Start()
    {
        // 启动时，记录初始角度
        _initialAngle = transform.eulerAngles.y;
        _currentAngle = _initialAngle;
    }
    

    public override void Update()
    {
        // 如果不在拖拽中，平滑地将物体转回初始角度
        if (!_isDragging)
        {
            float y = transform.eulerAngles.y;
            float newY = Mathf.MoveTowardsAngle(y, _initialAngle, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, newY, 0f);
            // 同步 _currentAngle，以免再次拖拽时产生突然跳变
            _currentAngle = newY;
        }
        float angle = transform.rotation.y;  
        if (Mathf.Abs(angle) >= 0.7f)  
        {  
            ExecuteAction();  
        }
        else{
            obj_2.SetActive(false);
        }
    }

    public override void OnMouseDrag()
    {
        if (!canDrag) return;
        obj_1.SetActive(false);
        // 计算鼠标 Y 位移并转为角度增量
        Vector3 mousePos = Input.mousePosition;
        float deltaX = mousePos.x - _prevMousePos.x;
        _prevMousePos = mousePos;

        float deltaAngle = deltaX * rotationSpeed;
        _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }

    public override void OnMouseUp(){
        _isDragging = false;
        float angle = transform.rotation.y;  
        if(Mathf.Abs(angle) >= 0.7f){
            _isDragging = true;
            canDrag = false;
            Finish();
        }
    }
    
    void ExecuteAction()
    {
        obj_2.SetActive(true);
    }

    void Finish(){
        transform.DOLocalRotate(new Vector3(0f, 180f, 0f), duration).OnComplete(() => {
            obj_3.SetActive(false);
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            this.GetComponent<Collider2D>().enabled = false;
            obj_4.GetComponent<Collider2D>().enabled = true;
            obj_4.SetActive(true);
            this.gameObject.SetActive(false);
        });

    }


}
