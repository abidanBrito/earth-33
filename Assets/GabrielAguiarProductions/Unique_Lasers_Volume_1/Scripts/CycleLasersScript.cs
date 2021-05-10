using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleLasersScript : MonoBehaviour {

	public GameObject playerFirePoint;
	public List<GameObject> targetPoints;
	public List<GameObject> Lasers = new List<GameObject> ();

	private GameObject newLaser;
	private LaserScript laserScript;
	private WaitForSeconds shortWait = new WaitForSeconds (0.5f);
	private WaitForSeconds longWait = new WaitForSeconds (4);

	void Start () {
		StartCoroutine (CycleLasers());
	}

	IEnumerator CycleLasers (){
		for(int i = 0; i<Lasers.Count; i++){
			
			newLaser = Instantiate (Lasers [i]);
			laserScript = newLaser.GetComponent<LaserScript> ();
			laserScript.trail = false;
			laserScript.firePoint = playerFirePoint;
			laserScript.endPoint = targetPoints [Random.Range (0, targetPoints.Count)].gameObject;
			newLaser.SetActive (true);
			laserScript.ShootLaser (3);

			yield return longWait;

			Destroy (newLaser);
		}

		StartCoroutine (CycleLasers());
	}
}
