// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33454,y:32568,varname:node_4795,prsc:2|normal-2270-OUT,alpha-9285-OUT,refract-6888-OUT;n:type:ShaderForge.SFN_TexCoord,id:8024,x:31256,y:32353,varname:node_8024,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:9553,x:31272,y:32744,varname:node_9553,prsc:2|UVIN-8024-UVOUT,SPD-3861-OUT;n:type:ShaderForge.SFN_Slider,id:3861,x:30875,y:32815,ptovrint:False,ptlb:Rotation Speed,ptin:_RotationSpeed,varname:node_3861,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:-1,max:10;n:type:ShaderForge.SFN_Tex2d,id:1826,x:31983,y:32567,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_1826,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4ee836b353abed8428dbfa958a957ed8,ntxv:0,isnm:False|UVIN-8822-OUT;n:type:ShaderForge.SFN_Slider,id:9168,x:31826,y:32777,ptovrint:False,ptlb:Normal Intensity,ptin:_NormalIntensity,varname:node_9168,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-3,cur:1,max:3;n:type:ShaderForge.SFN_Vector3,id:2678,x:31983,y:32415,varname:node_2678,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:2270,x:32219,y:32567,varname:node_2270,prsc:2|A-2678-OUT,B-1826-RGB,T-9168-OUT;n:type:ShaderForge.SFN_Slider,id:2736,x:31823,y:32895,ptovrint:False,ptlb:Distortion Intensity,ptin:_DistortionIntensity,varname:node_2736,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0.1,max:0.5;n:type:ShaderForge.SFN_Multiply,id:5434,x:32236,y:32911,varname:node_5434,prsc:2|A-9168-OUT,B-2736-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9748,x:32236,y:32724,varname:node_9748,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1826-RGB;n:type:ShaderForge.SFN_Multiply,id:4065,x:32497,y:32788,varname:node_4065,prsc:2|A-9748-OUT,B-5434-OUT;n:type:ShaderForge.SFN_Tex2d,id:9951,x:32481,y:32976,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:82f4b06147155c54da475b309b9e24fa,ntxv:2,isnm:False|UVIN-8107-OUT;n:type:ShaderForge.SFN_Multiply,id:9285,x:32930,y:32669,varname:node_9285,prsc:2|A-8513-OUT,B-9951-R;n:type:ShaderForge.SFN_Slider,id:8513,x:32463,y:32667,ptovrint:False,ptlb:Opacity Value,ptin:_OpacityValue,varname:node_8513,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_ComponentMask,id:5229,x:32708,y:33059,varname:node_5229,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9951-R;n:type:ShaderForge.SFN_Multiply,id:4857,x:32930,y:32928,varname:node_4857,prsc:2|A-4065-OUT,B-5229-OUT;n:type:ShaderForge.SFN_VertexColor,id:8182,x:32708,y:33251,varname:node_8182,prsc:2;n:type:ShaderForge.SFN_Color,id:4332,x:32708,y:33420,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4332,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5526,x:32936,y:33225,varname:node_5526,prsc:2|A-8182-A,B-4332-A;n:type:ShaderForge.SFN_Multiply,id:815,x:33154,y:32991,varname:node_815,prsc:2|A-4857-OUT,B-5526-OUT;n:type:ShaderForge.SFN_Time,id:4162,x:30748,y:32412,varname:node_4162,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9493,x:30976,y:32495,varname:node_9493,prsc:2|A-4162-T,B-5558-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2867,x:30594,y:32572,ptovrint:False,ptlb:Normal U Speed,ptin:_NormalUSpeed,varname:node_2867,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:9437,x:30594,y:32647,ptovrint:False,ptlb:Normal V Speed,ptin:_NormalVSpeed,varname:node_9437,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:3824,x:31256,y:32556,varname:node_3824,prsc:2|A-8024-UVOUT,B-9493-OUT;n:type:ShaderForge.SFN_Append,id:5558,x:30748,y:32604,varname:node_5558,prsc:2|A-2867-OUT,B-9437-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:8822,x:31689,y:32529,ptovrint:False,ptlb:RotateOrMove,ptin:_RotateOrMove,varname:node_8822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-3824-OUT,B-9553-UVOUT;n:type:ShaderForge.SFN_Time,id:9959,x:31804,y:33206,varname:node_9959,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3603,x:32032,y:33289,varname:node_3603,prsc:2|A-9959-T,B-5986-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3075,x:31650,y:33366,ptovrint:False,ptlb:Opacity U Speed,ptin:_OpacityUSpeed,varname:_NormalUSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:9261,x:31650,y:33441,ptovrint:False,ptlb:Opacity V Speed,ptin:_OpacityVSpeed,varname:_NormalVSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:5986,x:31804,y:33398,varname:node_5986,prsc:2|A-3075-OUT,B-9261-OUT;n:type:ShaderForge.SFN_Add,id:8107,x:32238,y:33225,varname:node_8107,prsc:2|A-3919-UVOUT,B-3603-OUT;n:type:ShaderForge.SFN_TexCoord,id:3919,x:32032,y:33129,varname:node_3919,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6888,x:32954,y:32312,varname:node_6888,prsc:2|A-3515-A,B-815-OUT;n:type:ShaderForge.SFN_Tex2d,id:3515,x:32507,y:32210,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_DistortionMask_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3a2a74664dda6f44b5aaaccac83bbfc,ntxv:2,isnm:False;proporder:4332-8822-1826-2736-9168-2867-9437-3861-9951-8513-3075-9261-3515;pass:END;sub:END;*/

