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

/****** PRECISION ******/
precision mediump float;
/****** PRECISION ******/

/******** SAMPLER ********/
uniform sampler2D uSampler;
/******** SAMPLER ********/		

/*********** BOOLEANS ***********/
uniform bool uUseTexture;
uniform bool uUseLighting;
/*********** BOOLEANS ***********/

/*********** LIGHTING ***********/
uniform vec3 uAmbientLight_Colour;

uniform vec3 uDirectionalLight_Direction;
uniform vec3 uDirectionalLight_Colour;

uniform vec3 uPointLight_Position;
uniform vec3 uPointLight_DiffuseColour;
uniform vec3 uPointLight_SpecularColour;
uniform float uPointLight_Shininess;
/*********** LIGHTING ***********/

/******** VARYING ********/
in vec4 oVertexColour;
in vec2 oVertexTexture;

in vec4 oPositionVec;
in vec3 oNormalVec;
in vec3 oEyeVec;
/******** VARYING ********/

uniform float uAlpha;
out vec4 fragColor;

vec4 Colour()
{
	if(uUseTexture)
		return texture2D(uSampler, oVertexTexture);
	else
		return oVertexColour;
}

vec3 Light()
{
	if(uUseLighting)
	{
		vec3 ambient;
		vec3 directional;
		vec3 point;	

		// Ambient Light
		ambient = uAmbientLight_Colour;
		// Ambient Light
		
		// Directoinal Light
		vec3 dirLightVec = normalize(uDirectionalLight_Direction - oPositionVec.xyz);
		float dirWeight = max(dot(oNormalVec, dirLightVec), 0.0);

		directional = uDirectionalLight_Colour * dirWeight;
		// Directoinal Light
		
		// Point Light
		vec3 poiLightVec = -normalize(uPointLight_Position - oPositionVec.xyz);
		float diffWeight = max(dot(oNormalVec, poiLightVec), 0.0);		
		float specWeight = pow(max(dot(poiLightVec, oEyeVec), 0.0), uPointLight_Shininess);
		
		point = (uPointLight_DiffuseColour * diffWeight) + (uPointLight_SpecularColour * specWeight);
		// Point Light

		return (ambient + directional + point);
	}
	else
		return vec3(1.0, 1.0, 1.0);
}

void main()
{
	vec4 colour = Colour();
	vec3 light = Light();
	
	fragColor = vec4(colour.rgb * light, 1.0);
}