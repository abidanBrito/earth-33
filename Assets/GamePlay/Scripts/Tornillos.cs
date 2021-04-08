using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornillos : BaseGame
{
    private float speed = 4f;
    private float rotationSpeed = 10f;
    private Vector3 towardsTarget;
    private Vector3 targetPosition;
    private int mode = 0;
    public int Mode
    {
        get => mode;
        set => mode = value;
    }

    
    void Start()
    {
        if(gameObject.tag == GameConstants.APARECER_TORNILLO_TAG)
        {
            var rg = gameObject.AddComponent <Rigidbody>();
            rg.useGravity = false;
            targetPosition = transform.position + Random.insideUnitSphere * 2f;
            targetPosition.y = 1;
            mode = 1;
        }
    }

    void Update()
    {
        if(mode == 1 || mode == 2)
        {
            buscarPunto();
        } 
    }

    //Si se detecta un collider, se comprueba el tag; si es tipo 'Player',
    //se elimina el Tornillo (ha sido recolectado)
    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            Destroy(gameObject);
        }
    }

    //Si aparece por muerte de enemigo, buscará un punto aleatorio
    //Si detecta un 'Player', ira a por él
    //Si el tornillo ha llegado al punto aleatorio, se convierte al tag 'Tornillo'
    public void buscarPunto()
    {
        playerTargetPosition.y +=1f;
        
        if(mode == 1) towardsTarget = targetPosition - transform.position;
        else if(mode == 2) towardsTarget = playerTargetPosition - transform.position; 

        suavizarMovimiento(transform,towardsTarget,speed, rotationSpeed);

        if(mode == 1 && towardsTarget.magnitude < 0.1f) mode = 0; gameObject.tag = GameConstants.TORNILLO_TAG;
    }
}