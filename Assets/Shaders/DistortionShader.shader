Shader "Custom/DistortionShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {} 
        _DistortionAmount ("Distortion Amount", Range(0,1)) = 0.0
        _DistortionSpeed ("Distortion Speed", Range(0,5)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DistortionAmount;
            float _DistortionSpeed;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                // 扭曲效果：根据 _DistortionAmount 调整幅度，并通过 _DistortionSpeed 调节速度
                float2 distortedUV = i.uv + _DistortionAmount * 0.05 * 
                                     float2(sin(i.uv.y * 10.0 + _Time.y * _DistortionSpeed * 10.0), 
                                            cos(i.uv.x * 10.0 + _Time.y * _DistortionSpeed * 10.0));
                fixed4 col = tex2D(_MainTex, distortedUV);
                return col;
            }
            ENDCG
        }
    }
}
