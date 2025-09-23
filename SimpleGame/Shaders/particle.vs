#version 330

in vec3 a_Position;
in float a_Radius;
in vec4 a_Color;
in float a_sTime;
in vec3 a_Vel;

out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;
const vec2 c_G = vec2(0,9.8);

void main()
{
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	float newTime = u_Time - a_sTime;

	if(newTime > 0){
	float t = fract(newTime/2.0) *2.0 ;
	float tt = t*t;
	float x = 0;		//a_Radius*cos(0)
	float y = -0.5*c_G.y*0.2*tt; //a_Radius*sin(0)

	
	newPosition.xy += vec2(x,y);
	
	}
	else {
		newPosition.xy = vec2(-100000,0);
	}

	gl_Position = newPosition;
	v_Color = a_Color;
}
 