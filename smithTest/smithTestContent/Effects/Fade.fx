sampler2D input : register(s0); 

float intensity = 1.0f;

float4 PS_Color(float2 uv : TEXCOORD) : COLOR 
{ 
    float4 Color; 
	Color = tex2D( input , uv.xy); 
	return Color * intensity;
}

technique PostProcessWiggle
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PS_Color();
	}
}
