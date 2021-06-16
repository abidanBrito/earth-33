using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : BaseGame
{
    private EnergyBall sphere;
    private Rigidbody rb;
    private Transform spherePosition;
    private GameObject player;
    private float distance; 

    [SerializeField] private float shotForce = 20f; 
    [SerializeField] private float shotDamage = 10f;
    public float ShotDamage
    {
        get => shotDamage;
    }

    private Vector3 shotDirection;

    public Vector3 ShotDirection
    {
        get => shotDirection;
    }
    private bool alreadyHitted;

    public bool AlreadyHitted{
        get => alreadyHitted;
        set => alreadyHitted = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spherePosition = transform.GetChild(0).transform;
        player = GameObject.FindWithTag(GameConstants.PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedObject == gameObject)
        {
            distance = Vector3.Distance(transform.position, pointMovableObject.transform.position);
            if (!collided)
            {
                transform.position =  Vector3.Lerp(transform.position, pointMovableObject.transform.position, 5f * Time.deltaTime);
                rb.Sleep();
            }
            if (sphereObjectControl.movements == 2)
            {
                sphereObjectControl.transform.position = spherePosition.position;
            }
        }
    }

    private void ControlObject()
    {
        if (!collectedObject)
        {
            rb.useGravity = false;
            transform.SetParent(pointMovableObject.gameObject.transform);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, gameObject.transform.rotation.y, 0));
            collectedObject = gameObject;
            sphere.movements = 2;               // Poniendo la esfera en estado de control 2 (Objetos)
            sphereObjectControl = sphere;       // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
            usingBall = false;                  // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
        } 
        else
        {
            if (gameObject != collectedObject)
            {
                stopControlingObject();
                ControlObject();
            }
        }
    }

    public void stopControlingObject()
    {
        collectedObject.GetComponent<Rigidbody>().useGravity = true;
        collectedObject.gameObject.transform.SetParent(null);
        collectedObject = null;
        sphereObjectControl.movements = -1;     // Poniendo la esfera en estado de control 2 (Objetos)
        sphereObjectControl = null;             // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
    }

    public void shootControlledObject()
    { 
        shotDirection = GameObject.FindWithTag(GameConstants.PLAYER_NECK_TAG).transform.forward;
        alreadyHitted = false;
        rb.AddForce(shotDirection * shotForce, ForceMode.Impulse);
        stopControlingObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        sphere = other.gameObject.GetComponent<EnergyBall>();
        if (sphere != null) 
        {
            EnergyBall energyBall = other.GetComponent<EnergyBall>();

            if(energyBall.modes == 0){
                GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactAttack, energyBall.transform.position, transform.rotation);
                Destroy(vfx, 2f);
            }
            if(energyBall.modes == 1){
                GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactControl, energyBall.transform.position, transform.rotation);
                Destroy(vfx, 2f);
            }
            if(energyBall.modes == 2){
                GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactPosession, energyBall.transform.position, transform.rotation);
                Destroy(vfx, 2f);
            }
            //si la bola esta en modo controlar
            if (sphere.modes == 1 && sphere.movements != -1 && sphere.movements != -2 
            && sphere.movements != 2 && sphere.movements != 1)
            {
                
                ControlObject();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == GameConstants.ENVIROMENT_TAG)
        {
            if (collectedObject)
            {
                if (collectedObject == gameObject)
                {
                    collided = true;
                    transform.position = Vector3.Lerp(transform.position, pointMovableObject.transform.position, 0.2f*Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (collectedObject == gameObject)
        {
            collided = false;
        }
    }
}
