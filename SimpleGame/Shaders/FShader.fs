#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform sampler2D u_RGBTesture;
uniform float u_Time;

in vec2 vTexPos;

const float c_PI = 3.141592;

void main()
{
    vec2 newUV = v_UV;
    float dx = 0;
    float dy = 0;
    newUV += vec2(dx,dy);
    vec4 newColor = texture(u_RGBTesture, newUV);
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.02,newUV.y));
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.04,newUV.y));
    newColor /=3;
    FragColor = newColor;

}
