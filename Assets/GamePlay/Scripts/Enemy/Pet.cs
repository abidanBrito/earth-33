using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pet : BaseGame
{
    public NavMeshAgent agent;
    public Transform player;
    public float health;
    //  Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //  Enemy under control by player
    public Transform positionGuard;
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
    public Transform ballPosition;
    public Esfera sphere;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        positionGuard = GameObject.Find("PositionPet").transform;
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void Takedamage(int damage){
            health -=damage;
            if(health <=0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    
    private void DestroyEnemy()
    {
        Destroy(gameObject);    
    }

    //  Following the player as pet
    private void FollowPlayer(){
        agent.SetDestination(positionGuard.position);
        transform.rotation = player.rotation;
    }
    private void ChaseEnemy()
    {
        agent.SetDestination(enemy.position);
    }
    private void AttackEnemy()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(enemy);

        if(!alreadyAttacked)
        {
            //Attack Code
            Rigidbody rb = Instantiate(projectile, transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward *32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ControlingEnemy()
    {
        //si no hay ningun enemigo controlado por el jugador lo hace directamente
                if(!hasPet){
                    if(!controllingEnemy){
                                sphere.movements = 1;                         // Poniendo la esfera en estado de control
                                sphereControlling = sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
                                usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
                                controllingEnemy = true;                       // Controlando Enemigo
                                hasPet = true;                   // Poniendo que el jugador tiene mascota
                                pet = gameObject;                 // La mascota es el transform
                                gameObject.layer = 11;  //   Layer: Pet         // Poniendo en layer Pet para que los enemigos puedan encontrar la mascota y atacarla
                                agent.speed = 7;                                // Agent Speed Growing
                                agent.acceleration = 100;                       // Agent A: Growing
                        }
                }else{
                    //Si el enemigo es distinto al que controla el jugador, el que tiene el jugador se elimina y se pone el nuevo.
                    if(pet != gameObject){
                        if(!controllingEnemy){
                                Destroy(pet.gameObject);
                                sphereControlling.movements = -1; // Poniendo la esfera anterior a que vuelva a su posicion
                                sphereControlling = sphere;       // Poniendo la nueva esfera como la actual controlando el enemigo
                                usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
                                sphere.movements = 1;                         // La nueva esfera pasa a estar en estado controlando
                                controllingEnemy = true;                       // Enemigo controlado
                                hasPet = true;                   // Pet = true
                                pet = gameObject;                 // La nueva mascota es el nuevo tranform
                                gameObject.layer = 11;  //   Layer: Pet         // Poniendo en layer Pet para que los enemigos puedan encontrar la mascota y atacarla
                                agent.speed = 7;                                // Agent Speed
                                agent.acceleration = 100;                       // Agent ac
                        } //if
                    }
                     //if
                }// fin Else
    }
    public void StopControlingEnemy()
    {
                sphereControlling.movements =-1; // La esfera vuelve a su posicion
                sphereControlling.modes = -1;     // Mientras vuelve se le quita el modo 
                sphereControlling = null;         // Global = null
                controllingEnemy = false;                       
                hasPet = false;
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
            // Solo mueve la esfera en la misma posicion si es la que controla el enemigo
            if(sphereControlling.movements == 1){
                sphereControlling.transform.position = ballPosition.position;
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
            if(!playerInRange){
                FollowPlayer();
            }else{
                if(!enemyInSightRange && !enemyInAttackRange) FollowPlayer();
                if(enemyInSightRange && !enemyInAttackRange)   ChaseEnemy();
                if(enemyInSightRange && enemyInAttackRange)   AttackEnemy();
            }
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        sphere = other.gameObject.GetComponent<Esfera>();

        if(sphere != null){
            //si la bola esta en modo controlar
            if(sphere.modes == 2){
                ControlingEnemy();
            }
        }
    }
}
