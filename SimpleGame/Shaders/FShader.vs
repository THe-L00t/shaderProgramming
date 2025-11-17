#version 330

in vec3 a_Position;

out vec2 v_UV;

uniform vec4 u_Trans;

void main()
{
	vec4 newPosition = vec4(a_Position,1);
	gl_Position = newPosition;

	v_UV = vec2(a_Position.x, -a_Position.y);
}
