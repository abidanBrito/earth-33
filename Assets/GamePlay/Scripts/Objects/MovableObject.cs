using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : BaseGame
{
    private Esfera _sphere;
    private Rigidbody rb;
    public Transform _spherePosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BaseGame.pointMovableObject = GameObject.Find("pointMovableObject");
    }

    // Update is called once per frame
    void Update()
    {
        if(BaseGame._collectedObject == gameObject){
            transform.position = BaseGame.pointMovableObject.transform.position;
            transform.rotation = BaseGame.pointMovableObject.transform.rotation;
            if(BaseGame._sphereObjectControl._Movements == 2){
                BaseGame._sphereObjectControl.transform.position = _spherePosition.position;
            }
        }
    }

    private void ControlObject()
    {
        if(!BaseGame._collectedObject){
            rb.useGravity = false;
            transform.SetParent(BaseGame.pointMovableObject.gameObject.transform);
            BaseGame._collectedObject = gameObject;
            _sphere._Movements = 2;                         // Poniendo la esfera en estado de control 2 (Objetos)
            BaseGame._sphereObjectControl = _sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
            BaseGame._usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
        }else{
            if(gameObject != BaseGame._collectedObject){
                StopControlingObject();
                ControlObject();
            }
        }
    }
    public void StopControlingObject()
    {
        BaseGame._collectedObject.GetComponent<Rigidbody>().useGravity = true;
        BaseGame._collectedObject.gameObject.transform.SetParent(null);
        BaseGame._collectedObject = null;
        BaseGame._sphereObjectControl._Movements = -1;                         // Poniendo la esfera en estado de control 2 (Objetos)
        BaseGame._sphereObjectControl = null;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
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
