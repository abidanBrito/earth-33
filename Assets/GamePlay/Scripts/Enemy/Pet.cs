using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pet : BaseGame
{
    private NavMeshAgent agent;
    private Transform player;
    //  Attacking
    //  Enemy under control by player
    private Transform positionGuard;
    private bool enemyControlled;
    private  bool controllingEnemy{
        get => enemyControlled;                 // el => es == a {}
        set {
            enemyControlled = value;            //le pasas un valor concreto, y hace la misma funcion que los 2 if de arriba
        }
    }
        //  Enemigo dentro del campo de vision, Enemigo dentro del campo de ataque
    private bool enemyInSightRange, enemyInAttackRange;
        //  Rango de vision y ataque en modo mascota.
    public float sightRangeControlled, attackRangeControlled, rangeAwayFromPlayer;
    private Transform enemy;
    public LayerMask whatIsEnemy, whatIsPlayer;
    private Collider[] enemies;
    private Transform ballPosition;
    private EnergyBall sphere;
    private Transform shootPosition;
    private AI_Enemy aiController;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        positionGuard = GameObject.Find("guardPosition").transform;
        ballPosition =  transform.GetChild(1).transform;    // 1 = Ball Position.
        shootPosition = transform.GetChild(2).transform;    // 2 = Shot Position.
        aiController = GetComponent<AI_Enemy>();
    }
    //  Following the player as pet
    private void FollowPlayer(){
        agent.SetDestination(positionGuard.position);
        transform.rotation = player.rotation;
    }
    private void ControlingEnemy()
    {
        //si no hay ningun enemigo controlado por el jugador lo hace directamente
        if(!pet){
            if(!controllingEnemy){
                enemyControl();
            }
        }else{
            //Si el enemigo es distinto al que controla el jugador, el que tiene el jugador se elimina y se pone el nuevo.
            if(pet != gameObject){
                if(!controllingEnemy){
                    Destroy(pet.gameObject); // Se destruye porque hay un bug
                    sphereControlling.movements = -1; // Poniendo la esfera anterior a que vuelva a su posicion
                    enemyControl();
                } //if
            }
        }// fin Else
    }
    private void enemyControl()
    {
        sphereControlling = sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
        sphereControlling.movements = 1;                         // Poniendo la esfera en estado de control
        usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
        controllingEnemy = true;                       // Controlando Enemigo
        pet = gameObject;                 // La mascota es el transform
        gameObject.layer = 11;  //   Layer: Pet         // Poniendo en layer Pet para que los enemigos puedan encontrar la mascota y atacarla
        agent.speed = 7;                                // Agent Speed Growing
        agent.acceleration = 100;                       // Agent A: Growing
    }
    public void StopControlingEnemy()
    {
                sphereControlling.movements =-1; // La esfera vuelve a su posicion
                sphereControlling.modes = -1;     // Mientras vuelve se le quita el modo 
                sphereControlling = null;         // Global = null
                controllingEnemy = false;                       
                pet = null;
                //devuelve al enemigo a su estado original
                gameObject.layer = 10;  //   Layer: Enemy
                gameObject.tag = GameConstants.ENEMY_TAG;
                agent.speed = 4;
                agent.acceleration = 20;
    }
    

    private void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRangeControlled);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRangeControlled);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeAwayFromPlayer);
    }
    void Update()
    {
        if(enemyControlled){
            if(sphereControlling.movements != 1){
                sphereControlling.movements = 1;
            }
            // Solo mueve la esfera en la misma posicion si es la que controla el enemigo
            if(sphereControlling.movements == 1){
                //Debug.Log("Moviendo");
                sphereControlling.transform.position = Vector3.Lerp(sphereControlling.transform.position, ballPosition.position,10f*Time.deltaTime);
                //sphereControlling.transform.position = ballPosition.position;
                //sphereControlling.transform.rotation = ballPosition.transform.rotation;
            }
            //Si el jugador controla el enemigo se ejecutan estas lineas de codigo            
            enemyInSightRange = Physics.CheckSphere(transform.position, sightRangeControlled, whatIsEnemy);
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRangeControlled, whatIsEnemy);

            enemies = Physics.OverlapSphere(transform.position, 10, whatIsEnemy);
            foreach(Collider c in enemies){
                if(c.tag == GameConstants.ENEMY_TAG){
                    enemy = c.transform;
                }
            }

            // Si esta fuera de rango del jugador le seguira
            bool playerInRange = Physics.CheckSphere(transform.position, rangeAwayFromPlayer, whatIsPlayer);
            if(!playerInRange)
            {
                FollowPlayer();
            }else
            {
                if(!enemyInSightRange && !enemyInAttackRange){
                    FollowPlayer();
                }
                if(enemy){
                    aiController.enemyFunctions(enemyInSightRange,enemyInAttackRange, enemy);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sphere = other.gameObject.GetComponent<EnergyBall>();

        if(sphere != null){
            //si la bola esta en modo controlar
            if(sphere.modes == 2){
                ControlingEnemy();
            }
        }
    }

   
}
