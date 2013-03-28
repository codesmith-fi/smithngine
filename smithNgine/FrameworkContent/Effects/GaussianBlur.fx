sampler2D input : register(s0); 

#define WEIGHT_COUNT 4

float weight[WEIGHT_COUNT] = {
    0.8,
    0.65,
    0.4,
    0.25
	};

float colorIntensity = 1.0f;
float intensity = 1.0f;
float2 pixelAspect = {1.0/1280, 1.0/768};

float4 PS_BlurHorizontal(in float2 uv : TEXCOORD) : COLOR 
{ 
    float4 Color = tex2D(input, uv);
	float mult = 1;
	for(int i=0; i<WEIGHT_COUNT; i++)
	{
		Color += tex2D(input, float2(uv.x-(intensity*pixelAspect.x*mult), uv.y)) * weight[i];
		Color += tex2D(input, float2(uv.x+(intensity*pixelAspect.x*mult), uv.y)) * weight[i];
		mult = mult + 4;
	}
	Color /= WEIGHT_COUNT;
	return Color * colorIntensity; 
}

float4 PS_BlurVertical(in float2 uv : TEXCOORD) : COLOR
{ 
	float4 Color = tex2D(input, uv);
 	float mult = 1;
	for(int i=0; i<WEIGHT_COUNT; i++)
	{
		Color += tex2D(input, float2(uv.x, uv.y-(intensity*pixelAspect.y*mult))) * weight[i];
		Color += tex2D(input, float2(uv.x, uv.y+(intensity*pixelAspect.y*mult))) * weight[i];
		mult = mult + 4;
	}
	Color /= WEIGHT_COUNT;
	return Color * colorIntensity;
}

technique GaussianBlur
{
	pass Pasinput
	{
		// A post process shader only needs a pixel shader.
		PixelShader = compile ps_2_0 PS_BlurHorizontal();
	}
	pass Pass2
	{
		// A post process shader only needs a pixel shader.
		PixelShader = compile ps_2_0 PS_BlurVertical();
	}
}
