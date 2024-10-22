Shader "Slices/X_Upwards_Transparent" {
    Properties{
      _MainTex("Texture", 2D) = "white" {}
      _BumpMap("Bumpmap", 2D) = "bump" {}
      _Offset("Extrusion Amount", Range(-53,70)) = 0
      _Transparency("Transparency", Range(0,2)) = 1
    }
        SubShader{
          Tags {"RenderType" = "Transparent" "IgnoreProjector" = "True"}
          Cull Front
          Blend DstColor SrcColor
          CGPROGRAM
          #pragma surface surf Lambert vertex:vert alpha:fade
          struct Input {
              float2 uv_MainTex;
              float2 uv_BumpMap;
              float3 worldPos;
              float3 objPos;
          };
          sampler2D _MainTex;
          sampler2D _BumpMap;
          float _Frequency;
          float _Offset;
          float _Transparency;

          void vert(inout appdata_full v, out Input o) {
              UNITY_INITIALIZE_OUTPUT(Input, o);
              o.objPos = v.vertex;
          }

          void surf(Input IN, inout SurfaceOutput o) {
              clip(frac((IN.objPos.x + _Offset) * 0.001) - 0.15);
              o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
              o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
              o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a * _Transparency;
          }
          ENDCG
    }
        Fallback "Diffuse"
}
