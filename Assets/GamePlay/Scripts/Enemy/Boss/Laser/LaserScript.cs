using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

	public List<LineRenderer> lineRenderers;
	public GameObject firePoint; //the point where the laser starts
	public GameObject endPoint; //previously called 'cam' - now allows for anything to be an end point, instead of only a camera
	public float maximumLenght;
	public float growWidth; //how much it grows in the beginning
	public float growSpeed; //how fast it grows in the beginning
	public float shrinkSpeed; //how fast it shrinks in the end
	public float disableDelay; //delay after laser is stopped
	public GameObject endVFX; //all the particle systems at the end of the laser
	public GameObject psVFX; //all the particles systems at the start and in the middle of the laser
	public GameObject trailVFX; //the particle system that leaves a trail
	public float trailInterval; //interval between each trail - 0 means continuous trail
	public bool trail;
	private Ray rayMouse;
	private List<ParticleSystem> psChilds = new List<ParticleSystem>();
	private List<ParticleSystem> psEndVFX = new List<ParticleSystem>();
	private List<float> lrWidth = new List<float> ();
	private ParticleSystemController psCtrl;
	private ParticleSystem psTrailVFX;
	private bool emittingTrail;
	private bool isStarting;
	private bool isOver;
	private WaitForSeconds growWait = new WaitForSeconds (0.025f);
	private WaitForSeconds updateWait = new WaitForSeconds (0.01f);
	private ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams ();
	public Transform Direction;
	public Transform Player;

	void Start () {
		//add width of the lasers to a list and sets the width to 0
		for (int i = 0; i < lineRenderers.Count; i++) {
			lrWidth.Add(lineRenderers[i].widthMultiplier);
			lineRenderers [i].widthMultiplier = 0;
			lineRenderers [i].enabled = false;
		}

		//COLOR - changes color according to ParticleSystemController
		psCtrl = GetComponent<ParticleSystemController>();
		if(psCtrl != null && psCtrl.changeColor){
			for(int i = 0; i< lineRenderers.Count; i++){
				lineRenderers [i].colorGradient = psCtrl.ChangeGradientColor (lineRenderers [i].colorGradient, psCtrl.newMaxColor);
			}
		}

		//add particle systems to a respective list
		if (endVFX != null) {
			endVFX.SetActive (false);
			AddPSToList (endVFX, psEndVFX, true);
		}
		if (psVFX != null) {
			psVFX.SetActive (false);
			AddPSToList (psVFX, psChilds, true);
		}
		if (trailVFX != null && trail) {
			psTrailVFX = trailVFX.GetComponent<ParticleSystem> ();
		}

		
	}

	//use this function to shoot a laser that isn't in FPS or TPS.
	public void ShootLaser (float duration){
		StartCoroutine (ShootLaserCoroutine(duration));
	}

	IEnumerator ShootLaserCoroutine (float duration){
		yield return new WaitForSeconds (0.02f);//safe wait

		EnableLaser ();

		UpdateLaserContinuously ();

		yield return new WaitForSeconds (duration);

		DisableLaserCaller (disableDelay);
	}

	//enables line renderers and respective particle systems of a Laser. Also adds extra width (growWidth) to each line renderer.
	public void EnableLaser (){
		for (int i = 0; i < lineRenderers.Count; i++) 
			lineRenderers[i].enabled = true;

		if (endVFX != null) {
			endVFX.SetActive (true);
			EnablePS (psEndVFX);
		}
		if (psVFX != null) {
			psVFX.SetActive (true);
			EnablePS (psChilds);
		}
			
		isStarting = true;
		isOver = false;
		GrowLaserCaller ();
		RotateToMouse (psVFX, endVFX.transform.position);
	}

	//after 'EnableLaser', 'UpdateLaser' is used to keep the position, normals and trail updated, according to an input or to a gameobject position.
	public void UpdateLaser () {

		RaycastHit hit;
		Vector3 fromPosition = firePoint.transform.position;
		Direction.transform.position = Player.transform.position;
		Vector3 toPosition = Direction.transform.position;
		Vector3 direction = toPosition - fromPosition;


		if(Physics.Raycast(firePoint.transform.position,direction,out hit))
		{
			toPosition = hit.point;
		}
		direction = toPosition - fromPosition;

		if (firePoint != null) {
			for (int i = 0; i < lineRenderers.Count; i++)
				lineRenderers [i].SetPosition (0, firePoint.transform.position);

			psVFX.transform.position = firePoint.transform.position;
		}
		else
			Debug.Log ("There is no fire point in the inspector.");

		if (direction != null) {
			 if (Physics.Raycast (firePoint.transform.position, direction, out hit, maximumLenght)) {
				if (hit.collider) {
					for (int i = 0; i < lineRenderers.Count; i++)
						lineRenderers [i].SetPosition (1, hit.point);
					if (endVFX != null) {
						endVFX.transform.position = hit.point;
						endVFX.transform.forward = -hit.normal;
					}
					if (trailVFX != null && trail) {
						if (emittingTrail == false)
							StartCoroutine (EmitTrail (hit.point));
					}
				}
			} else {	
				var pos = rayMouse.GetPoint (maximumLenght);
				for (int i = 0; i < lineRenderers.Count; i++)
					lineRenderers [i].SetPosition (1, pos);	
				if (endVFX != null) {
					endVFX.transform.position = pos;
					RotateToMouse (endVFX, psVFX.transform.position);
				}
				if (trailVFX != null && trail) {
					emittingTrail = false;
					psTrailVFX.Stop ();
				}
			}
		} else {
			for (int i = 0; i < lineRenderers.Count; i++)
				lineRenderers [i].SetPosition (1, endPoint.transform.position);
			if (endVFX != null) {
				endVFX.transform.position = endPoint.transform.position;
			}
			if (trailVFX != null && trail) {
				if (emittingTrail == false)
					StartCoroutine (EmitTrail (endPoint.transform.position));
			}
		}

		RotateToMouse (psVFX, endVFX.transform.position);
	}

	//useful for when not using any input to trigger the Laser
	void UpdateLaserContinuously (){
		StartCoroutine (UpdateLaserCoroutine());
	}

	IEnumerator UpdateLaserCoroutine (){
		while (isStarting) {
			UpdateLaser ();
			yield return updateWait;
		}
	}

	//function to iterate trhough the line renderers and make them grow in the begging 
	void GrowLaserCaller () {
		if (gameObject.activeSelf) {
			for (int i = 0; i < lineRenderers.Count; i++)
				StartCoroutine (GrowLaser (lineRenderers [i], lrWidth [i]));
		}
	}

	//coroutine used to give some extra width in the beginning
	IEnumerator GrowLaser (LineRenderer lr, float originalWidth){		
		while (isStarting && lr.widthMultiplier < originalWidth + growWidth) {
			if (lr.widthMultiplier + growSpeed > originalWidth + growWidth)
				lr.widthMultiplier = originalWidth + growWidth;
			else
				lr.widthMultiplier += growSpeed;
			yield return growWait;
		}
		yield return growWait;
		while (isStarting && lr.widthMultiplier > originalWidth) {
			if (lr.widthMultiplier - growSpeed < originalWidth)
				lr.widthMultiplier = originalWidth;
			else
				lr.widthMultiplier -= growSpeed/2;
			yield return growWait;
		}
	}

	public void DisableLaserCaller (float timer){
		StartCoroutine (DisableLaser(timer));
	}

	IEnumerator DisableLaser (float timer){
		isStarting = false;
		isOver = true;
		ShrinkLaserCaller ();
		HoldLaserCaller ();

		if (endVFX != null) {
			DisablePS (psEndVFX);
		}
		if (psVFX != null) {
			DisablePS (psChilds);
		}
		yield return new WaitForSeconds (timer);
		if (isStarting == false) {
			isOver = false;
			for (int i = 0; i < lineRenderers.Count; i++) 
				lineRenderers[i].enabled = false;
		}
	} 

	//shrink each line renderer
	void ShrinkLaserCaller () {
		for(int i = 0; i< lineRenderers.Count; i++)
			StartCoroutine (ShrinkLaser(lineRenderers[i]));
	}

	IEnumerator ShrinkLaser (LineRenderer lr){
		while (isOver && lr.widthMultiplier > 0) {
			lr.widthMultiplier -= shrinkSpeed;
			yield return growWait;
		}
	}

	//update the laser while shrinking
	void HoldLaserCaller (){
		StartCoroutine (HoldLaser ());
	}
		
	IEnumerator HoldLaser () {
		while (isOver){
			UpdateLaser ();
			RotateToMouse (psVFX, endVFX.transform.position);
			yield return updateWait;
		}
	}

	//emits the trail at a certain rate/interval
	IEnumerator EmitTrail (Vector3 hitPosition){
		if (emittingTrail == false) {
			emittingTrail = true;
			emitParams.position = hitPosition;
			emitParams.rotation3D = -endVFX.transform.eulerAngles;
			psTrailVFX.Emit (emitParams, 1);
		}
		yield return new WaitForSeconds (trailInterval);
		emittingTrail = false;
	}

	//auxiliary public function to rotate a gameobject to a destination
	void RotateToMouse (GameObject obj, Vector3 destination ) {
		var direction = destination - obj.transform.position;
		Quaternion rotation = Quaternion.LookRotation (direction);
		obj.transform.rotation = Quaternion.Lerp (obj.transform.rotation, rotation, 1);
	}

	//auxiliary function to add particle systems to a given list from a gameobject
	void AddPSToList (GameObject obj, List<ParticleSystem> listToAdd, bool addFromObj) {
		if (addFromObj) {
			var psObj = obj.GetComponent<ParticleSystem> ();
			if (psObj != null)
				listToAdd.Add (psObj);
		}

		if(obj.transform.childCount>0){
			for (int i = 0; i < obj.transform.childCount; i++) {
				var ps = obj.transform.GetChild (i).GetComponent<ParticleSystem> ();
				if(ps != null)
					listToAdd.Add (ps);
			}
		}
		else
			Debug.Log ("The GameObject " + obj.name + " contains no childs.");
	}

	//auxiliary function to enable a list of particle systems
	void EnablePS (List<ParticleSystem> psList){
		for (int i = 0; i < psList.Count; i++) {			
			if (psList [i].isEmitting == false){
				psList [i].Play ();
				var emission = psList [i].emission;
				emission.enabled = true;//Play ();
			}
		}
	}

	//auxiliary function to stop and disable emission of a list of particle systems
	void DisablePS (List<ParticleSystem> psList){
		for (int i = 0; i < psList.Count; i++) {
			if(psList[i].isEmitting == true) {
				psList [i].Stop ();
				var emission = psList [i].emission;
				emission.enabled = false;//
			}
		}
	} 
		
}
