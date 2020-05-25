// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/shader2"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)// the color of our object
		_Tex("Pattern", 2D) = "white" {} // Optional texture

		_Shiniess("Shiniess", Float) = 10 // Shininess
		_SpecColor("Specular Color", Color) = (1, 1, 1, 1) // Specular highlights color

		_Ka("Ka", Vector) = (1, 1, 1, 1)
		_Kd("Kd", Vector) = (1, 1, 1, 1)
		_Ks("Ks", Vector) = (1, 1, 1, 1)
	}
	SubShader
	{
				Tags {"RenderType" = "Opaque"} // not rendering any transparent objects
			Pass
			{
				Tags{"LightMode}" = "ForwardBase"}//for first light
				//Tags{"LightMode}" = "ForwardAdd"}
				CGPROGRAM//for GPU code
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"


				uniform float4 _LightColor0; // from UnityCG
				sampler2D _Tex; //used for texture
				float4 _Tex_ST; //For tiling

				uniform float4 _Color;
				uniform float4 _SpecColor;
				uniform float _Shininess;

				uniform float4 _Ka;
				uniform float4 _Kd;
				uniform float4 _Ks;


				struct appdata
				{
					float4 vertex: POSITION;
					float3 normal: NORMAL;
					float2 uv: TEXCOORD0;
				};
				struct v2f
				{
					float4 pos: POSITION;
					float3 normal: NORMAL;
					float2 uv: TEXCOORD0;
					float4 posWorld:TEXCOORD1;
				};


				v2f vert(appdata v)
				{
					v2f o;
					o.posWorld = mul(unity_ObjectToWorld, v.vertex);
					//float4(0, 0, 0, 0);
					o.normal = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
					//o.normal = float3(0, 0, 0);
					o.pos = UnityObjectToClipPos(v.vertex);
					//o.pos = UnityObjectToClipPos(v.vertex);
					//o.pos = float4(0, 0, 0, 0);

					o.uv = TRANSFORM_TEX(v.uv, _Tex);
					//o.uv = float2(0, 0);
					return o;
				}

				fixed4 frag(v2f i) : COLOR
				{
					float3 normalDirection = i.normal;
					float3 lightDirection =
						normalize(_WorldSpaceLightPos0.xyz -
							i.posWorld.xyz * _WorldSpaceLightPos0.w);


					// ambient 
					float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Ka.rgb; //_Color.rgb;//ambient光乘顏色

					//difuse component
					float3 diffuseReflection =
						_LightColor0.rgb *
						_Ka.rgb *//_Color.rgb * 
						max(0.0, dot(normalDirection, lightDirection));

					float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

					float3 specularReflection;// = (1.0, 1.0, 1.0);

					if (dot(i.normal, lightDirection) < 0.0)
					{
						specularReflection = float3(0.0, 0.0, 0.0);
					}
					else
					{
						specularReflection = _LightColor0.rgb * _SpecColor.rgb *
							pow(
								max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)),
								_Shininess) * _Ks.rgb;
					}


					//float3 color = ()

					float3 color = (ambientLighting + diffuseReflection) * tex2D(_Tex, i.uv);//+ specularReflection;

					return float4(color, 1.0);

					//return float4(1.0, 1.0, 1.0, 1.0);
				}
				ENDCG
			}
			Pass
				{
					Tags{"LightMode}" = "ForwardAdd"}
					Blend One One
					CGPROGRAM//for GPU code
					#pragma vertex vert
					#pragma fragment frag

					#include "UnityCG.cginc"


					uniform float4 _LightColor0; // from UnityCG
					sampler2D _Tex; //used for texture
					float4 _Tex_ST; //For tiling

					uniform float4 _Color;
					uniform float4 _SpecColor;
					uniform float _Shininess;

					uniform float4 _Ka;
					uniform float4 _Kd;
					uniform float4 _Ks;


					struct appdata
					{
						float4 vertex: POSITION;
						float3 normal: NORMAL;
						float2 uv: TEXCOORD0;
					};
					struct v2f
					{
						float4 pos: POSITION;
						float3 normal: NORMAL;
						float2 uv: TEXCOORD0;
						float4 posWorld:TEXCOORD1;
					};


					v2f vert(appdata v)
					{
						v2f o;
						o.posWorld = mul(unity_ObjectToWorld, v.vertex);
						//float4(0, 0, 0, 0);
						o.normal = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
						//o.normal = float3(0, 0, 0);
						o.pos = UnityObjectToClipPos(v.vertex);
						//o.pos = UnityObjectToClipPos(v.vertex);
						//o.pos = float4(0, 0, 0, 0);

						o.uv = TRANSFORM_TEX(v.uv, _Tex);
						//o.uv = float2(0, 0);
						return o;
					}

					fixed4 frag(v2f i) : COLOR
					{
						float3 normalDirection = i.normal;
						float3 lightDirection =
							normalize(_WorldSpaceLightPos0.xyz -
								i.posWorld.xyz * _WorldSpaceLightPos0.w);


						// ambient 
						float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Ka.rgb; //_Color.rgb;//ambient光乘顏色

						//difuse component
						float3 diffuseReflection =
							_LightColor0.rgb *
							_Ka.rgb *//_Color.rgb * 
							max(0.0, dot(normalDirection, lightDirection));

						float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

						float3 specularReflection;// = (1.0, 1.0, 1.0);

						if (dot(i.normal, lightDirection) < 0.0)
						{
							specularReflection = float3(0.0, 0.0, 0.0);
						}
						else
						{
							specularReflection = _LightColor0.rgb * _SpecColor.rgb *
								pow(
									max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)),
									_Shininess) * _Ks.rgb;
						}


						//float3 color = ()

						float3 color = (ambientLighting + diffuseReflection) * tex2D(_Tex, i.uv);//+ specularReflection;

						return float4(color, 1.0);

						//return float4(1.0, 1.0, 1.0, 1.0);
					}
					ENDCG
				}
				
				
	}
}
