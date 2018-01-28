Shader "Vertex Colored Lit"
{
	Properties{	}
	SubShader
	{
		Tags{ "Queue" = "Geometry" "IgnoreProjector" = "True" "RenderType" = "Opaque"  }
		LOD 200
		Lighting On
		CGPROGRAM
		#pragma surface surf Lambert 

		struct Input 
		{
			float2 uv_MainTex;
			float4 color: Color; // Vertex color
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = IN.color.rgb; // vertex RGB
			o.Alpha = IN.color.a; // vertex Alpha
		}

		ENDCG
	}
	FallBack "Diffuse"
}