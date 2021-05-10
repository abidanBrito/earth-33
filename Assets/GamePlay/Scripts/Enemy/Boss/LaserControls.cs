using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControls : MonoBehaviour {

    
    [SerializeField] private GameObject Laser;
	private GameObject activeLaser;
	private LaserScript laserScript;

    // Control From Pilars
    
    [SerializeField] private Transform pilarsController;
    private List<GameObject> crystals = new List<GameObject>();
    private bool activatedLaser = false;
    public bool getActiveLaser{
        get => activatedLaser;
    }
    private bool shooting = false;
    public Camera cam;
    private Ray rayMouse;
    public Transform playerTarget;

    private int counterActivationLaser = 0;
    void Start()
    {
        crystals = pilarsController.GetComponent<PilarsController>().crystals;
        activeLaser = Laser;
		activeLaser.SetActive (true);
		laserScript = activeLaser.GetComponent<LaserScript> ();
        counterActivationLaser = crystals.Count-1;
    }

	void Update () {

        
        if (cam != null) {
			RaycastHit hit;
			var mousePos = cam.transform.forward;
			rayMouse = cam.ScreenPointToRay (mousePos);

			if (Physics.Raycast (rayMouse.origin, rayMouse.direction, out hit, 60)) {
                    if(shooting){
                        if(hit.collider.tag == GameConstants.PLAYER_TAG){
                            playerTarget.GetComponent<CharHealth>().health-= 0.2f;
                        }
                    }
            }
        }
        
        if(crystals.Count == 2)
        {
            if(crystals.Count == counterActivationLaser){
                ActivateLaser();
            }else{
                laserScript.UpdateLaser();
            }
        }
        else if(crystals.Count == 1)
        {
            if(crystals.Count == counterActivationLaser){
                ActivateLaser();
            }else{
                laserScript.UpdateLaser();
            }
        }
        else if(crystals.Count == 0)
        {
            if(crystals.Count == counterActivationLaser){
                ActivateLaser();
            }else{
                laserScript.UpdateLaser();
            }
        }
	}
    public void ActivateLaser()
    {
        if(!activatedLaser){
            activatedLaser = true;
            shooting = true;
            laserScript.EnableLaser();
            counterActivationLaser--;
            StartCoroutine(ActivatedTimeLaser());
        }
    }
    IEnumerator ActivatedTimeLaser()
    {
        yield return new WaitForSeconds(3);
        shooting = false;
        activatedLaser = false;
        laserScript.DisableLaserCaller(laserScript.disableDelay);
    }
	
}
