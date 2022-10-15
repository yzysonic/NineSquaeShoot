Shader "Unlit/GaussianBlurShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            static const int samplingCount = 10;
            sampler2D _MainTex;
            half _Weights[samplingCount];
            half4 _Offsets;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                half4 col = 0;
                int j = 0;

                [unroll]
                for (j = samplingCount - 1; j > 0; j--)
                {
                    col += tex2D(_MainTex, i.uv - (_Offsets.xy * j)) * _Weights[j];
                }

                [unroll]
                for (j = 0; j < samplingCount; j++)
                {
                    col += tex2D(_MainTex, i.uv + (_Offsets.xy * j)) * _Weights[j];
                }

                return col;
            }
            ENDCG
        }
    }
}
