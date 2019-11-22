Shader "Custom/hb-shader"
{
    Properties
    {
        [HideInInspector]
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _HP ("HP", Range(0,100)) = 50
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _HP;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            if(IN.uv_MainTex.r >= 0.5-(_HP)/200 && IN.uv_MainTex.r <= 0.5+(_HP)/200)
            {
                o.Emission = fixed4(0,1,0,1)*4;
                o.Albedo = fixed4(0,1,0,1);
            }
            else
                o.Albedo = fixed4(0.5,0,0,1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
