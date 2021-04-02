using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    public bool _controlling;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*10f, ForceMode.Impulse);
        
        //modo controlar activado
        _controlling = true;
    }   

    void Update(){
        if(transform.position.y < -1f){
            Destroy(gameObject);
        }
    }
}
