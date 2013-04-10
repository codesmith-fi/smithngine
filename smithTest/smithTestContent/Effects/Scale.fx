sampler2D input : register(s0); 

float intensity = 1.0f;
float scale = 1.0f;
float rotation = 0.0f;
float2 pixelAspect = {1.0/1280, 1.0/720};

float4 PS_Color(float2 uv : TEXCOORD) : COLOR 
{ 
    float4 Color; 
	Color = tex2D( input , float2(uv.x * scale, uv.y * scale)); 
	return Color * intensity;
}

technique PostProcessScale
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PS_Color();
	}
}
