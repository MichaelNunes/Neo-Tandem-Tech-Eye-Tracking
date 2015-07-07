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
in vec4 mvPosition;

out vec4 FragColor;

void main()
{
	vec4 colour = oVertexColour;

    vec3 ambient = vec3(0.1,0.1,0.1);
	
	float dirWeight = max( dot( oVertexNormal, normalize( vec3( 1.0, 2.0, 1.0 ) - mvPosition.xyz ) ), 0.0 );
	vec3 dirLightColor = vec3(0.3, 0.0, 0.3);
	vec3 dirLight = dirLightColor * dirWeight;
	
	vec3 light = ambient + dirLight;
	
	FragColor = vec4( (colour.rgb * light), colour.a);
}