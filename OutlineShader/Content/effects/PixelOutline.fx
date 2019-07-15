sampler _mainTex;
float4 _color;
float _texelSize;
float2 _frameSize;

float4 frag(float4 _colorTint : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 c = tex2D(_mainTex, coords);

    // If outline is enabled and this is not an alpha pixel, try to draw an outline.
    // Because we want this to be reusable we hackily grab the color tint (set sprite color = red/black to enable, or white to disable)
    if (_colorTint.r > 0.1 && c.a != 0)
    {

        //find frame edges from given coords
        int xFrameStart = coords.x / (_frameSize.x * _texelSize);
        int yFrameStart = coords.y / (_frameSize.y * _texelSize);
        float2 up = float2(0, -_texelSize);
        float2 down = float2(0, _texelSize);
        float2 left = float2(-_texelSize, 0);
        float2 right = float2(_texelSize, 0);
        // if this pixel is on frame edge, it is an outline pixel
        if (
            ((coords + left).x < (xFrameStart * _texelSize * _frameSize.x)) ||
            ((coords + right).x > ((xFrameStart + 1) * _texelSize * _frameSize.x)) ||
            ((coords + up).y < (yFrameStart * _texelSize * _frameSize.y)) ||
            ((coords + down).y > ((yFrameStart + 1) * _texelSize * _frameSize.y))
            )
        {
            c.rgba = float4(1, 1, 1, 1) * _color;
        }
        else
        {            
            // Get the neighbouring four pixels.
            float4 pixelUp = tex2D(_mainTex, coords + up);
            float4 pixelDown = tex2D(_mainTex, coords + down);
            float4 pixelRight = tex2D(_mainTex, coords + right);
            float4 pixelLeft = tex2D(_mainTex, coords + left);

            // If one of the neighbouring pixels is invisible, we render an outline.
            if (pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a == 0)
            {
                c.rgba = float4(1, 1, 1, 1) * _color;
            }
        }
    }

    return c;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 frag();
    }
}