Shader "Hidden/Styliser"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TextureSize("_TextureSize", Float) = 0
		_Softness("_Softness", Float) = 0
		_Texture ("_Texture", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
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
				float2 patternUv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			
			sampler2D _MainTex;			
			float _TextureSize;
			float _Softness;
			sampler2D _Texture;

			#define MaxLength 8
			float _Starts[MaxLength];
			float4 _Colors[MaxLength];

			float4 _RotationMatrix;
			float _TransitionSize;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.patternUv = v.uv * _ScreenParams.xy / _TextureSize;
				o.patternUv = mul (o.patternUv, float2x2(_RotationMatrix));
				return o;
			}


			float4 frag (v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				float light = (color.x + color.y + color.z) / 3;

				int pattern = 0;
				for(int j = 0; j < MaxLength; j++)
				{
					pattern = _Starts[j] < light ? j : pattern;
				}

				color = _Colors[pattern];

				float f = (light - _Starts[pattern]) / (_Starts[pattern + 1] - _Starts[pattern]);

				f = lerp(0.5, f, _TransitionSize); 

				float tex = saturate(0.5 + _Softness * (f - tex2D(_Texture, i.patternUv).r));
				color = lerp(_Colors[pattern], _Colors[pattern + 1], tex);

				return color;
			}
			ENDCG
		}
	}
}
