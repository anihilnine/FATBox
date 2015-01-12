#define technique technique10
#define DIRECT3D10 1

// shader 4.0 mapping
#define vs_1_1 vs_4_0
#define vs_1_3 vs_4_0
#define vs_1_4 vs_4_0
#define vs_2_0 vs_4_0
#define vs_3_0 vs_4_0
#define ps_1_1 ps_4_0
#define ps_1_3 ps_4_0
#define ps_1_4 ps_4_0
#define ps_2_0 ps_4_0
#define ps_2_a ps_4_0
#define ps_2_b ps_4_0
#define ps_3_0 ps_4_0

// workarounds for backwords compatbility issues in new d3d10 compiler
#define MipFilter Filter
#define MinFilter Filter
#define MagFilter Filter
#define NONE MIN_MAG_MIP_POINT
#define LINEAR MIN_MAG_MIP_LINEAR
#define POINT MIN_MAG_MIP_POINT

#define FIXED_FUNC_VS compile vs_4_0 FixedFuncVS()
#define FIXED_FUNC_BLOOM_VS compile vs_4_0 FixedFuncBloomVS()
#define FIXED_FUNC_PS compile ps_4_0 FixedFuncPS()

// state
#define AlphaState(p) SetBlendState( p, float4(0,0,0,0), 0xFFFFFFFF );
#define DepthState(p) SetDepthStencilState( p, 0x03 );
#define RasterizerState(p) SetRasterizerState( p );

// decals
#define decalDepthOffset (-0.00001)

// alpha test workaround for D3D10
#define d3d_LessEqual 0
#define d3d_NotEqual 1
#define d3d_Equal 2
#define d3d_Greater 3

// engine was not written for D3D10
#define CompatSwizzle(c) c.rgba = c.bgra

void AlphaTestD3D10(float inputAlpha, int alphaFunc, int alphaRef)
{
	if (alphaFunc == d3d_LessEqual)
	{
		if (inputAlpha > alphaRef / 255.0)
			discard;
	}
	else if (alphaFunc == d3d_NotEqual)
	{
		if (inputAlpha == alphaRef / 255.0)
			discard;
	}
	else if (alphaFunc == d3d_Equal)
	{
		if (inputAlpha != alphaRef / 255.0)
			discard;
	}
	else if (alphaFunc == d3d_Greater)
	{
		if (inputAlpha <= alphaRef / 255.0)
			discard;
	}
}

