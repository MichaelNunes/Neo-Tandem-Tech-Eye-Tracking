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

void main()
{
	vec4 colour = oVertexColour;

    vec3 ambient = vec3(0.2,0.2,0.2);
	vec3 light = ambient;
/*
    float dirWeight = max( dot( vNormalVec, uDirecitonalLight_Direction ), 0.0 );
    vec3 directional = uDirectionalLight_Colour * dirWeight;


    light = ambient + directional;*/
	gl_FragColor = vec4( light * colour.rgb, colour.a);
}