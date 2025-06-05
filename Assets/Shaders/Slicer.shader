Shader "Custom/ClippingPlane"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Properties for the clipping plane
        _PlanePosition ("Plane Position", Vector) = (0,0,0,0)
        _PlaneNormal ("Plane Normal", Vector) = (0,1,0,0)
    }
    SubShader
    {
        // Define default tags for opaque rendering.
        // These will be overridden by C# for transparent mode.
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        LOD 200

        Cull Off
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0



        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        fixed4 _Color;
        half _Glossiness;
        half _Metallic;

        float4 _PlanePosition;
        float4 _PlaneNormal;
        float _CutTransparency;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Clipping logic remains the same
            float dist = dot(IN.worldPos - _PlanePosition.xyz, _PlaneNormal.xyz);
            clip(dist);

            // Standard PBR surface output
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Standard"
}