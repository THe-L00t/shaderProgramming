#version 330

in vec3 a_Position;
in float a_Radius;
in vec4 a_Color;
in float a_sTime;
in vec3 a_Vel;
in float a_LifeTime;
in float a_Mass;

out vec4 v_Color;

uniform float u_Time;
uniform vec3 u_Force;

const float PI = 3.141592;
const vec2 c_G = vec2(0,9.8);

void raining(){
	float newAlpha = 1.0;

	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	float newTime = u_Time - a_sTime;

	if(newTime > 0){
		float t = fract(newTime/a_LifeTime) * a_LifeTime ;
		float tt = t*t;
		float forceX = u_Force.x + c_G.x*a_Mass;
		float forceY = u_Force.y + c_G.y*a_Mass;

		float aX = forceX/ a_Mass;
		float aY = forceY/a_Mass;

		float x = a_Vel.x*t - 0.5*aX*tt;		//a_Radius*cos(0)
		float y = a_Vel.y * t -0.5*aY*tt; //a_Radius*sin(0)

		
		newPosition.xy += vec2(x,y);
		newAlpha = 1 - t/a_LifeTime;
		
	}
	else {
		newPosition.xy = vec2(-100000,0);
	}

	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb,newAlpha);
}

void sinParticle(){
	
	vec4 newPosition = vec4(a_Position.x,a_Position.y,a_Position.z,1);
	float newTime = u_Time - a_sTime;
	float t = fract(newTime);
	float cycle = 2*t * PI;
	if(newTime > 0){
		

		newPosition.x += 2*t-1;
		newPosition.y += sin(cycle)*a_Mass;
	}
	else{
		newPosition.xy = vec2(-100000,0);
	}
	


	gl_Position = newPosition;
	v_Color = vec4(a_Color.rgb,1);
}

void main()
{
	//raining();
	sinParticle();
}
 