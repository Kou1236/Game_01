using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragRotateX : DragRotate
{

    public Sprite changeSprite;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private bool canDrag = true;
    public float duration = 2f;

    public GameObject next;

    public override void Start()
    {
        // 启动时，记录初始角度
        _initialAngle = transform.eulerAngles.x;
        _currentAngle = _initialAngle;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }
    

    public override void Update()
    {
        // 如果不在拖拽中，平滑地将物体转回初始角度
        if (!_isDragging)
        {
            float x = transform.eulerAngles.x;
            float newX = Mathf.MoveTowardsAngle(x, _initialAngle, returnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(newX, 0f, 0f);
            // 同步 _currentAngle，以免再次拖拽时产生突然跳变
            _currentAngle = newX;
        }
        float angle = transform.rotation.x;  
        Debug.Log("Angle: " + angle);
        if (Mathf.Abs(angle) >= 0.7f)  
        {  
            ExecuteAction();  
        }
        else{
            spriteRenderer.sprite = originalSprite;
        }
    }

    public override void OnMouseDrag()
    {
        if (!canDrag) return;
        // 计算鼠标 Y 位移并转为角度增量
        Vector3 mousePos = Input.mousePosition;
        float deltaY = mousePos.y - _prevMousePos.y;
        _prevMousePos = mousePos;

        float deltaAngle = deltaY * rotationSpeed;
        _currentAngle = Mathf.Clamp(_currentAngle + deltaAngle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(_currentAngle, 0f, 0f);
    }

    public override void OnMouseUp(){
        _isDragging = false;
        float angle = transform.rotation.x;  
        if(Mathf.Abs(angle) >= 0.7f){
            _isDragging = true;
            canDrag = false;
            Finish();
        }
    }
    
    void ExecuteAction()
    {
        spriteRenderer.sprite = changeSprite;
    }

    void Finish(){
        transform.DOLocalRotate(new Vector3(180f, 0f, 0f), duration).OnComplete(() => {
            next.SetActive(true);
        });

    }


}
