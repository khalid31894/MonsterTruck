Shader "Unluck Software/ZippyLight2D Mobile (1xLight + Color)" {
	Properties{
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	}
	Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }
		BindChannels{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
		SubShader{ 
			UsePass "Unluck Software/ZippyLight2D Mobile (1xLight)/LIGHT"
			UsePass "Unluck Software/ZippyLight2D Mobile (Color)/COLOR"
		}
	}
}