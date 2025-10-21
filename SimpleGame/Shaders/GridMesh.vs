#version 330

in vec3 a_Position;

out vec4 v_Color;

uniform float u_Time;

const float PI = 3.141592;
const vec4 c_Points[3] = vec4[](vec4(0,0,2,0), vec4(0.5,0,3,0), vec4(0.25,-0.5,4,0));

void Flag()
{
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	float value = a_Position.x + 0.5;

	newPosition.y = newPosition.y *(1-value);

	float y = sin(2*value*PI - u_Time*3)*value*0.3;
	
	newPosition += vec4(0,y,0,0);
	
	gl_Position = newPosition;

	v_Color = vec4(1,1,1,1)*(1+sin(2*value*PI - u_Time*3));
}

void Wave()
{
	vec4 newPosition = vec4(a_Position,1);
	float dX = 0;
	float dY = 0;

	vec2 pos = vec2(a_Position.xy);
	vec2 cen = vec2(0,0);
	
	float d = distance(pos,cen) ;
	float v = clamp(0.5 - d,0,1);

	float newColor = v*sin(d*4*PI*10 - u_Time*3);

	

	newPosition += vec4(dX,dY,0,0);
	gl_Position = newPosition;

	v_Color = vec4(newColor);
}
void RainDrop()
{
	vec4 newPosition = vec4(a_Position,1);
	float dX = 0;
	float dY = 0;

	vec2 pos = vec2(a_Position.xy);
	float newColor = 0;
	for(int i = 0; i < 3 ;++i)
	{
		if(u_Time > c_Points[i].z){
			float t = u_Time - c_Points[i].z;
			vec2 cen = c_Points[i].xy;
		
			float d = distance(pos,cen) ;
			float v = clamp(0.5 - d,0,1);

			newColor += v*sin(d*4*PI*10 - t*3);
		}
		
	}
	

	

	newPosition += vec4(dX,dY,0,0);
	gl_Position = newPosition;

	v_Color = vec4(newColor);
}

void main()
{
	RainDrop();
}


/*

if(d <0.5)
	{
		newColor =1;
	}
	else
	{
		newColor = 0;
	}

	float value = 0.5 - d;
	value = clamp(value,0,1);
	value = ceil(value);
*/
