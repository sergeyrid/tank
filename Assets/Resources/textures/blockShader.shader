Shader "Custom/Test"
{
    Properties
    {
        [PerRendererData]
        _Color ("Color" , Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _AlphaTex("Alpha Texture", 2D) = "white" {}
        _State("Block State", Int) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        
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
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            int _State;
            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _AlphaTex;             

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {                           
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 aColor = fixed4(0,0,0,1);

                switch(_State)
                {
                    case 0:
                    {
                        aColor = tex2D(_AlphaTex, i.uv)*i.color;
                        break;
                    }

                    case 1:
                    {
                        if(i.uv.r > 0.5 && i.uv.g < 0.5)
                            aColor = tex2D(_AlphaTex, i.uv)*i.color;
                        else
                        {
                            if(i.uv.g >= 0.0625 && i.uv.r <= 1-0.0625)
                                aColor = 1;
                        }
                        break;
                    }

                    case 2:
                    {
                        if(i.uv.g >= 0.0625)
                        {
                             aColor = 1;
                        }
                        break;
                    }

                    case 3:
                    {
                        if(i.uv.r <= 0.5 && i.uv.g < 0.5)
                            aColor = tex2D(_AlphaTex, i.uv)*i.color;
                        else
                        {
                            if(i.uv.g >= 0.0625 && i.uv.r > 0.0625)
                                aColor = 1;
                        }
                        break;
                    }

                    case 4:
                    {
                        if(i.uv.r > 0.0625)
                        {
                            aColor = 1;
                        }
                        break;
                    }

                    case 5:
                    {
                        if(i.uv.r <= 0.5 && i.uv.g >= 0.5)
                            aColor = tex2D(_AlphaTex, i.uv)*i.color;
                        else
                        {
                            if(i.uv.g < 1-0.0625 && i.uv.r > 0.0625)
                                aColor = 1;
                        }
                        break;
                    }

                    case 6:
                    {
                        if(i.uv.g < 1-0.0625)
                        {
                             aColor = 1;
                        }
                        break;
                    }

                    case 7:
                    {
                        if(i.uv.r > 0.5 && i.uv.g >= 0.5)
                            aColor = tex2D(_AlphaTex, i.uv)*i.color;
                        else
                        {
                            if(i.uv.g < 1-0.0625 && i.uv.r <= 1-0.0625)
                                aColor = 1;
                        }
                        break;
                    }

                    case 8:
                    {
                        if(i.uv.r <= 1-0.0625)
                        {
                            aColor = 1;
                        }
                        break;
                    }

                    default:
                    {
                        aColor = 1;
                        break;
                    }
                }
                return fixed4(texColor.r, texColor.g, texColor.b, aColor.r);
            }
            ENDCG
        }
    }
}