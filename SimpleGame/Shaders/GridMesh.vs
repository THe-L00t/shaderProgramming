#version 330

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;

void main()
{
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	float value = a_Position.x + 0.5;

	newPosition.y = newPosition.y *(1-value);

	float y = sin(2*value*PI - u_Time*3)*value*0.3;
	
	newPosition += vec4(0,y,0,0);
	
	gl_Position = newPosition;

	v_Color = vec4(1,1,1,1)*(1+(sin(2*value*PI - u_Time*3)*value));
}
 