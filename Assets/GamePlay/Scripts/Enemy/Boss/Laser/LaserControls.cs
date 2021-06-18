using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControls : MonoBehaviour {

    
    public GameObject Laser;
	private GameObject activeLaser;
	private LaserScript laserScript;

    // Control From Pilars
    
    public PilarsController pilarsController;
    private List<GameObject> crystals = new List<GameObject>();
    private bool activatedLaser = false;
    public bool getActiveLaser{
        get => activatedLaser;
    }
    private bool shooting = false;
    public Transform playerTarget;
    
    private Transform Direction;
    private GameObject firePoint;
    public Transform Player;
    private int counterActivationLaser = 0;
    private Animator bossAnimator;
    private BossLaserSound laserSound;
    public bool laserSoundActivated = false;

    private void Awake() {
        laserSound = GetComponent<BossLaserSound>();
    }    
    void Start()
    {
        crystals = pilarsController.crystals;
        activeLaser = Laser;
		activeLaser.SetActive (true);
		laserScript = activeLaser.GetComponent<LaserScript> ();
        counterActivationLaser = crystals.Count-1;
        Direction = laserScript.Direction;
        firePoint = laserScript.firePoint;
        bossAnimator = GetComponentInChildren<Animator>();
    }

	private void Update () {
        
        RayCastControl();
        
        if(crystals.Count == 2)
        {
           ControlingCrystals(2);
        }
        else if(crystals.Count == 1)
        {
            ControlingCrystals(1);
        }
        else if(crystals.Count == 0)
        {
            ControlingCrystals(0);
        }
	}
    private void RayCastControl()
    {
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

        if (Physics.Raycast (firePoint.transform.position, direction, out hit, 60)) {
            if(shooting){
                if(hit.collider.tag == GameConstants.PLAYER_TAG){
                    playerTarget.GetComponent<CharHealth>().health-= 0.5f;
                }
            }
        }
    }

    private void ControlingCrystals(int crystalsCounter)
    {
        if(crystalsCounter == counterActivationLaser){
            bossAnimator.SetBool("LaserAttack", true);
            if(!laserSoundActivated)
                laserSound.PlayLaserSound();
                laserSoundActivated = true;
            Invoke(nameof(ActivateLaser), 1.5f);
        }else{
            laserScript.UpdateLaser();
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
        laserSoundActivated = false;
        bossAnimator.SetBool("LaserAttack", false);
    }
	
}
