#version 330

in vec3 a_Position;
in float a_Radius;
in vec4 a_Color;


out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;
const vec2 c_G = vec2(0,9.8);

void main()
{
	float t = fract(u_Time) ;
	float tt = t*t;
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	
	float jinja = -1 + 2*fract(u_Time);
	float rad = (jinja+1)*PI;
	float x = 0;		//a_Radius*cos(0)
	float y = -0.5*c_G.y*tt; //a_Radius*sin(0)

	newPosition.xy += vec2(x,y);
	gl_Position = newPosition;

	v_Color = a_Color;
}
 