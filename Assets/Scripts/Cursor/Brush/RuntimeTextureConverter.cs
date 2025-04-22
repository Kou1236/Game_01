using UnityEngine;

public class RuntimeTextureConverter {
    /// <summary>
    /// 将任意格式的 Texture2D 转换为可读写的 RGBA32 纹理。
    /// </summary>
    public static Texture2D ToReadableRGBA(Texture2D src) {
        // 1. 创建 RGBA32 格式的临时 RenderTexture
        var rt = RenderTexture.GetTemporary(
            src.width, src.height, 0,
            RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear
        );
        // 2. 将源纹理绘制到 RT
        Graphics.Blit(src, rt);                                            // :contentReference[oaicite:5]{index=5}

        // 3. 读取 RT 到新的 Texture2D（RGBA32）
        var prev = RenderTexture.active;
        RenderTexture.active = rt;
        var dst = new Texture2D(src.width, src.height, TextureFormat.RGBA32, false);
        dst.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0, false);  // :contentReference[oaicite:6]{index=6}
        dst.Apply();
        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(rt);

        return dst;
    }
}
