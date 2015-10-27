#version 330

/*
 * Copyright (c) 2015 The University of Pretoria.
 *
 * The following was designed for the Centre of GeoInformation
 * Science (CGIS), University of Pretoria. All code is property
 * of the University of Pretoria and is available under the 
 * Creative Commons Attribution-ShareAlike (CC BY-SA) see:
 * "https://creativecommons.org/licenses/"
 *
 * Author: Duran Cole
 * Email: u13329414@tuks.co.za
 * Author: Michael Nunes
 * Email: u12104592@tuks.co.za
 * Author: Molefe Molefe
 * Email: u12260429@tuks.co.za
 * Author: Tebogo Christopher Seshibe
 * Email: u13181442@tuks.co.za
 * Author: Timothy Snayers
 * Email: u13397134@tuks.co.za
 */


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

// OUTPUT
out vec4 oVertexColour;
out vec2 oVertexTexture;

out vec4 oPositionVec;
out vec3 oNormalVec;
out vec3 oEyeVec;
// OUTPUT

void main(void)
{
	oVertexColour = aVertexColour;
	oVertexTexture = aVertexTexture;

	oPositionVec = uModelViewMatrix * vec4(aVertexPosition, 1.0);
	oNormalVec = normalize(uNormalMatrix * aVertexNormal);
	oEyeVec = vec3(-oNormalVec.xyz);

	gl_Position = uProjectionMatrix * oPositionVec;
}