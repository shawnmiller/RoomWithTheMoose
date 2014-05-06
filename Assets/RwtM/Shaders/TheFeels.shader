Shader "RwtM/TheFeels" {
	Properties {
		_Color ("Lit Color", Color) = (0.31, 0.56, 0.78, 1)
    //_EdgeTex("Edge Texture", 2D) = "black" {}
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
    };

    fixed4 _Color;
    float3 _TempTouch;
    half _Fade;

    float4 _HandPosition;

    float4x4 _TouchLocationsHand;
    float4x4 _TouchLocationsBody;

    void surf(Input IN, inout SurfaceOutput o) {
      half closest = 100000.0;
      half cStrength = 1.0;

      //half dist;
      closest = distance(IN.worldPos.xyz, _HandPosition.xyz);

      /*for(int i=0; i<4; ++i)
      {
        dist = distance(IN.worldPos.xyz, _TouchLocationsHand[i].xyz); 
        if(dist < closest)
        {
          closest = dist;
        }
      }
      for(int j=0; j<3; ++j) 
      {
        dist = distance(IN.worldPos.xyz, _TouchLocationsBody[j].xyz);
        if(dist < closest)
        {
          closest = dist;
          cStrength = _TouchLocationsBody[j].w;
        }
      }*/

      half4 cMod;
      cMod.rgb = _Color.rgb - closest * _Fade * cStrength;
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
