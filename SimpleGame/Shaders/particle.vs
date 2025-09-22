#version 330

in vec3 a_Position;
in float a_Radius;
in vec4 a_Color;


out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;

void main()
{
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	
	float jinja = -1 + 2*fract(u_Time);
	float y = (jinja+1)*PI;
	newPosition.xy = newPosition.xy + vec2(a_Radius*cos(y),a_Radius*sin(y));
	gl_Position = newPosition;

	v_Color = a_Color;
}
 