Shader "Shader Forge/DistortionScroll01" {
    Properties {
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _RotateOrMove ("RotateOrMove", Float ) = 0.229402
        _NormalMap ("Normal Map", 2D) = "white" {}
        _DistortionIntensity ("Distortion Intensity", Range(-0.5, 0.5)) = 0.1
        _NormalIntensity ("Normal Intensity", Range(-3, 3)) = 1
        _NormalUSpeed ("Normal U Speed", Float ) = 0.1
        _NormalVSpeed ("Normal V Speed", Float ) = 0
        _RotationSpeed ("Rotation Speed", Range(-10, 10)) = -1
        _Opacity ("Opacity", 2D) = "black" {}
        _OpacityValue ("Opacity Value", Range(-5, 5)) = 0
        _OpacityUSpeed ("Opacity U Speed", Float ) = 0.1
        _OpacityVSpeed ("Opacity V Speed", Float ) = 0
        _Mask ("Mask", 2D) = "black" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _GrabTexture;
            uniform float _RotationSpeed;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _NormalIntensity;
            uniform float _DistortionIntensity;
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            uniform float _OpacityValue;
            uniform float4 _Color;
            uniform float _NormalUSpeed;
            uniform float _NormalVSpeed;
            uniform fixed _RotateOrMove;
            uniform float _OpacityUSpeed;
            uniform float _OpacityVSpeed;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float4 node_4162 = _Time;
                float4 node_1205 = _Time;
                float node_9553_ang = node_1205.g;
                float node_9553_spd = _RotationSpeed;
                float node_9553_cos = cos(node_9553_spd*node_9553_ang);
                float node_9553_sin = sin(node_9553_spd*node_9553_ang);
                float2 node_9553_piv = float2(0.5,0.5);
                float2 node_9553 = (mul(i.uv0-node_9553_piv,float2x2( node_9553_cos, -node_9553_sin, node_9553_sin, node_9553_cos))+node_9553_piv);
                float2 _RotateOrMove_var = lerp( (i.uv0+(node_4162.g*float2(_NormalUSpeed,_NormalVSpeed))), node_9553, _RotateOrMove );
                float4 _NormalMap_var = tex2D(_NormalMap,TRANSFORM_TEX(_RotateOrMove_var, _NormalMap));
                float3 normalLocal = lerp(float3(0,0,1),_NormalMap_var.rgb,_NormalIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_9959 = _Time;
                float2 node_8107 = (i.uv0+(node_9959.g*float2(_OpacityUSpeed,_OpacityVSpeed)));
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(node_8107, _Opacity));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (_Mask_var.a*(((_NormalMap_var.rgb.rg*(_NormalIntensity*_DistortionIntensity))*_Opacity_var.r.r)*(i.vertexColor.a*_Color.a)));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(_OpacityValue*_Opacity_var.r)),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
