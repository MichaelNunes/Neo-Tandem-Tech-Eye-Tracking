#version 330
 
in vec3 vPosition;
in  vec3 vertexColor;
out vec4 vColor;
uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;
 
void
main()
{
    gl_Position = uMVMatrix * vec4(vPosition, 1.0);
	
    vColor = vec4( vertexColor, 1.0);
}