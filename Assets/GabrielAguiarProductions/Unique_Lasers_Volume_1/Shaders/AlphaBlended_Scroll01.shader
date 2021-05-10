// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33174,y:32647,varname:node_4795,prsc:2|emission-2393-OUT,custl-2393-OUT,alpha-5350-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32180,y:32512,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:10ff1f268ba06f7429f974521abe4b42,ntxv:0,isnm:False|UVIN-9664-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32870,y:32709,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-914-OUT,D-9248-OUT,E-9633-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.6235296,c3:0.1470588,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Time,id:9160,x:31342,y:33199,varname:node_9160,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4567,x:31144,y:33377,ptovrint:False,ptlb:Gradient U Speed,ptin:_GradientUSpeed,varname:_USpeedMain,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:4497,x:31144,y:33449,ptovrint:False,ptlb:Gradient V Speed,ptin:_GradientVSpeed,varname:_VSpeedMain,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:8618,x:31342,y:33377,varname:node_8618,prsc:2|A-4567-OUT,B-4497-OUT;n:type:ShaderForge.SFN_Multiply,id:9458,x:31597,y:33281,varname:node_9458,prsc:2|A-9160-T,B-8618-OUT;n:type:ShaderForge.SFN_Tex2d,id:4069,x:32173,y:33219,ptovrint:False,ptlb:Gradient,ptin:_Gradient,varname:_Gradient,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-6774-OUT;n:type:ShaderForge.SFN_Multiply,id:9633,x:32638,y:33219,varname:node_9633,prsc:2|A-6074-A,B-5108-OUT,C-4289-A;n:type:ShaderForge.SFN_Slider,id:1571,x:31185,y:33036,ptovrint:False,ptlb:Noise Amount,ptin:_NoiseAmount,varname:node_1571,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.1,max:1;n:type:ShaderForge.SFN_Lerp,id:405,x:31596,y:32916,varname:node_405,prsc:2|A-3523-UVOUT,B-2788-R,T-1571-OUT;n:type:ShaderForge.SFN_TexCoord,id:3523,x:31185,y:32633,varname:node_3523,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2788,x:31185,y:32819,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:_Gradient_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:82f4b06147155c54da475b309b9e24fa,ntxv:0,isnm:False|UVIN-2765-OUT;n:type:ShaderForge.SFN_Add,id:6774,x:31949,y:33209,varname:node_6774,prsc:2|A-405-OUT,B-9458-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:9664,x:31939,y:32621,ptovrint:False,ptlb:Distort Main Texture,ptin:_DistortMainTexture,varname:node_9664,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-3523-UVOUT,B-405-OUT;n:type:ShaderForge.SFN_Multiply,id:4009,x:30781,y:32820,varname:node_4009,prsc:2|A-7842-T,B-9706-OUT;n:type:ShaderForge.SFN_Append,id:9706,x:30570,y:32903,varname:node_9706,prsc:2|A-8025-OUT,B-7472-OUT;n:type:ShaderForge.SFN_Time,id:7842,x:30570,y:32722,varname:node_7842,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8025,x:30372,y:32903,ptovrint:False,ptlb:Distortion U Speed,ptin:_DistortionUSpeed,varname:_GradientUSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:7472,x:30372,y:32975,ptovrint:False,ptlb:Distortion V Speed,ptin:_DistortionVSpeed,varname:_GradientVSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Add,id:2765,x:30981,y:32819,varname:node_2765,prsc:2|A-3523-UVOUT,B-4009-OUT;n:type:ShaderForge.SFN_Multiply,id:5350,x:32870,y:32908,varname:node_5350,prsc:2|A-2053-A,B-797-A,C-6074-A,D-4289-A;n:type:ShaderForge.SFN_Tex2d,id:4289,x:32638,y:33389,ptovrint:False,ptlb:MainTexMask,ptin:_MainTexMask,varname:_DistortionMask_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:22,x:32397,y:33470,varname:node_22,prsc:2|VAL-4069-RGB,EXP-4674-OUT;n:type:ShaderForge.SFN_Slider,id:4674,x:32043,y:33474,ptovrint:False,ptlb:Gradient Power,ptin:_GradientPower,varname:node_2998,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:50;n:type:ShaderForge.SFN_Multiply,id:5108,x:32397,y:33219,varname:node_5108,prsc:2|A-4069-RGB,B-22-OUT;n:type:ShaderForge.SFN_Multiply,id:914,x:32638,y:32976,varname:node_914,prsc:2|A-797-RGB,B-1441-OUT;n:type:ShaderForge.SFN_Slider,id:1441,x:32344,y:33081,ptovrint:False,ptlb:Color Multiplier,ptin:_ColorMultiplier,varname:node_1441,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;proporder:797-1441-9664-6074-4674-4567-4497-4069-1571-8025-7472-2788-4289;pass:END;sub:END;*/

