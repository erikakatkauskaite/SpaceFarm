Shader "Unlit/GroundDeform"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}

    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 100
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            struct Attributes
            {
                float4 vertex   : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 vertex  : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // To make the Unity shader SRP Batcher compatible, declare all
            // properties related to a Material in a a single CBUFFER block with 
            // the name UnityPerMaterial.
            CBUFFER_START(UnityPerMaterial)
                // The following line declares the _BaseColor variable, so that you
                // can use it in the fragment shader.
                half4 _BaseColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.vertex = TransformObjectToHClip(IN.vertex.xyz);
                float3 worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
                if (IN.vertex.z > 3.5)
                {
                    OUT.vertex.y += sin(worldPos.z);
                    //OUT.vertex.y += cos(worldPos.z);
                }

                //if(worldPos.)
                
                //OUT.vertex.y += 10;
               // if (worldPos.z > 0.8)
               // {
                 //   OUT.vertex.y += 10;
                //}
                float2 uv = IN.uv;


                OUT.uv = TRANSFORM_TEX(uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                // Returning the _BaseColor value.                
                return color;//_BaseColor;
            }
            ENDHLSL
        }
    }
}
