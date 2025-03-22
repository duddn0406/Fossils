// Made with Amplify Shader Editor v1.9.2.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify Unscripted Sandstone Shader"
{
	Properties
	{
		_Color("Color", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_DetailColor("Detail Color", 2D) = "gray" {}
		_DetailNormals("Detail Normals", 2D) = "bump" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_OcclusionMult("OcclusionMult", Range( 0 , 1)) = 0
		_LocalSmooth("Local Smooth", Float) = 1
		_LocalTint("Local Tint", Color) = (0.4811321,0.4811321,0.4811321,0)
		_LocalContrast("Local Contrast", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _DetailNormals;
		uniform float4 _DetailNormals_ST;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;
		uniform sampler2D _Color;
		uniform float4 _Color_ST;
		uniform float4 _LocalTint;
		uniform float _LocalContrast;
		uniform sampler2D _DetailColor;
		uniform float4 _DetailColor_ST;
		uniform float _LocalSmooth;
		uniform float _OcclusionMult;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float2 uv_DetailNormals = i.uv_texcoord * _DetailNormals_ST.xy + _DetailNormals_ST.zw;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			float4 tex2DNode7 = tex2D( _Occlusion, uv_Occlusion );
			o.Normal = BlendNormals( UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) ) , UnpackScaleNormal( tex2D( _DetailNormals, uv_DetailNormals ), tex2DNode7.a ) );
			float2 uv_Color = i.uv_texcoord * _Color_ST.xy + _Color_ST.zw;
			float4 tex2DNode1 = tex2D( _Color, uv_Color );
			float4 blendOpSrc14 = tex2DNode1;
			float4 blendOpDest14 = ( _LocalTint * saturate( ( _LocalContrast + tex2DNode1.r ) ) );
			float2 uv_DetailColor = i.uv_texcoord * _DetailColor_ST.xy + _DetailColor_ST.zw;
			float temp_output_9_0_g1 = tex2DNode7.a;
			float temp_output_18_0_g1 = ( 1.0 - temp_output_9_0_g1 );
			float3 appendResult16_g1 = (float3(temp_output_18_0_g1 , temp_output_18_0_g1 , temp_output_18_0_g1));
			o.Albedo = ( ( saturate( 2.0f*blendOpDest14*blendOpSrc14 + blendOpDest14*blendOpDest14*(1.0f - 2.0f*blendOpSrc14) )).rgb * ( ( ( tex2D( _DetailColor, uv_DetailColor ).rgb * (unity_ColorSpaceDouble).rgb ) * temp_output_9_0_g1 ) + appendResult16_g1 ) );
			o.Smoothness = ( tex2DNode1.a * _LocalSmooth );
			o.Occlusion = saturate( ( tex2DNode7.r + _OcclusionMult ) );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19201
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amplify Unscripted Sandstone Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-512.0231,24.70139;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;6;-428.6356,486.122;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;7;-1276.858,103.8208;Inherit;True;Property;_Occlusion;Occlusion;8;0;Create;True;0;0;0;False;0;False;-1;None;1fd7cc6979efdf6489fec275b45caa92;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-862.2357,358.1226;Inherit;True;Property;_NormalMap;Normal Map;5;0;Create;True;0;0;0;False;0;False;-1;None;4f6f164dfb9227a49a81cfd978fbbd9b;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-862.2347,558.1218;Inherit;True;Property;_DetailNormals;Detail Normals;7;0;Create;True;0;0;0;False;0;False;-1;None;4e92277bd12e548419f9ce2417d1a10e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-1191.483,-130.8253;Inherit;True;Property;_DetailColor;Detail Color;6;0;Create;True;0;0;0;False;0;False;-1;None;1413732e30f95bc40bc924fa5379d3d3;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-559.139,140.6774;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;13;-409.2764,178.5736;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-822.723,55.90123;Inherit;False;Property;_LocalSmooth;Local Smooth;10;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-829.5787,221.6369;Inherit;False;Property;_OcclusionMult;OcclusionMult;9;0;Create;True;0;0;0;False;0;False;0;0.25;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;10;-318.3031,-162.1335;Inherit;False;Detail Albedo;1;;1;29e5a290b15a7884983e27c8f1afaa8c;0;3;12;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;9;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-663.9734,-516.2827;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;4;-811.1722,-466.6819;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-931.1721,-473.0821;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1204.771,-547.0756;Inherit;False;Property;_LocalContrast;Local Contrast;12;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-1010.655,-691.8799;Inherit;False;Property;_LocalTint;Local Tint;11;0;Create;True;0;0;0;False;0;False;0.4811321,0.4811321,0.4811321,0;0.4811321,0.4811321,0.4811321,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1252.472,-380.7993;Inherit;True;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;-1;None;464ee7c4d3466b446bfc0d037e48246c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;14;-505.4232,-396.145;Inherit;False;SoftLight;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
WireConnection;0;0;10;0
WireConnection;0;1;6;0
WireConnection;0;4;2;0
WireConnection;0;5;13;0
WireConnection;2;0;1;4
WireConnection;2;1;17;0
WireConnection;6;0;8;0
WireConnection;6;1;9;0
WireConnection;9;5;7;4
WireConnection;12;0;7;1
WireConnection;12;1;18;0
WireConnection;13;0;12;0
WireConnection;10;12;14;0
WireConnection;10;11;11;0
WireConnection;10;9;7;4
WireConnection;3;0;15;0
WireConnection;3;1;4;0
WireConnection;4;0;5;0
WireConnection;5;0;16;0
WireConnection;5;1;1;1
WireConnection;14;0;1;0
WireConnection;14;1;3;0
ASEEND*/
//CHKSM=480E326996669C04D81B025A65412A3E8EB66269