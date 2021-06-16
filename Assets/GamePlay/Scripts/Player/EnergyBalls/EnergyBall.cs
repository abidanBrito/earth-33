using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : BaseGame
{
    [SerializeField] private float speed = 4f;
    public GameObject explosionVFX;
    [SerializeField] private Material modeAttack;
    [SerializeField] private Material modeControl;
    [SerializeField] private Material modePosesion;
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

    public GameObject vfxImpactAttack;
    public GameObject vfxImpactControl;
    public GameObject vfxImpactPosession;

    
    void Start(){
        //not doing anything
        mode = -1;
        esferas.Add(gameObject.GetComponent<EnergyBall>());
    }

    void Update()
    {
        //si la esfera en concreto no esta siendo utilizada para controlar, no hara el movimiento de volver
        if(movement != 1 || movement != 2){
            if(movement == 0) calcularRuta(hitPosition); speed = 3f;
            if(transform.parent)
            {
                if(movement == -1) calcularRuta(transform.parent.position); speed = 5f;
            }
        }
      
        SphereColorModes();
    }
    private void SphereColorModes()
    {

        if(sphereModes == 0)
        {
            if(gameObject.GetComponent<Renderer>().material != modeAttack)
            gameObject.GetComponent<Renderer>().material = modeAttack;
        }
        if(sphereModes == 1)
        {
            if(gameObject.GetComponent<Renderer>().material != modeControl)
            gameObject.GetComponent<Renderer>().material = modeControl;
        }
        if(sphereModes == 2)
        {
            if(gameObject.GetComponent<Renderer>().material != modePosesion)
            gameObject.GetComponent<Renderer>().material = modePosesion;
            
        }
    }

    public void Disparar()
    {
        int mode = sphereModes;
        
        for(int i = 0;i<3; i++)
        {
            if(usingBall == false && esferas[i].movements == -2)
            {
                esferas[i].movements = 0; //modo de movimiento de la esfera
                esferas[i].modes = mode; //Modo de la esfera
                usingBall = true;
            } 
        }
    }
    public void CambiarModo()
    {
        //cambia de modo
        sphereModes++;
        if(sphereModes > 2) sphereModes = 0;//vuelve al modo ataque
    }

    public void calcularRuta(Vector3 p)
    {
        towardTarget = p - transform.position;
        
        smoothMovement(transform, towardTarget, speed, rotationSpeed);

        if(towardTarget.magnitude < 0.3f)
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
        if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG || other.GetComponent<BossShiled>())
        {
            if(movements != 1)
            {
                if(movements != 2)
                {
                    if(movements != -2)
                    {
                        if(modes == 0){
                            GameObject vfx = GameObject.Instantiate(vfxImpactAttack, transform.position, transform.rotation);
                            Destroy(vfx, 2f);
                        }
                        if(modes == 1){
                            GameObject vfx = GameObject.Instantiate(vfxImpactControl, transform.position, transform.rotation);
                            Destroy(vfx, 2f);
                        }
                        if(modes == 2){
                            GameObject vfx = GameObject.Instantiate(vfxImpactPosession, transform.position, transform.rotation);
                            Destroy(vfx, 2f);
                        }
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