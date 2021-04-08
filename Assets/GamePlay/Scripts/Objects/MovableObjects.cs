using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : BaseGame
{
    private Esfera sphere;
    private Rigidbody rb;
    public Transform spherePosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pointMovableObject = GameObject.Find("pointMovableObject");
    }

    // Update is called once per frame
    void Update()
    {
        if(collectedObject == gameObject){
            transform.position = pointMovableObject.transform.position;
            transform.rotation = pointMovableObject.transform.rotation;
            if(sphereObjectControl.movements == 2){
                sphereObjectControl.transform.position = spherePosition.position;
            }
        }
    }

    private void ControlObject()
    {
        if(!collectedObject){
            rb.useGravity = false;
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
        collectedObject.GetComponent<Rigidbody>().useGravity = true;
        collectedObject.gameObject.transform.SetParent(null);
        collectedObject = null;
        sphereObjectControl.movements = -1;                         // Poniendo la esfera en estado de control 2 (Objetos)
        sphereObjectControl = null;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
    }
    private void OnTriggerEnter(Collider other)
    {
        sphere = other.gameObject.GetComponent<Esfera>();
        if(sphere != null){
            //si la bola esta en modo controlar
            if(sphere.modes == 1 && sphere.movements != -1 && sphere.movements != -2){
                ControlObject();
            }
        }
    }
}
