// Simplified VertexLit shader, optimized for high-poly meshes. Differences from regular VertexLit one:
// - less per-vertex work compared with Mobile-VertexLit
// - supports only DIRECTIONAL lights and ambient term, saves some vertex processing power
// - no per-material color
// - no specular
// - no emission

Shader "CityBuild/VertexLit/DirectionLightOnlyWithLightMap" 

//  {
// SubShader {
//     Pass {
//         CGPROGRAM
//         #pragma vertex vert
//         #pragma fragment frag
//         #include "UnityCG.cginc"

//         // vertex input: position, UV
//         struct appdata {
//             float4 vertex : POSITION;
//             float4 texcoord : TEXCOORD1;
//         };

//         struct v2f {
//             float4 pos : SV_POSITION;
//             float4 uv : TEXCOORD0;
//         };
        
//         v2f vert (appdata v) {
//             v2f o;
//             o.pos = UnityObjectToClipPos(v.vertex );
//             o.uv = float4( v.texcoord.xy, 0, 0 );
//             return o;
//         }
        
//         half4 frag( v2f i ) : SV_Target {
//             half4 c = frac( i.uv );
//             if (any(saturate(i.uv) - i.uv))
//                 c.b = 0.5;
//             return c;
//         }
//         ENDCG
//     }
// }
// }

// {
// SubShader {
//     Pass {
//         CGPROGRAM
//         #pragma vertex vert
//         #pragma fragment frag
//         #include "UnityCG.cginc"

//         // vertex input: position, second UV
//         struct appdata {
//             float4 vertex : POSITION;
//             float4 texcoord1 : TEXCOORD1;
//         };

//         struct v2f {
//             float4 pos : SV_POSITION;
//             float4 uv : TEXCOORD0;
//         };
        
//         v2f vert (appdata v) {
//             v2f o;
//             o.pos = UnityObjectToClipPos(v.vertex );
//             o.uv = float4( v.texcoord1.xy, 0, 0 );
//             return o;
//         }
        
//         half4 frag( v2f i ) : SV_Target {
//             half4 c = frac( i.uv );
//             if (any(saturate(i.uv) - i.uv))
//                 c.b = 0.5;
//             return c;
//         }
//         ENDCG
//     }
// }
// }



{
	Properties {
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _AoTex ("AO (RGB)", 2D) = "white" {}

        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            sampler2D _AoTex;


            struct Input {
                float2 uv_MainTex : TEXCOORD0;
                float2 uv2_AoTex : TEXCOORD1;
            };

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex.xy);
                half4 ao = tex2D (_AoTex, IN.uv2_AoTex.xy);
                o.Albedo = c.rgb * ao.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        } 
        FallBack "Diffuse"
    }
