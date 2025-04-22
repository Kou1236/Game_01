using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteMask), typeof(Collider2D))]
public class MaskBrush : MonoBehaviour 
{
    public int brushRadius = 200;
    [Range(0f, 1f)] public float completionThreshold = 0.8f;
    // public System.Action onEraseComplete;

    private Texture2D editableTex;
    private SpriteRenderer sr;
    private bool isCompleted = false;

    public bool isPost = true;

    public bool isPaper = false;

    public int index;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        // 只复制一次原贴图，后续所有擦除都作用于 editableTex
        editableTex = Instantiate(sr.sprite.texture);
        editableTex.Apply();  // :contentReference[oaicite:11]{index=11}
        sr.sprite = Sprite.Create(
            editableTex, sr.sprite.rect,
            new Vector2(0.5f, 0.5f),
            sr.sprite.pixelsPerUnit
        );
    }

    void OnMouseDrag() {
        if (isCompleted) return;
        if (this.gameObject.tag != "Post") return;
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wp.z = transform.position.z;
        EraseAtPoint(wp);
        // 可改为在 OnMouseUp 或每隔 N 帧调用
        CheckCompletion();
    }

    void EraseAtPoint(Vector2 worldPos) {
         // ① 世界坐标 → 本地坐标（受缩放影响） :contentReference[oaicite:7]{index=7}
        Vector2 localPos = transform.InverseTransformPoint(worldPos);

        // ② 精灵在本地空间下的尺寸（不含 Transform.localScale） :contentReference[oaicite:8]{index=8}
        Vector2 spriteSize = sr.sprite.bounds.size;

        // ③ 归一化到 [0,1] UV 空间
        float u = localPos.x / spriteSize.x + 0.5f;
        float v = localPos.y / spriteSize.y + 0.5f;

        // ④ 转为像素坐标
        int cx = Mathf.FloorToInt(u * editableTex.width);
        int cy = Mathf.FloorToInt(v * editableTex.height);

        // ⑤ 在圆形区域内置 alpha 为 0
        int r = brushRadius;
        for (int x = cx - r; x <= cx + r; x++) {
            for (int y = cy - r; y <= cy + r; y++) {
                if (x < 0 || x >= editableTex.width || y < 0 || y >= editableTex.height) 
                    continue;
                if ((x - cx) * (x - cx) + (y - cy) * (y - cy) > r * r) 
                    continue;

                editableTex.SetPixel(x, y, new Color(0, 0, 0, 0));  // :contentReference[oaicite:9]{index=9}
            }
        }

        // ⑥ 上传修改（保持所有“洞”持续存在） :contentReference[oaicite:10]{index=10}
        editableTex.Apply();
    }

    float CalculateTransparentRatio() {
        Color32[] pixels = editableTex.GetPixels32();  // :contentReference[oaicite:13]{index=13}
        int transparentCount = 0;
        for (int i = 0; i < pixels.Length; i++) {
            if (pixels[i].a == 0) transparentCount++;
        }
        return (float)transparentCount / pixels.Length;
    }

    void CheckCompletion() {
        if (isCompleted) return;
        float ratio = CalculateTransparentRatio();
        if (ratio >= completionThreshold) {
            isCompleted = true;
            // 一次性清空所有像素
            Color[] clearColors = new Color[editableTex.width * editableTex.height];
            for (int i = 0; i < clearColors.Length; i++)
                clearColors[i] = new Color(0,0,0,0);
            editableTex.SetPixels(clearColors);  // :contentReference[oaicite:14]{index=14}
            editableTex.Apply();
            // onEraseComplete?.Invoke();  // :contentReference[oaicite:15]{index=15}
            Debug.Log("Erase Complete!");
            if (isPost)
                FinishGame();
            if (isPaper)
                StartCoroutine(NextGame());
        }
    }

    void FinishGame(){
        PostManager.Instance.isFinished[index] = true;
        PostManager.Instance.postNum --;
        this.gameObject.SetActive(false);
    }

    IEnumerator NextGame(){
        yield return new WaitForSeconds(1.8f);
        FadeEffect fade = GetComponent<FadeEffect>();
        fade.FadeInImmediately();
    }
}
