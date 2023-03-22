Shader "Unlit/Earth"
{
	Properties
	{
			   _MainTex("Texture", 2D) = "white" {}
	 _SecTex("Second Texture", 2D) = "white"{}
	 _Blend("Blend value", Range(0.01,1)) = 0.0
	 _Color("Color", Color) = (1,1,1,1)
	 _ScrollXSpeed("X Scroll Speed", Range(0,10)) = 0.01

	}
		SubShader
	 {
		  Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
		 LOD 100

				Pass{
					 CGPROGRAM
					 #pragma vertex vert
					 #pragma fragment frag
					 #include "UnityCG.cginc"
					 struct appdata
					 {
						 float4 vertex : POSITION;
						 float2 uv : TEXCOORD0;
					 };
					 struct v2f
					 {
						 float2 uv : TEXCOORD0;
						 UNITY_FOG_COORDS(1)
						 float4 vertex : SV_POSITION;
					 };

					 sampler2D _MainTex, _SecTex;
					 float4 _MainTex_ST; half _Blend; fixed4 _Color; float _ScrollXSpeed;

					 v2f vert(appdata v)
					 {
						 v2f o;
						 o.vertex = UnityObjectToClipPos(v.vertex);
						float2 uv = v.uv;
						uv.x = _ScrollXSpeed * _Time;
						 o.uv = TRANSFORM_TEX(v.uv, _MainTex);
						UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					 }
					 float4 blend(float4 firstTex, float4 secondTex, half blendValue)
					 {
						 fixed4 c = lerp(firstTex, secondTex, blendValue);
						 return c;
					 }
					 fixed4 frag(v2f i) : SV_Target
					 {
						i.uv.x = i.uv.x + _ScrollXSpeed * _Time;
						 fixed4 col1 = tex2D(_MainTex, i.uv) * _Color;
						fixed4 col2 = tex2D(_SecTex, i.uv) * _Color;
						fixed4 col = blend(col1, col2, _Blend);
						 return col;
					 }
			 ENDCG

		 }
	 }
}
