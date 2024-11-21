Shader "HighlightPlus/Geometry/IconFX"
{
      Properties
    {
        _MainTex ("Texture", 2D) = "white" {} 
        _Color ("Color", Color) = (1,1,1,1)
        _DarkColor ("Dark Color", Color) = (0.5, 0.5, 0.5,1)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            half4 _Color, _DarkColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.color = v.color;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 finalColor = i.color;

                // Basic Lambertian reflection (diffuse lighting)
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float lambert = max(dot(i.normal, lightDir), 0);

                // Apply lighting to final color
                finalColor = lerp(_DarkColor, _Color, lambert);

                return finalColor;
            }
            ENDCG
        }
    }
}