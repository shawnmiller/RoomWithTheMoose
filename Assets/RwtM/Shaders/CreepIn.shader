Shader "RWTM/CreepIn"
{
	Properties 
	{
		_FirstWorld("_FirstWorld", 2D) = "black" {}
		_SecondWorld("_SecondWorld", 2D) = "black" {}
		_BlendPos("_BlendPos", Range(0,1) ) = 0.5
		_BlendMethod("_BlendMethod", 2D) = "black" {}
	}
	
	SubShader 
	{
		Tags
		{
			"Queue"="Geometry"
			"RenderType"="Transparent"
		}

		CGPROGRAM
		#pragma surface surf BlinnPhongEditor vertex:vert
		#pragma target 2.0
		sampler2D _FirstWorld;
		sampler2D _SecondWorld;
		float _BlendPos;
		sampler2D _BlendMethod;
		
		struct EditorSurfaceOutput {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
			half4 Custom;
		};
		
		struct Input {
			float2 uv_FirstWorld;
			float2 uv_BlendMethod;
			float2 uv_SecondWorld;
		};
		
		inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
		{
			half3 spec = light.a * s.Gloss;
			half4 c;
			c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
			c.a = s.Alpha;
			return c;
		}

		inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize (lightDir + viewDir);
			
			half diff = max (0, dot ( lightDir, s.Normal ));
			
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular*128.0);
			
			half4 res;
			res.rgb = _LightColor0.rgb * diff;
			res.w = spec * Luminance (_LightColor0.rgb);
			res *= atten * 2.0;

			return LightingBlinnPhongEditor_PrePass( s, res );
		}
		
		// Not Used
		void vert (inout appdata_full v, out Input o) {	}
		
		void surf (Input IN, inout EditorSurfaceOutput o) {
			o.Normal = float3(0.0,0.0,1.0);
			o.Alpha = 1.0;
			o.Albedo = 0.0;
			o.Emission = 0.0;
			o.Gloss = 0.0;
			o.Specular = 0.0;
			o.Custom = 0.0;
			
			float4 Tex2D0=tex2D(_FirstWorld,(IN.uv_FirstWorld.xyxy).xy);
			float4 Tex2D1=tex2D(_SecondWorld,(IN.uv_SecondWorld.xyxy).xy);
			float4 Tex2D2=tex2D(_BlendMethod,(IN.uv_BlendMethod.xyxy).xy);
			
			if(Tex2D2[3] > _BlendPos)
			{
				o.Albedo = Tex2D0;
			}
			else
			{
				o.Albedo = Tex2D1;
			}

			o.Normal = normalize(o.Normal);
		}
		ENDCG
	}
	Fallback "Diffuse"
}