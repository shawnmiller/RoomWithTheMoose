Shader "RwtM/TheFeels" {
	Properties {
		_Color ("Lit Color", Color) = (0.31, 0.56, 0.78, 1)
    _Touch ("Touch Location", 2D) = ""
    _TempTouch ("Temp Touch", Vector) = (0,0,0,0)
    _IgnoreDistance("Ignore Radius", Float) = 5
    _Radius ("Touch Radius", Float) = 0.01
    _Fade ("Fade Amount", Float) = 0.5
	} 
	SubShader {
		Lighting off

		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members uv_MainTex)
#pragma exclude_renderers d3d11 xbox360
		#pragma surface surf NoLighting

    struct Input {
          float3 worldPos;
    };
    fixed4 _Color;
    float3 _TempTouch;
    half _Radius;
    half _IgnoreDistance;
    half _Fade;

		sampler2D _MainTex;

    void surf(Input IN, inout SurfaceOutput o) {
      half dist = distance(IN.worldPos.xyz, _TempTouch.xyz);
      if(dist > _IgnoreDistance) {
        o.Albedo = (0,0,0);
      }
      else {
        half4 cMod;
        cMod.rgb = _Color.rgb - dist * _Fade;
        cMod.a = 1;
        half4 black = (1,1,1,1);
        //o.Albedo = lerp(_Color, black, dist/_Radius);
        o.Albedo = cMod;
      }
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