//
// Blend States
//
BlendState AlphaBlend_Disable
{
	BlendEnable[0] = false;

	RenderTargetWriteMask[0] = 0x0F;
	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Default_Write_RGB
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x07;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Default_Write_A
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x08;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_InvSrcAlpha_Write_RGBA
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x0F;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_One_Write_RGBA
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x0F;
	SrcBlend = SRC_ALPHA;
	DestBlend = ONE;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_One_Write_RGB
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x07;
	SrcBlend = SRC_ALPHA;
	DestBlend = ONE;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_One
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x07;
	SrcBlend = SRC_ALPHA;
	DestBlend = ONE;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_InvSrcAlpha_Write_RGB
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x07;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_InvSrcAlpha_Write_RG
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x03;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_InvSrcAlpha_Write_RBA
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x0D;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_InvSrcAlpha_Write_RGB
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x07;
	SrcBlend = ONE;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_InvSrcAlpha_Write_RGBA
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x0F;
	SrcBlend = ONE;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_InvSrcAlpha
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = INV_SRC_ALPHA;

	RenderTargetWriteMask[0] = 0x0F;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Zero_InvSrcColor
{
	BlendEnable[0] = true;
	SrcBlend = ZERO;
	DestBlend = INV_SRC_COLOR;

	RenderTargetWriteMask[0] = 0x07;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Zero_InvSrcColor_Write_RGBA
{
	BlendEnable[0] = true;
	SrcBlend = ZERO;
	DestBlend = INV_SRC_COLOR;

	RenderTargetWriteMask[0] = 0x0F;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Zero_SrcColor_Write_RGB
{
	BlendEnable[0] = true;
	SrcBlend = ZERO;
	DestBlend = SRC_COLOR;
	RenderTargetWriteMask[0] = 0x07;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_SrcAlpha_InvSrcAlpha_Write_A
{
	BlendEnable[0] = true;
	RenderTargetWriteMask[0] = 0x08;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_InvDestColor_InvSrcColor
{
	BlendEnable[0] = true;
	SrcBlend = INV_DEST_COLOR;
	DestBlend = INV_SRC_COLOR;

	RenderTargetWriteMask[0] = 0x07;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_InvDestColor_InvSrcColor_Write_RGBA
{
	BlendEnable[0] = true;
	SrcBlend = INV_DEST_COLOR;
	DestBlend = INV_SRC_COLOR;

	RenderTargetWriteMask[0] = 0x0F;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_Zero
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ZERO;

	RenderTargetWriteMask[0] = 0x0F;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_One
{
	RenderTargetWriteMask[0] = 0x07;

	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ONE;
	BlendOp = ADD;

	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_One_Write_RGB
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ONE;
	RenderTargetWriteMask[0] = 0x07;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_One_Write_RGBA
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ONE;
	RenderTargetWriteMask[0] = 0x0F;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_One_Write_A
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ONE;
	RenderTargetWriteMask[0] = 0x08;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_One_Zero_Write_A
{
	BlendEnable[0] = true;
	SrcBlend = ONE;
	DestBlend = ZERO;
	RenderTargetWriteMask[0] = 0x08;

	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Write_A
{
	RenderTargetWriteMask[0] = 0x08;
	BlendEnable[0] = true;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Write_RGB
{
	RenderTargetWriteMask[0] = 0x07;
	BlendEnable[0] = true;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Write_RGBA
{
	RenderTargetWriteMask[0] = 0x0F;
	BlendEnable[0] = true;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_A
{
	RenderTargetWriteMask[0] = 0x08;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_RG
{
	RenderTargetWriteMask[0] = 0x03;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_BA
{
	RenderTargetWriteMask[0] = 0x0C;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_RGB
{
	RenderTargetWriteMask[0] = 0x07;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_RGBA
{
	RenderTargetWriteMask[0] = 0x0F;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};

BlendState AlphaBlend_Disable_Write_None
{
	RenderTargetWriteMask[0] = 0x00;
	BlendEnable[0] = false;

	SrcBlend = ONE;
	DestBlend = ZERO;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
};



DepthStencilState Depth_Disable
{
	DepthEnable = FALSE;

	DepthWriteMask = ALL;
	DepthFunc = LESS_EQUAL;

	StencilEnable = FALSE;
#if 0
	StencilReadMask = 0;
	StencilWriteMask = 0;
	FrontFaceStencilFail = KEEP;
	FrontFaceStencilDepthFail = KEEP;
	FrontFaceStencilPass = KEEP;
	FrontFaceStencilFunc = ALWAYS;
	BackFaceStencilFail = KEEP;
	BackFaceStencilDepthFail = KEEP;
	BackFaceStencilPass = KEEP;
	BackFaceStencilFunc = ALWAYS;
#endif
};

RasterizerState Rasterizer_Cull_None
{
	FillMode = SOLID;
	CullMode = None;
	FrontCounterClockwise = FALSE;
	DepthBias = 0;
	DepthBiasClamp = 0;
	SlopeScaledDepthBias = 0;
	DepthClipEnable = FALSE;
	ScissorEnable = FALSE;
	MultisampleEnable = TRUE;
	AntialiasedLineEnable = FALSE;
};


////////////////////////
////////////////////////
////////////////////////
////////////////////////
////////////////////////
////////////////////////
////////////////////////
////////////////////////
////////////////////////

float4x4 CompositeMatrix;
texture Texture1;


sampler PointSampler = sampler_state
{
    Texture = (Texture1);
    MipFilter = POINT;
    MinFilter = POINT;
    MagFilter = POINT;
    AddressU  = Clamp;
    AddressV  = Clamp;
};


struct VS_OUTPUT
{
    float4 Pos  : POSITION;
    float4 Color : COLOR0;
    float2 Tex1  : TEXCOORD0;
};


VS_OUTPUT PrimBatcherVS(
    float3 Pos  : POSITION,
    float4 Color : COLOR0,
    float2 Tex  : TEXCOORD0 )
{
    VS_OUTPUT Out;
    CompatSwizzle(Color);
	/*CompositeMatrix[0, 0] = 0.001;
	CompositeMatrix[0, 1] = 0;
	CompositeMatrix[0, 2] = 0;
	CompositeMatrix[0, 3] = -1.001;

	CompositeMatrix[1, 0] = 0;
	CompositeMatrix[1, 1] = -0.002;
	CompositeMatrix[1, 2] = 0;
	CompositeMatrix[1, 3] = 1.001;

	CompositeMatrix[2, 0] = 0;
	CompositeMatrix[2, 1] = 0;
	CompositeMatrix[2, 2] = -0.5;
	CompositeMatrix[2, 3] = 0.5;

	CompositeMatrix[3, 0] = 0;
	CompositeMatrix[3, 1] = 0;
	CompositeMatrix[3, 2] = 0;
	CompositeMatrix[3, 3] = 1.0;*/

		//Out.Pos = float4(Pos, 1);
    Out.Pos = mul(float4(Pos,1), CompositeMatrix);
	//Out.Pos = float4(Pos, 1);
    Out.Color = Color;
    Out.Tex1 = Tex;

    return Out;
}



float4 StrategicIconPS(
    float4 Pos : POSITION,
    float4 Diff : COLOR0,
    float2 Tex1  : TEXCOORD0) : COLOR
{
	//return float4(1, 0, 0, 1);

    float4 color = tex2D(PointSampler, Tex1);
    float3 d = (color.rgb-float3(0.5,0.5,0.5)) * (color.rgb-float3(0.5,0.5,0.5));
    if( dot(d, float3(1,1,1)) < (0.25) )
        color.rgb = Diff.rgb;
    return color;
}


technique TStrategicIcon
{
    pass P0
    {
        AlphaState( AlphaBlend_SrcAlpha_InvSrcAlpha_Write_RGB )
        DepthState( Depth_Disable )
        RasterizerState( Rasterizer_Cull_None )

#ifndef DIRECT3D10
        AlphaTestEnable = true;
        AlphaRef = 0;
        AlphaFunc = Greater;
#endif

        VertexShader = compile vs_1_1 PrimBatcherVS();
        PixelShader = compile ps_2_0 StrategicIconPS();
    }
}
