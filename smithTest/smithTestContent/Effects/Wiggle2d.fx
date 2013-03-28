
// Global variables
// This will use the texture bound to the object( like from the sprite batch ).
sampler ColorMapSampler : register(s0);

//A timer we can use for whatever purpose we want
float timer;
float intensity = 1.0f;
float colorIntensity = 1.0f;

struct PixelShaderInput
{
    float4 TexCoord: TEXCOORD0;
};

float4 PixelShaderFunction(PixelShaderInput input) : COLOR
{
	// Use the timer to move the texture coordinated before using them to lookup
	// in the ColorMapSampler. This makes the scene look like its underwater
	// or something similar :)

	input.TexCoord.x += intensity * ( sin( timer + input.TexCoord.x * 10) * 0.01f );
	input.TexCoord.y += intensity * ( cos( timer + input.TexCoord.y * 10) * 0.01f );
	float4 Color = tex2D( ColorMapSampler, input.TexCoord );		
    return Color * colorIntensity;
}

technique PostProcessWiggle
{
	pass Pass1
	{
		// A post process shader only needs a pixel shader.
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
