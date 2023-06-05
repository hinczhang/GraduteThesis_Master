// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Border Shader"
{
   
        Properties{
            _Color("Main Color", Color) = (1,1,1,1)
            _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
            _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
            _EmissiveAmount("Emissive Amount", Range(0,1)) = 0.5
            _Outline("Outline Thickness", Range(0,10)) = 0.0
        }

            SubShader{
                Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
                LOD 200

            CGPROGRAM
            #pragma surface surf Lambert alphatest:_Cutoff

            sampler2D _MainTex;
            fixed4 _Color;
            float _EmissiveAmount;
            float _Outline;
            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;

                o.Emission = c.rgb * _EmissiveAmount;
            }
            ENDCG
            }

                Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
