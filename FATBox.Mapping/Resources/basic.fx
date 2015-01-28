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


BlendState AlphaBlend_SrcAlpha_INVSRCALPHA_Write_RGBA
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

//
// DepthStencilStates
//

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


//
// RasterizerStates
//


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


//
// End
//


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
    float2 Tex1  : TEXCOORD0;
};


VS_OUTPUT MyVs(
    float3 Pos  : POSITION,
    float2 Tex  : TEXCOORD0 )
{
    VS_OUTPUT Out;

    Out.Pos = mul(float4(Pos,1), CompositeMatrix);
    Out.Tex1 = Tex;

    return Out;
}



float4 MyPs(
    float4 Pos : POSITION,
    float2 Tex1  : TEXCOORD0) : COLOR
{

	float4 color = tex2D(PointSampler, Tex1);
    return color;
}


technique TMyTechnique
{
    pass P0
    {
		AlphaState(AlphaBlend_SrcAlpha_INVSRCALPHA_Write_RGBA)
        DepthState( Depth_Disable )
        RasterizerState( Rasterizer_Cull_None )

#ifndef DIRECT3D10
        AlphaTestEnable = true;
        AlphaRef = 0;
        AlphaFunc = Greater;
#endif

        VertexShader = compile vs_1_1 MyVs();
        PixelShader = compile ps_2_0 MyPs();
    }
}
