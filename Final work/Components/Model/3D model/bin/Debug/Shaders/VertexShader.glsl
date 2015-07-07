#version 330

// ATTRIBUTES
in vec3 aVertexPosition;
in vec3 aVertexNormal;
    
in vec4 aVertexColour;
in vec2 aVertexTexture;    
// ATTRIBUTES

// MATRICES
uniform mat4 uProjectionMatrix;
uniform mat4 uModelViewMatrix;
uniform mat3 uNormalMatrix;
// MATRICES

out vec4 oVertexColour;
out vec3 oVertexNormal;
out vec4 mvPosition;

void main( void )
{
	mvPosition = uModelViewMatrix * vec4(aVertexPosition, 1.0);
	gl_Position = uProjectionMatrix * mvPosition;
	
	oVertexColour = aVertexColour;
	oVertexNormal = normalize(uNormalMatrix * aVertexNormal);
}