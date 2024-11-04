Shader "HighlightPlus/Geometry/ComposeOutline" {
Properties {
    _MainTex ("Texture", Any) = "black" {}
	_Color("Color", Color) = (1,1,1) // not used; dummy property to avoid inspector warning "material has no _Color property"
	_Cull("Cull Mode", Int) = 2
	_ZTest("ZTest Mode", Int) = 0
	_Flip("Flip", Vector) = (0, 1, 0)
	_Debug("Debug Color", Color) = (0,0,0,0)
	_OutlineStencilComp("Stencil Comp", Int) = 6
	_OutlineSharpness("Outline Sharpness", Float) = 1.0
}
SubShader
	{
		Tags { "Queue" = "Transparent+120" "RenderType" = "Transparent" "DisableBatching" = "True" }
		Blend SrcAlpha OneMinusSrcAlpha

		// Compose effect on camera target (optimal quad blit)
		Pass
		{
			ZWrite Off
			ZTest Always // [_ZTest]
			Cull Off // [_Cull]
			Stencil {
				Ref 2
				Comp [_OutlineStencilComp]
				Pass keep
				ReadMask 2
				WriteMask 2
			}
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_local _ HP_ALL_EDGES
			#pragma multi_compile_local _ HP_MASK_CUTOUT

			#include "UnityCG.cginc"

			UNITY_DECLARE_SCREENSPACE_TEXTURE(_HPComposeOutlineFinal);
			UNITY_DECLARE_SCREENSPACE_TEXTURE(_HPSourceRT);
			float4 _HPSourceRT_TexelSize;

            fixed4 _Color;
			float3 _Flip;
			fixed4 _Debug;
			fixed _OutlineSharpness;
			#if HP_ALL_EDGES
				#define OUTLINE_SOURCE outline.g
			#else
				#define OUTLINE_SOURCE outline.r
			#endif

            struct appdata
            {
                float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
				float4 pos: SV_POSITION;
				float4 scrPos: TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);
				o.scrPos.y = o.scrPos.w * _Flip.x + o.scrPos.y * _Flip.y;
				return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				float2 uv = i.scrPos.xy/i.scrPos.w;
				fixed4 outline = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_HPComposeOutlineFinal, uv);
            	fixed4 color = _Color;
            	color.a *= OUTLINE_SOURCE;

				#if HP_MASK_CUTOUT
					fixed4 maskN = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_HPSourceRT, uv + float2(0, 1) * _HPSourceRT_TexelSize.xy);
					fixed4 maskS = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_HPSourceRT, uv + float2(0, -1) * _HPSourceRT_TexelSize.xy);
					fixed4 maskW = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_HPSourceRT, uv + float2(-1, 0) * _HPSourceRT_TexelSize.xy);
					fixed4 maskE = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_HPSourceRT, uv + float2(1, 0) * _HPSourceRT_TexelSize.xy);
					color.a *= all(maskN.rgb==0) || all(maskS.rgb == 0) || all(maskW.rgb == 0) || all(maskE.rgb == 0);
				#endif

				color += _Debug;
            	color.a = saturate(color.a);
				color.a = pow(color.a, _OutlineSharpness);

            	return color;
			}
				ENDCG
	}

		// Compose effect on camera target (full-screen blit)
		Pass
			{
				ZWrite Off
				ZTest Always //[_ZTest]
				Cull Off // [_Cull]
				Stencil {
					Ref 2
					Comp [_OutlineStencilComp]
					Pass keep
					ReadMask 2
					WriteMask 2
				}

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_local _ HP_ALL_EDGES

				#include "UnityCG.cginc"

				UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
				float4 _MainTex_ST;
				fixed4 _Color;
				float3 _Flip;
				fixed _OutlineSharpness;
				#if HP_ALL_EDGES
					#define OUTLINE_SOURCE outline.g
				#else
					#define OUTLINE_SOURCE outline.r
				#endif

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv     : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos: SV_POSITION;
					float2 uv     : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2f vert(appdata v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = UnityStereoScreenSpaceUVAdjust(v.uv, _MainTex_ST);
					o.uv.y = _Flip.x + o.uv.y * _Flip.y;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(i);
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
					fixed4 outline = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv);
					fixed4 color = _Color;
					color.a *= OUTLINE_SOURCE;
                    color = saturate(color);
					color.a = pow(color.a, _OutlineSharpness);
					return color;
				}
				ENDCG
			}

    }
}