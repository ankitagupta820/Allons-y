void MainLight_half(out half3 Direction) {
	
#if SHADERGRAH_PREVIEW
	Direction = half3(0,1,0);
#else
	#ifdef LIGHTWEIGHT_LIGHTING_INCLUDED
		Light light = GetMainLight();
		Direction = light.direction;
	#else
		Direction = half3(0, 1, 0);
	#endif
#endif
}