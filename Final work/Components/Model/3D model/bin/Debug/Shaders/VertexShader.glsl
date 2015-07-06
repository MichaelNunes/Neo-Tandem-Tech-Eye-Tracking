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

void main( void )
{
	gl_Position = uProjectionMatrix * uModelViewMatrix * vec4( aVertexPosition, 1.0 );
	oVertexColour = aVertexColour;
	oVertexNormal = aVertexNormal;
}