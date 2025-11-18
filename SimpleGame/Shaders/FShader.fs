#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform sampler2D u_RGBTesture;
uniform float u_Time;

in vec2 vTexPos;

const float c_PI = 3.141592;

void test()
{
	vec2 newUV = v_UV;
    float dx = 0.1 * sin(newUV.y *2*c_PI - u_Time);
    float dy = 0.1 * sin(newUV.x *2*c_PI - u_Time);
    newUV += vec2(dx,dy);
    vec4 newColor = texture(u_RGBTesture, newUV);
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.02,newUV.y));
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.04,newUV.y));
    newColor /=3;
    FragColor = newColor;

}

void Circle(){
	vec2 newUV = v_UV;//0~1
	vec2 center = vec2(0.5,0.5);
	float d = distance(newUV, center);
	vec4 newColor = vec4(0);

    float value = sin(d*4*c_PI- u_Time);
    newColor = vec4(value);

	FragColor = newColor;
}

void Flag()
{
    vec2 newUV = v_UV;
    vec4 newColor = vec4(0);

    float width = 0.2;
    float sinValue = sin(v_UV.x*2*c_PI);

    if(v_UV.y < sinValue + width){
        newColor = vec4(1);
    }
}

void main()
{
    Flag();

}
