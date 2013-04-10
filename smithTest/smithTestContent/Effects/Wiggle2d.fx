sampler ColorMapSampler : register(s0);

float timer;
float intensity = 1.0f;
float colorIntensity = 1.0f;

struct PixelShaderInput
{
    float4 TexCoord: TEXCOORD0;
};

float4 PixelShaderFunction(PixelShaderInput input) : COLOR
{
	// let's take pixels with circle motion around the current pixel, 
	// causing the screen to wobble
	input.TexCoord.x += ( sin( timer + input.TexCoord.x * 30 * intensity) * 0.01f );
	input.TexCoord.y += ( cos( timer + input.TexCoord.y * 30 * intensity) * 0.01f );
	float4 Color = tex2D( ColorMapSampler, input.TexCoord );		
    return Color * colorIntensity;
}

technique PostProcessWiggle
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
