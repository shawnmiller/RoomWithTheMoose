Shader "RwtM/TheFeels" {
	Properties {
		_Color ("Lit Color", Color) = (0.31, 0.56, 0.78, 1)
    _EdgeTex("Edge Texture", 2D) = "black" {}
    _ComputedTex("Comp Texture", 2D) = "black" {}
    _Touch ("Touch Location", 2D) = ""
    _TempTouch ("Temp Touch", Vector) = (0,0,0,0)
    _IgnoreDistance("Ignore Radius", Float) = 5
    _Radius ("Touch Radius", Float) = 0.01
    _Fade ("Fade Amount", Float) = 0.5
	} 
	SubShader {
		Lighting off

		CGPROGRAM
		#pragma surface surf NoLighting

    struct Input {
          float3 worldPos;
          float2 uv_EdgeTex;
    };

    fixed4 _Color;
    float3 _TempTouch;
    half _Fade;

    sampler2D _EdgeTex;
    //sampler2D _CompTex;

    void surf(Input IN, inout SurfaceOutput o) {
      half dist = distance(IN.worldPos.xyz, _TempTouch.xyz);
      half4 cMod;
      half3 edgeVal;
      edgeVal = tex2D(_EdgeTex, IN.uv_EdgeTex).rgb;
      cMod.rgb = _Color.rgb - dist * _Fade;
      cMod.rgb += edgeVal * 0.2 - dist * _Fade;
      cMod.a = 1;
      o.Albedo = cMod;
    }

    fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
    {
        fixed4 c;
        c.rgb = s.Albedo; 
        c.a = s.Alpha;
        return c;
    }
    
		ENDCG
	} 
	FallBack "Diffuse"
}
