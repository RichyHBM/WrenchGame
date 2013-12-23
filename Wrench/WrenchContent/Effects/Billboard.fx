float4x4 World;
float4x4 View;
float4x4 Projection;
float FogStart;
float FogEnd;
float4 FogColor;
bool FogEnabled;
texture Texture;

sampler2D texSampler = sampler_state
{
	texture = < Texture >;
};


struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0 ;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float FogFactor : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    input.Position.w = 1.0f;
    
	float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	output.UV = input.UV;
	 
    output.FogFactor = saturate((FogEnd - output.Position.z) / (FogEnd - FogStart));
    
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 texColor = tex2D( texSampler, input.UV ) ;
	clip(texColor.a - 0.2f );
    
	float4 finalColor = texColor;
	if(FogEnabled)
		finalColor = input.FogFactor * texColor + (1.0 - input.FogFactor) * FogColor;
	return finalColor;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
