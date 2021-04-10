using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : BaseGame
{
    private float speed = 4f;
    private float rotationSpeed = 10f;
    private Vector3 towardTarget;
    private int movement = -2;
    public int movements
    {
        get => movement;
        set => movement = value;
    }
    private int mode = 0;
    public int modes
    {
        get => mode;
        set => mode = value;
    }

    void Start(){
        //not doing anything
        mode = -1;
    }

    void Update()
    {
        //si la esfera en concreto no esta siendo utilizada para controlar, no hara el movimiento de volver
        if(movement != 1 || movement != 2){
            if(movement == 0) calcularRuta(hitPosition);
            if(movement == -1) calcularRuta(transform.parent.position);
        }
        if(movements != 1 || movements != 2){
            if(movements == 0) calcularRuta(hitPosition);
            if(movements == -1) calcularRuta(transform.parent.position);
        }
    }

    public void calcularRuta(Vector3 p)
    {
        towardTarget = p - transform.position;

        smoothMovement(transform, towardTarget, speed, rotationSpeed);
            

        if(towardTarget.magnitude < 0.1f)
        {
            if (movement == 0)
            {
                movement = -1;       // volviendo
                usingBall = false;   // mientras vuelve puede usar otras esferas
            }
            else if(movement == -1)
            {
                movement = -2;      // Estado estatico
                mode = -1;           
            } 
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG)
        {
            if(movements != 1)
            {
                if(movements != 2)
                {
                    if(movements != -2)
                    {
                        movements  = -1;
                        usingBall = false;   // mientras vuelve puede usar otras esferas
                    }
                }
            }
        }

        if(other.gameObject.tag == GameConstants.MOVABLE_OBJECTS_TAG)
        {
            if(modes != 1)
            {
                if(collectedObject)
                {
                    if(collectedObject != gameObject)
                    {
                        if(movements != -2)
                        {
                            movements  = -1;
                            usingBall = false;   // mientras vuelve puede usar otras esferas
                        }
                    }
                }else
                {
                    if(movements != -2)
                    {
                        movements  = -1;
                        usingBall = false;   // mientras vuelve puede usar otras esferas
                    }
                }
            }
        }
    }
}