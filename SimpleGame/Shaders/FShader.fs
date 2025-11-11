#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform vec4 u_Color;

void main()
{
	FragColor = vec4(v_UV,0,1);

}
