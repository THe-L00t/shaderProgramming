#version 330

in vec3 a_Position;
in vec4 a_Color;

out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;

void main()
{
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	//vec4 newPosition = vec4(a_Position,1);
	
	float jinja = -1 + 2*fract(u_Time);
	float y = (jinja+1)*PI;
	newPosition.xy = newPosition.xy + vec2(u_Time*cos(y),u_Time*sin(y));
	gl_Position = newPosition;

	v_Color = a_Color;
}
 