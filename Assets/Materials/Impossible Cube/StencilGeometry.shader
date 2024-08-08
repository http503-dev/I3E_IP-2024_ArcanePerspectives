/*
 * Author: Muhammad Farhan
 * Date: 16/7/24
 * Description: Script for shader for geometry that shows only through stencil buffer mask
 */
Shader "Unlit/StencilGeometry"
{
    Properties
    {
        _StencilMask("Stencil Mask", Int) = 0
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 200

        Stencil
        {
            Ref [_StencilMask]
            Comp Equal
            Pass Keep
            Fail Keep
        }

        Pass
        {
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            half4 _Color;
            half _Glossiness;
            half _Metallic;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 col = _Color;
                return col;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
