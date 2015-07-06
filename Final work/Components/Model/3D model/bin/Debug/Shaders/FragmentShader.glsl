#version 330

/******** SAMPLER ********/
uniform sampler2D uSampler;
/******** SAMPLER ********/

/********** LIGHTING **********/
uniform vec3 uAmbientLight_Color;
uniform vec3 uDirectionalLight_Color;
uniform vec3 uDirectionalLight_Direction;
uniform vec3 uPointLight_DiffuseColor;
uniform vec3 uPointLight_SpecularColor;
uniform vec3 uPointLight_Position;
uniform float uPointLight_Shininess;
/********** LIGHTING **********/	

in vec4 oVertexColour;
in vec3 oVertexNormal;

void main()
{
	vec4 colour = oVertexColour;

    vec3 ambient = vec3(0.5,0.5,0.5);
	vec3 light = ambient;
	float dirWeight = max( dot( oVertexNormal, vec3( 0.0, -1.0, 1.0 ) ), 0.0 );
/*
    
    vec3 directional = uDirectionalLight_Colour * dirWeight;


    light = ambient + directional;*/
	gl_FragColor = vec4( (colour.rgb * ambient) * dirWeight, colour.a);
}