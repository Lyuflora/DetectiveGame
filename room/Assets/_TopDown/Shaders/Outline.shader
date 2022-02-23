Shader "TD/Outline"
{

    Properties{

        _Color("Color", Color) = (1, 1, 1, 1)
        _Glossiness("Smoothness", Range(0, 1)) = 0
        _Metallic("Metallic", Range(0, 1)) = 0.8
        _MainTex("Texture", 2D) = "white" { }
        _OutlineColor("Outline Color", Color) = (0.04, 0.72, 0.6, 1)
        _OutlineWidth("Outline Width", Range(0, 0.2)) = 0.0

    }

        Subshader{

            Tags {
                "RenderType" = "Opaque"
            }

            CGPROGRAM

            #pragma surface surf Standard fullforwardshadows

            struct Input {
                float4 color : COLOR;
                float2 uv_MainTex;
            };
            
            sampler2D _MainTex;
            half4 _Color;
            half _Glossiness;
            half _Metallic;

            void surf(Input IN, inout SurfaceOutputStandard o) {
                //fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
                //o.Albedo = tex.rgb * IN.color.rgb;
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Smoothness = _Glossiness;
                o.Metallic = _Metallic;
                o.Alpha =  IN.color.a;
            }

            ENDCG

            Pass {

                Cull Front

                CGPROGRAM

                #pragma vertex VertexProgram
                #pragma fragment FragmentProgram

                half _OutlineWidth;

                float4 VertexProgram(float4 position : POSITION, float3 normal : NORMAL) : SV_POSITION{

                    float4 clipPosition = UnityObjectToClipPos(position);
                    float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));

                    //clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;
                    float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;
                    clipPosition.xy += offset;
                    return clipPosition;

                }

                half4 _OutlineColor;

                half4 FragmentProgram() : SV_TARGET {
                    return _OutlineColor;
                }

                ENDCG

            }

    }

}