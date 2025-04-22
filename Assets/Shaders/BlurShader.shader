Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_TexelSize; // 每个像素的尺寸 (1/width, 1/height)
            float _BlurAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 offset = _MainTex_TexelSize.xy * _BlurAmount;
                fixed4 col = tex2D(_MainTex, i.uv) * 0.2;
                col += tex2D(_MainTex, i.uv + float2(offset.x, 0)) * 0.2;
                col += tex2D(_MainTex, i.uv - float2(offset.x, 0)) * 0.2;
                col += tex2D(_MainTex, i.uv + float2(0, offset.y)) * 0.2;
                col += tex2D(_MainTex, i.uv - float2(0, offset.y)) * 0.2;
                return col;
            }
            ENDCG
        }
    }
}