Shader "Shader Forge/AlphaBlended_Scroll01" {
    Properties {
        _TintColor ("Color", Color) = (1,0.6235296,0.1470588,1)
        _ColorMultiplier ("Color Multiplier", Range(0, 10)) = 1
        [MaterialToggle] _DistortMainTexture ("Distort Main Texture", Float ) = 0
        _MainTex ("MainTex", 2D) = "white" {}
        _GradientPower ("Gradient Power", Range(0, 50)) = 0
        _GradientUSpeed ("Gradient U Speed", Float ) = 0.1
        _GradientVSpeed ("Gradient V Speed", Float ) = 0.1
        _Gradient ("Gradient", 2D) = "white" {}
        _NoiseAmount ("Noise Amount", Range(-1, 1)) = 0.1
        _DistortionUSpeed ("Distortion U Speed", Float ) = 0.1
        _DistortionVSpeed ("Distortion V Speed", Float ) = 0.1
        _Distortion ("Distortion", 2D) = "white" {}
        _MainTexMask ("MainTexMask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _GradientUSpeed;
            uniform float _GradientVSpeed;
            uniform sampler2D _Gradient; uniform float4 _Gradient_ST;
            uniform float _NoiseAmount;
            uniform sampler2D _Distortion; uniform float4 _Distortion_ST;
            uniform fixed _DistortMainTexture;
            uniform float _DistortionUSpeed;
            uniform float _DistortionVSpeed;
            uniform sampler2D _MainTexMask; uniform float4 _MainTexMask_ST;
            uniform float _GradientPower;
            uniform float _ColorMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7842 = _Time;
                float2 node_2765 = (i.uv0+(node_7842.g*float2(_DistortionUSpeed,_DistortionVSpeed)));
                float4 _Distortion_var = tex2D(_Distortion,TRANSFORM_TEX(node_2765, _Distortion));
                float2 node_405 = lerp(i.uv0,float2(_Distortion_var.r,_Distortion_var.r),_NoiseAmount);
                float2 _DistortMainTexture_var = lerp( i.uv0, node_405, _DistortMainTexture );
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(_DistortMainTexture_var, _MainTex));
                float4 node_9160 = _Time;
                float2 node_6774 = (node_405+(node_9160.g*float2(_GradientUSpeed,_GradientVSpeed)));
                float4 _Gradient_var = tex2D(_Gradient,TRANSFORM_TEX(node_6774, _Gradient));
                float4 _MainTexMask_var = tex2D(_MainTexMask,TRANSFORM_TEX(i.uv0, _MainTexMask));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*(_TintColor.rgb*_ColorMultiplier)*2.0*(_MainTex_var.a*(_Gradient_var.rgb*pow(_Gradient_var.rgb,_GradientPower))*_MainTexMask_var.a));
                float3 finalColor = emissive + (_MainTex_var.rgb*i.vertexColor.rgb*(_TintColor.rgb*_ColorMultiplier)*2.0*(_MainTex_var.a*(_Gradient_var.rgb*pow(_Gradient_var.rgb,_GradientPower))*_MainTexMask_var.a));
                return fixed4(finalColor,(i.vertexColor.a*_TintColor.a*_MainTex_var.a*_MainTexMask_var.a));
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
