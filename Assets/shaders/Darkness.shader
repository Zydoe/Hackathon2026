Shader "Custom/RadialLight"
{
    Properties
    {
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _Radius ("Light Radius", Float) = 4
        _Softness ("Softness", Float) = 2
        _DarkColor ("Dark Color", Color) = (0,0,0,0.85)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _PlayerPos;
            float _Radius;
            float _Softness;
            float4 _DarkColor;

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = distance(i.worldPos.xy, _PlayerPos.xy);

                // 0 inside radius, 1 outside radius
                float t = saturate((dist - _Radius) / _Softness);

                // DarkColor alpha fades in smoothly
                return float4(_DarkColor.rgb, t * _DarkColor.a);
            }
            ENDCG
        }
    }
}
