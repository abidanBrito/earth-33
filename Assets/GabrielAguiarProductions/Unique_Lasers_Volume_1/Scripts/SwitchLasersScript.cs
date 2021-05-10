using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLasersScript : MonoBehaviour {

	public GameObject fixedCamera;
	public GameObject fpsCamera;
	public List<GameObject> Lasers = new List<GameObject> ();

	private int count = 0;
	private GameObject activeLaser;
	private LaserScript laserScript;

	void Start () {
		for(int i = 0; i < Lasers.Count; i++){
			Lasers [i].SetActive (false);
		}
		activeLaser = Lasers [0];
		activeLaser.SetActive (true);
		laserScript = activeLaser.GetComponent<LaserScript> ();
	}

	void Update () {

		if (Input.GetMouseButtonDown (0)) {			
			laserScript.EnableLaser ();
		}

		if (Input.GetMouseButton (0)) {
			laserScript.UpdateLaser ();
		}

		if(Input.GetMouseButtonUp (0)){
			laserScript.DisableLaserCaller (laserScript.disableDelay);
		}


		if (Input.GetKeyDown (KeyCode.E))
			Next ();		

		if (Input.GetKeyDown (KeyCode.Q)) 
			Previous ();	

		if (Input.GetKeyDown (KeyCode.C) && fixedCamera != null && fpsCamera != null) {
			ChangeCamera ();
			RefreshLaser ();
		}
	}

	public void Next () {
		count++;

		if (count > Lasers.Count)
			count = 0;

		for(int i = 0; i < Lasers.Count; i++){
			if (count == i) {
				laserScript.DisableLaserCaller (0.05f);
				activeLaser.SetActive (false);
				activeLaser = Lasers [i];
				laserScript = activeLaser.GetComponent<LaserScript> ();
				laserScript.EnableLaser ();
				activeLaser.SetActive (true);
			}
		}
	}

	public void Previous () {
		count--;

		if (count < 0)
			count = Lasers.Count;

		for(int i = 0; i < Lasers.Count; i++){
			if (count == i) {
				laserScript.DisableLaserCaller (0.05f);
				activeLaser.SetActive (false);
				activeLaser = Lasers [i];
				laserScript = activeLaser.GetComponent<LaserScript> ();
				laserScript.EnableLaser ();
				activeLaser.SetActive (true);
			}
		}
	}

	public void ChangeCamera () {
		if (!fixedCamera.activeSelf) {
			fixedCamera.SetActive (true);
			fpsCamera.SetActive (false);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {
			fixedCamera.SetActive (false);
			fpsCamera.SetActive (true);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void RefreshLaser (){
		for(int i = 0; i < Lasers.Count; i++){
			laserScript = Lasers[i].GetComponent<LaserScript> ();
			if (fixedCamera.gameObject.activeSelf) {
				laserScript.firePoint = Lasers [i];
				laserScript.endPoint = fixedCamera;
			}
			else{
				laserScript.firePoint = fpsCamera.transform.FindDeepChild("FirePoint").gameObject;
				laserScript.endPoint = fpsCamera;
			}
		}
	}
}
