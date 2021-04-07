using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private Esfera _sphere;
    private Rigidbody rb;
    public Transform _spherePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameConstants.pointMovableObject = GameObject.Find("pointMovableObject");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameConstants._collectedObject == gameObject){
            transform.position = GameConstants.pointMovableObject.transform.position;
            transform.rotation = GameConstants.pointMovableObject.transform.rotation;
            if(GameConstants._sphereObjectControl._Movements == 2){
                GameConstants._sphereObjectControl.transform.position = _spherePosition.position;
            }
        }
    }

    private void ControlObject()
    {
        if(!GameConstants._collectedObject){
            rb.useGravity = false;
            transform.SetParent(GameConstants.pointMovableObject.gameObject.transform);
            GameConstants._collectedObject = gameObject;
            _sphere._Movements = 2;                         // Poniendo la esfera en estado de control 2 (Objetos)
            GameConstants._sphereObjectControl = _sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
            GameConstants._usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
        }else{
            if(gameObject != GameConstants._collectedObject){
                StopControlingObject();
                ControlObject();
            }
        }
    }
    public void StopControlingObject()
    {
        GameConstants._collectedObject.GetComponent<Rigidbody>().useGravity = true;
        GameConstants._collectedObject.gameObject.transform.SetParent(null);
        GameConstants._collectedObject = null;
        GameConstants._sphereObjectControl._Movements = -1;                         // Poniendo la esfera en estado de control 2 (Objetos)
        GameConstants._sphereObjectControl = null;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
    }
    private void OnTriggerEnter(Collider other)
    {
        _sphere = other.gameObject.GetComponent<Esfera>();
        if(_sphere != null){
            //si la bola esta en modo controlar
            if(_sphere._Mode == 1 && _sphere._Movements != -1 && _sphere._Movements != -2){
                ControlObject();
            }
        }
    }
}
