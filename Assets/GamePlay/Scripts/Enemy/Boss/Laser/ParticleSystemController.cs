using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleSystemController : MonoBehaviour {

	class ParticleSystemOriginalSettings
	{
		public ParticleSystem.MinMaxGradient startColor;
		public ParticleSystem.MinMaxCurve startSize;
		public ParticleSystem.MinMaxCurve startSpeed;
		public ParticleSystem.MinMaxCurve startDelay;
		public ParticleSystem.MinMaxCurve startLifetime;
		public Vector3 localPosition;
	}

	public float size;
	public float speed;
	public bool loop;
	public bool lights;
	public bool trails;
	public bool changeColor;
	public Color newMaxColor = new Color (0,0,0,1);
	public Color newMinColor = new Color (0,0,0,1);
	public List<GameObject> ParticleSystems;
	public List<bool> ActiveParticleSystems;

	private List<ParticleSystemOriginalSettings> psOriginalSettingsList = new List<ParticleSystemOriginalSettings> ();

	void Awake (){
		UpdateParticleSystem ();
	}

	void Start () {
		UpdateParticleSystem ();
	}

	public void UpdateParticleSystem(){
		//Enables or Disbales Particle Systems you choose in inspector
		for(int i = 0; i< ParticleSystems.Count; i++){
			if (ActiveParticleSystems.Count == ParticleSystems.Count) {
				if (ActiveParticleSystems [i] == true)
					ParticleSystems [i].SetActive (true);
				else
					ParticleSystems [i].SetActive (false);
			} else
				Debug.Log ("Make sure the Active Particle Systems list has the same amount as the Particle Systems List");
		}

		if (ParticleSystems.Count > 0) {

			for (int i = 0; i < ParticleSystems.Count; i ++) {
				var ps = ParticleSystems [i].GetComponent<ParticleSystem> ();
				var main = ps.main;
				var shape = ps.shape;
				var psLights = ps.lights;
				var psTrails = ps.trails;
				var colorOverLifetime = ps.colorOverLifetime;

				//KEEP ORIGINAL VALUES - allows to refresh while in play
				if (psOriginalSettingsList.Count != ParticleSystems.Count) {
					ParticleSystemOriginalSettings psOriginalSettings = new ParticleSystemOriginalSettings () {
						startColor = main.startColor,
						startSize = main.startSize,
						startSpeed = main.startSpeed,
						startDelay = main.startDelay,
						startLifetime = main.startLifetime,
						localPosition = ParticleSystems[i].transform.localPosition
					};
					psOriginalSettingsList.Add (psOriginalSettings);
				}

				var startColor = psOriginalSettingsList[i].startColor;
				var startSize = psOriginalSettingsList[i].startSize;
				var startSpeed = psOriginalSettingsList[i].startSpeed;
				var startDelay = psOriginalSettingsList[i].startDelay;
				var startLifetime = psOriginalSettingsList[i].startLifetime;
				var localPos =psOriginalSettingsList[i].localPosition;

				//LOOP
				if (!loop && main.loop == true)
					main.loop = loop;

				//LIGHTS
				if (!lights && psLights.enabled) 					
					psLights.enabled = false;				

				//TRAILS
				if (!trails && psTrails.enabled)
					psTrails.enabled = false;	

				//POSITION
				if(i > 0){
					if(localPos.x != 0 || localPos.y != 0 || localPos.z != 0){						
						localPos.x *= size;
						localPos.y *= size;
						localPos.z *= size;
						ParticleSystems [i].transform.localPosition = localPos;
					}
				}					
					
				//SIZE
				if (startSize.mode == ParticleSystemCurveMode.TwoConstants) {
					startSize.constantMax *= size;
					startSize.constantMin *= size;
					main.startSize = startSize;
				} else {
					startSize.constant *= size;
					main.startSize = startSize;
				}

				//START_SPEED (affected by size)
				if (startSpeed.mode == ParticleSystemCurveMode.TwoConstants) {
					startSpeed.constantMax *= size;
					startSpeed.constantMin *= size;
					main.startSpeed = startSpeed;
				} else {
					startSpeed.constant *= size;
					main.startSpeed = startSpeed;
				}

				//START_SPEED (affected by speed)
				if (startSpeed.mode == ParticleSystemCurveMode.TwoConstants) {
					startSpeed.constantMax *= speed;
					startSpeed.constantMin *= speed;
					main.startSpeed = startSpeed;
				} else {
					startSpeed.constant *= speed;
					main.startSpeed = startSpeed;
				}

				//LIFETIME
				if (main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants) {
					startLifetime.constantMax *= 1/speed;
					startLifetime.constantMin *= 1/speed;
					main.startLifetime = startLifetime;
				} else {
					startLifetime.constant *= 1/speed;
					main.startLifetime = startLifetime;
				}

				//START_DELAY
				if (startDelay.mode == ParticleSystemCurveMode.TwoConstants) {
					startDelay.constantMax *= 1/speed;
					startDelay.constantMin *= 1/speed;
					main.startDelay = startDelay;
				} else {
					startDelay.constant *= 1/speed;
					main.startDelay = startDelay;
				}

				//RADIUS
				if(shape.enabled){
					shape.radius *= size;
				}

				//COLOR
				if (changeColor) {
					if (main.startColor.mode == ParticleSystemGradientMode.Color) {
						startColor.color = ChangeHUE(startColor.color, newMaxColor);
						main.startColor = startColor;
					}
					if (main.startColor.mode == ParticleSystemGradientMode.TwoColors) {
						startColor.colorMax = ChangeHUE(startColor.colorMax, newMaxColor);
						startColor.colorMin = ChangeHUE(startColor.colorMin, newMinColor);
						main.startColor = startColor;
					} 
					if (main.startColor.mode == ParticleSystemGradientMode.Gradient) {
						startColor.gradient = ChangeGradientColor(startColor.gradient, newMaxColor);
						main.startColor = startColor;
					} 
					if (main.startColor.mode == ParticleSystemGradientMode.TwoGradients) {
						startColor.gradientMax = ChangeGradientColor (startColor.gradientMax, newMaxColor);
						startColor.gradientMin = ChangeGradientColor (startColor.gradientMin, newMinColor);
						main.startColor = startColor;
					} 

					//COLOR OVERLIFETIME
					if(colorOverLifetime.enabled){
						var colColor = colorOverLifetime.color;
						if(colorOverLifetime.color.mode == ParticleSystemGradientMode.Gradient){							
							colColor.gradient = ChangeGradientColor(colorOverLifetime.color.gradient, newMaxColor);
						}
						if(colorOverLifetime.color.mode == ParticleSystemGradientMode.TwoGradients){
							colColor.gradientMax = ChangeGradientColor (colorOverLifetime.color.gradientMax, newMaxColor);
							colColor.gradientMin = ChangeGradientColor (colorOverLifetime.color.gradientMin, newMinColor);
						}
						colorOverLifetime.color = colColor;
					}
				}
			}				
		}
		else
			Debug.Log("No Particle Systems added to the Particle Systems list");
	}

	public Color ChangeHUE (Color oldColor, Color newColor){
		float newHue;
		float newSaturation;
		float newValue;
		float oldHue;
		float oldSaturation;
		float oldValue;
		float originalAlpha = oldColor.a;
		Color.RGBToHSV (newColor, out newHue, out newSaturation, out newValue); 
		Color.RGBToHSV (oldColor, out oldHue, out oldSaturation, out oldValue); 
		var updatedColor = Color.HSVToRGB (newHue, oldSaturation, oldValue);
		updatedColor.a = originalAlpha;
		return updatedColor;
	}

	public Gradient ChangeGradientColor (Gradient oldGradient, Color newColor){
		GradientColorKey[] colorKeys = new GradientColorKey[oldGradient.colorKeys.Length];
		for(int j = 0; j < oldGradient.colorKeys.Length; j++){
			colorKeys [j].time = oldGradient.colorKeys [j].time;
			colorKeys [j].color = ChangeHUE (oldGradient.colorKeys[j].color, newColor);
		}
		oldGradient.SetKeys (colorKeys, oldGradient.alphaKeys);
		return oldGradient;
	}
		
}
