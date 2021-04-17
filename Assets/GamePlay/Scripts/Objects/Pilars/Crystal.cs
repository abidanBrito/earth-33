using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : BaseGame
{
    private EnergyBall sphere;
    private Transform spherePosition;
    private float distance; 
    private Rigidbody rb;
    private PilarsController pilarsController;
    private bool active = true;
    private BoxCollider cd;
    

    void Start()
    {
        spherePosition = transform.GetChild(0).transform;
        pilarsController = transform.parent.parent.GetComponent<PilarsController>(); //obtiene el pilar padre y luego el controlador de pilares
        pilarsController.crystals.Add(gameObject);
        cd = GetComponent<BoxCollider>();
        cd.size = new Vector3(2f, 2f, 2f);
    }
    void Update()
    {

        if(active)
        {
            transform.Rotate (0,25*Time.deltaTime,0); //rotates 50 degrees per second around z axis
        }
        if(collectedObject == gameObject){
            distance = Vector3.Distance(transform.position, pointMovableObject.transform.position);
            if(!collided){
                transform.position =  Vector3.Lerp(transform.position, pointMovableObject.transform.position, 5f*Time.deltaTime);
                if(rb){
                    rb.Sleep();
                }
            }
            if(sphereObjectControl.movements == 2){
                sphereObjectControl.transform.position = spherePosition.position;
            }
        }
    }
    private void ControlObject()
    {
        if(!collectedObject){
            transform.parent = null;
            transform.SetParent(pointMovableObject.gameObject.transform);
            collectedObject = gameObject;
            sphere.movements = 2;                         // Poniendo la esfera en estado de control 2 (Objetos)
            sphereObjectControl = sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
            usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
        }else{
            if(gameObject != collectedObject){
                StopControlingObject();
                ControlObject();
            }
        }
    }
    public void StopControlingObject()
    {
        collectedObject.gameObject.transform.SetParent(null);
        collectedObject = null;
        sphereObjectControl.movements = -1;                         // Poniendo la esfera en estado de control 2 (Objetos)
        sphereObjectControl = null;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
        if(!rb){
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sphere = other.gameObject.GetComponent<EnergyBall>();
        if(sphere != null){
            //si la bola esta en modo controlar
            if(sphere.modes == 1 && sphere.movements != -1 && sphere.movements != -2 && sphere.movements != 2 && sphere.movements != 1){
                cd.size = new Vector3(1f, 1f, 1f);
                for(int i = 0; i<pilarsController.crystals.Count;i++)
                {
                    if(pilarsController.crystals[i].Equals(gameObject)){
                        if(GetComponent<ParticleSystem>() != null)
                        {
                            Destroy(GetComponent<ParticleSystem>());
                        }
                        pilarsController.crystals.Remove(gameObject);
                        active = false;
                    }
                }
                ControlObject();
            }
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG)
        {
            if(collectedObject)
            {
                if(collectedObject == gameObject){
                    collided = true;
                    transform.position =  Vector3.Lerp(transform.position, pointMovableObject.transform.position, 0.2f*Time.deltaTime);
                }
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(collectedObject == gameObject){
            collided = false;
        }
    }
}
