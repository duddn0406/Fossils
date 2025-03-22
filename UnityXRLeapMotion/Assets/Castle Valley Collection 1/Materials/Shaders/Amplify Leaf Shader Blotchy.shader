// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify Leaf Blotchy"
{
	Properties
	{
		_Tint("Tint", Color) = (0,0.9201922,1,0)
		_BlotchTint("Blotch Tint", Color) = (1,0,0,0)
		_BlotchSize("Blotch Size", Float) = 0.1
		_Contrast("Contrast", Float) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Roughness("Roughness", Range( 0 , 1)) = 0.23
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.45
		_Normals("Normals", 2D) = "bump" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_WindFrequency("Wind Frequency", Float) = 1.34
		_MovementFadeEnd("Movement Fade End", Float) = 60
		_NoiseScale("Noise Scale", Float) = 1.41
		_MovementFadeStart("Movement Fade Start", Float) = 30
		_MovementMultiplier("Movement Multiplier", Float) = 0.07
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustom keepalpha addshadow fullforwardshadows exclude_path:deferred dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			half ASEIsFrontFacing : VFACE;
			float3 worldPos;
		};

		struct SurfaceOutputStandardCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			half3 Transmission;
		};

		uniform float _WindFrequency;
		uniform float _NoiseScale;
		uniform float _MovementMultiplier;
		uniform float _MovementFadeEnd;
		uniform float _MovementFadeStart;
		uniform sampler2D _Normals;
		uniform float4 _Normals_ST;
		uniform float _Contrast;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _BlotchSize;
		uniform float4 _Tint;
		uniform float4 _BlotchTint;
		uniform float _Metallic;
		uniform float _Roughness;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;
		uniform float _Cutoff = 0.45;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime10 = _Time.y * _WindFrequency;
			float2 temp_cast_0 = (mulTime10).xx;
			float2 uv_TexCoord7 = v.texcoord.xy + temp_cast_0;
			float simplePerlin3D6 = snoise( float3( uv_TexCoord7 ,  0.0 )*_NoiseScale );
			simplePerlin3D6 = simplePerlin3D6*0.5 + 0.5;
			float temp_output_30_0 = ( ( simplePerlin3D6 + -0.5 ) * _MovementMultiplier );
			float simplePerlin2D33 = snoise( uv_TexCoord7*_NoiseScale );
			simplePerlin2D33 = simplePerlin2D33*0.5 + 0.5;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float cameraDepthFade82 = (( -UnityObjectToViewPos( ase_vertex3Pos ).z -_ProjectionParams.y - _MovementFadeStart ) / _MovementFadeEnd);
			float temp_output_36_0 = ( _MovementMultiplier * ( simplePerlin2D33 + -0.5 ) * ( 1.0 - saturate( cameraDepthFade82 ) ) );
			float3 appendResult27 = (float3(temp_output_30_0 , temp_output_36_0 , ( ( temp_output_30_0 + temp_output_36_0 ) * 0.4 )));
			v.vertex.xyz += ( v.texcoord1.xy.y * appendResult27 );
			v.vertex.w = 1;
		}

		inline half4 LightingStandardCustom(SurfaceOutputStandardCustom s, half3 viewDir, UnityGI gi )
		{
			half3 transmission = max(0 , -dot(s.Normal, gi.light.dir)) * gi.light.color * s.Transmission;
			half4 d = half4(s.Albedo * transmission , 0);

			SurfaceOutputStandard r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Metallic = s.Metallic;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandard (r, viewDir, gi) + d;
		}

		inline void LightingStandardCustom_GI(SurfaceOutputStandardCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardCustom o )
		{
			float2 uv_Normals = i.uv_texcoord * _Normals_ST.xy + _Normals_ST.zw;
			float3 tex2DNode2 = UnpackNormal( tex2D( _Normals, uv_Normals ) );
			float3 appendResult78 = (float3(tex2DNode2.xy , ( tex2DNode2.b * i.ASEIsFrontFacing )));
			o.Normal = appendResult78;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _Albedo, uv_Albedo );
			float3 ase_worldPos = i.worldPos;
			float simplePerlin2D65 = snoise( (ase_worldPos).xz*_BlotchSize );
			simplePerlin2D65 = simplePerlin2D65*0.5 + 0.5;
			float layeredBlendVar70 = simplePerlin2D65;
			float4 layeredBlend70 = ( lerp( _Tint,_BlotchTint , layeredBlendVar70 ) );
			o.Albedo = CalculateContrast(_Contrast,( tex2DNode1 * layeredBlend70 )).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			float4 tex2DNode25 = tex2D( _Occlusion, uv_Occlusion );
			o.Occlusion = tex2DNode25.r;
			float3 temp_cast_2 = (tex2DNode25.g).xxx;
			o.Transmission = temp_cast_2;
			o.Alpha = 1;
			clip( tex2DNode1.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19302
Node;AmplifyShaderEditor.RangedFloatNode;8;-1100.445,600.1421;Inherit;False;Property;_WindFrequency;Wind Frequency;10;0;Create;True;0;0;0;False;0;False;1.34;0.54;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-896.8459,607.843;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-985.3492,1219.275;Inherit;False;Property;_MovementFadeStart;Movement Fade Start;13;0;Create;True;0;0;0;False;0;False;30;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;80;-889.3492,979.2751;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;81;-969.3492,1139.275;Inherit;False;Property;_MovementFadeEnd;Movement Fade End;11;0;Create;True;0;0;0;False;0;False;60;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-701.1458,561.4418;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-676.4127,692.6339;Inherit;False;Property;_NoiseScale;Noise Scale;12;0;Create;True;0;0;0;False;0;False;1.41;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;82;-649.3492,1075.275;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;33;-415.2275,704.614;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;6;-439.7458,481.5416;Inherit;False;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;83;-387.5659,1078.369;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-193.9351,692.2194;Inherit;False;Property;_MovementMultiplier;Movement Multiplier;14;0;Create;True;0;0;0;False;0;False;0.07;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-218.4274,795.8138;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-225.4586,567.2444;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;75;-956.8929,-349.0807;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;84;-227.5913,1078.444;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;75.97253,699.814;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;26.46487,560.9194;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-640.0386,5.59639;Inherit;False;Property;_BlotchSize;Blotch Size;2;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;74;-704.1288,-325.4783;Inherit;False;FLOAT2;0;2;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;267.1733,730.214;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;59;-445.701,145.2438;Inherit;False;Property;_Tint;Tint;0;0;Create;True;0;0;0;False;0;False;0,0.9201922,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;65;-407.4501,-104.4785;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;61;-435.0613,313.6519;Inherit;False;Property;_BlotchTint;Blotch Tint;1;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;411.1733,727.0139;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.LayeredBlendNode;70;-77.08778,15.4831;Inherit;True;6;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-167.0459,-204.4612;Inherit;True;Property;_Albedo;Albedo;4;0;Create;True;0;0;0;False;0;False;-1;5d93f9a7872434d419a703ec94e2b61b;47e31f197cf93ac468e725e114f473a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;208.7648,81.14058;Inherit;True;Property;_Normals;Normals;8;0;Create;True;0;0;0;False;0;False;-1;164b81533617247499e86da8845bbcc4;50db1768118f76d489afcdee99ab8c04;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FaceVariableNode;76;413.9753,292.3847;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;27;516.8499,563.9042;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;57;285.3972,457.5728;Inherit;False;1;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;244.8283,-147.2706;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;73;445.1215,-4.804946;Inherit;False;Property;_Contrast;Contrast;3;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;573.9749,228.3848;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;620.0734,400.9138;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;25;130.892,-340.9033;Inherit;True;Property;_Occlusion;Occlusion;9;0;Create;True;0;0;0;False;0;False;-1;95ac4b066fb1566438ed2d7938ad0c37;234817250c675f94cb603eb067190ed1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;462.5802,-379.874;Inherit;False;Property;_Roughness;Roughness;5;0;Create;True;0;0;0;False;0;False;0.23;0.15;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;72;573.8259,-127.3322;Inherit;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;849.8809,-209.3155;Inherit;False;Property;_Metallic;Metallic;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;78;733.9749,164.3848;Inherit;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1389.019,-28.48312;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amplify Leaf Blotchy;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.45;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;7;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;8;0
WireConnection;7;1;10;0
WireConnection;82;2;80;0
WireConnection;82;0;81;0
WireConnection;82;1;79;0
WireConnection;33;0;7;0
WireConnection;33;1;28;0
WireConnection;6;0;7;0
WireConnection;6;1;28;0
WireConnection;83;0;82;0
WireConnection;34;0;33;0
WireConnection;22;0;6;0
WireConnection;84;0;83;0
WireConnection;36;0;29;0
WireConnection;36;1;34;0
WireConnection;36;2;84;0
WireConnection;30;0;22;0
WireConnection;30;1;29;0
WireConnection;74;0;75;0
WireConnection;38;0;30;0
WireConnection;38;1;36;0
WireConnection;65;0;74;0
WireConnection;65;1;71;0
WireConnection;39;0;38;0
WireConnection;70;0;65;0
WireConnection;70;1;59;0
WireConnection;70;2;61;0
WireConnection;27;0;30;0
WireConnection;27;1;36;0
WireConnection;27;2;39;0
WireConnection;58;0;1;0
WireConnection;58;1;70;0
WireConnection;77;0;2;3
WireConnection;77;1;76;0
WireConnection;40;0;57;2
WireConnection;40;1;27;0
WireConnection;72;1;58;0
WireConnection;72;0;73;0
WireConnection;78;0;2;0
WireConnection;78;2;77;0
WireConnection;0;0;72;0
WireConnection;0;1;78;0
WireConnection;0;3;60;0
WireConnection;0;4;4;0
WireConnection;0;5;25;1
WireConnection;0;6;25;2
WireConnection;0;10;1;4
WireConnection;0;11;40;0
ASEEND*/
//CHKSM=E5900D3A102AC1406C7D8327D108FDBA5EDABA6C