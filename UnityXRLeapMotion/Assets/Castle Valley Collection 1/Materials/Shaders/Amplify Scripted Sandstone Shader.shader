// Made with Amplify Shader Editor v1.9.2.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify Scripted Sandstone Shader"
{
	Properties
	{
		_Color("Color", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_DetailColor("Detail Color", 2D) = "gray" {}
		_DetailNormals("Detail Normals", 2D) = "bump" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_OcclusionMult("OcclusionMult", Range( 0 , 1)) = 0
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
		uniform float4 GlobalTint;
		uniform float GlobalContrast;
		uniform sampler2D _DetailColor;
		uniform float4 _DetailColor_ST;
		uniform float GlobalSmooth;
		uniform float _OcclusionMult;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float2 uv_DetailNormals = i.uv_texcoord * _DetailNormals_ST.xy + _DetailNormals_ST.zw;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			float4 tex2DNode3 = tex2D( _Occlusion, uv_Occlusion );
			o.Normal = BlendNormals( UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) ) , UnpackScaleNormal( tex2D( _DetailNormals, uv_DetailNormals ), tex2DNode3.a ) );
			float2 uv_Color = i.uv_texcoord * _Color_ST.xy + _Color_ST.zw;
			float4 tex2DNode1 = tex2D( _Color, uv_Color );
			float4 blendOpSrc12 = tex2DNode1;
			float4 blendOpDest12 = ( GlobalTint * saturate( ( GlobalContrast + tex2DNode1.r ) ) );
			float2 uv_DetailColor = i.uv_texcoord * _DetailColor_ST.xy + _DetailColor_ST.zw;
			float temp_output_9_0_g1 = tex2DNode3.a;
			float temp_output_18_0_g1 = ( 1.0 - temp_output_9_0_g1 );
			float3 appendResult16_g1 = (float3(temp_output_18_0_g1 , temp_output_18_0_g1 , temp_output_18_0_g1));
			o.Albedo = ( ( saturate( 2.0f*blendOpDest12*blendOpSrc12 + blendOpDest12*blendOpDest12*(1.0f - 2.0f*blendOpSrc12) )).rgb * ( ( ( tex2D( _DetailColor, uv_DetailColor ).rgb * (unity_ColorSpaceDouble).rgb ) * temp_output_9_0_g1 ) + appendResult16_g1 ) );
			o.Smoothness = ( tex2DNode1.a * GlobalSmooth );
			o.Occlusion = saturate( ( tex2DNode3.g + _OcclusionMult ) );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19201
Node;AmplifyShaderEditor.SamplerNode;1;-1158.066,-547.4333;Inherit;True;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;-1;None;464ee7c4d3466b446bfc0d037e48246c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-485.3964,-201.7383;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;7;-402.0087,259.6823;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;3;-1250.232,-122.6189;Inherit;True;Property;_Occlusion;Occlusion;8;0;Create;True;0;0;0;False;0;False;-1;None;1fd7cc6979efdf6489fec275b45caa92;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-835.6093,131.6829;Inherit;True;Property;_NormalMap;Normal Map;5;0;Create;True;0;0;0;False;0;False;-1;None;4f6f164dfb9227a49a81cfd978fbbd9b;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-835.6083,331.6822;Inherit;True;Property;_DetailNormals;Detail Normals;7;0;Create;True;0;0;0;False;0;False;-1;None;4e92277bd12e548419f9ce2417d1a10e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1164.857,-357.265;Inherit;True;Property;_DetailColor;Detail Color;6;0;Create;True;0;0;0;False;0;False;-1;None;1413732e30f95bc40bc924fa5379d3d3;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-532.5123,-85.76235;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;25;-382.6495,-47.86606;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-796.0967,-170.5385;Inherit;False;Global;GlobalSmooth;Global Smooth;9;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-802.9524,-4.802772;Inherit;False;Property;_OcclusionMult;OcclusionMult;9;0;Create;True;0;0;0;False;0;False;0;0.25;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;12;-388.8988,-551.1887;Inherit;False;SoftLight;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;4;-154.9727,-391.0197;Inherit;False;Detail Albedo;1;;1;29e5a290b15a7884983e27c8f1afaa8c;0;3;12;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;9;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-569.5832,-690.5938;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;19;-716.7822,-640.9928;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-836.7822,-647.393;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;13;-916.2656,-866.1907;Inherit;False;Global;GlobalTint;Global Tint;10;0;Create;True;0;0;0;False;0;False;0.4811321,0.4811321,0.4811321,0;0.4779873,0.8117452,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-1110.382,-719.3933;Inherit;False;Global;GlobalContrast;Global Contrast;11;0;Create;True;0;0;0;False;0;False;0;-0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;163.3862,-1.738151;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Amplify Scripted Sandstone Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;1;4
WireConnection;10;1;11;0
WireConnection;7;0;2;0
WireConnection;7;1;8;0
WireConnection;8;5;3;4
WireConnection;24;0;3;2
WireConnection;24;1;22;0
WireConnection;25;0;24;0
WireConnection;12;0;1;0
WireConnection;12;1;16;0
WireConnection;4;12;12;0
WireConnection;4;11;5;0
WireConnection;4;9;3;4
WireConnection;16;0;13;0
WireConnection;16;1;19;0
WireConnection;19;0;18;0
WireConnection;18;0;20;0
WireConnection;18;1;1;1
WireConnection;0;0;4;0
WireConnection;0;1;7;0
WireConnection;0;4;10;0
WireConnection;0;5;25;0
ASEEND*/
//CHKSM=491FE4B24C0D05E9516EC9BB5EC5811A560F2527