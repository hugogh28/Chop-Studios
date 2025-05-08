Shader "Unlit/Protanopia"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Correction Intensity", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Intensity;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Copia original
                float3 original = col.rgb;

                // Simula cómo vería un protanope
                float3 simulated;
                simulated.r = dot(original.rgb, float3(0.567, 0.433, 0.0));
                simulated.g = dot(original.rgb, float3(0.558, 0.442, 0.0));
                simulated.b = dot(original.rgb, float3(0.0,   0.242, 0.758));

                // Calcula la diferencia entre original y simulado
                float3 error = original.rgb - simulated;

                // Aplica corrección visual amplificando la diferencia
                float3 corrected = original + error * _Intensity;

                return float4(corrected, col.a);
            }
            ENDCG
        }
    }
}
