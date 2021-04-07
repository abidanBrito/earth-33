using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pet : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float health;
    //  Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //  Enemy under control by player
    public Transform _PositionGuard;
    private bool _EnemyControlled;
    private  bool _ControllingEnemy{
        get => _EnemyControlled;                 // el => es == a {}
        set {
            _EnemyControlled = value;            //le pasas un valor concreto, y hace la misma funcion que los 2 if de arriba
        }
    }
        //  Enemigo dentro del campo de vision, Enemigo dentro del campo de ataque
    private bool _EnemyInSightRange, _EnemyInAttackRange;
        //  Rango de vision y ataque en modo mascota.
    public float _sightRangeControlled, _attackRangeControlled, _RangeAwayFromPlayer;
    private Transform _Enemy;
    public LayerMask whatIsEnemy, whatIsPlayer;
    private Collider[] _Enemies;
    public Transform _BallPosition;
    public Esfera _sphere;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _PositionGuard = GameObject.Find("PositionPet").transform;
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
        agent.SetDestination(_PositionGuard.position);
        transform.rotation = player.rotation;
    }
    private void ChaseEnemy()
    {
        agent.SetDestination(_Enemy.position);
    }
    private void AttackEnemy()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(_Enemy);

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
                if(!GameConstants._HasPet){
                    if(!_ControllingEnemy){
                                _sphere._Movements = 1;                         // Poniendo la esfera en estado de control
                                GameConstants._sphereControlling = _sphere;     // Poniendo la esfera en las constantes para tener en cuenta cual es la que esta siendo utilizada para controlar
                                GameConstants._usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
                                _ControllingEnemy = true;                       // Controlando Enemigo
                                GameConstants._HasPet = true;                   // Poniendo que el jugador tiene mascota
                                GameConstants.Pet = gameObject;                 // La mascota es el transform
                                gameObject.layer = 11;  //   Layer: Pet         // Poniendo en layer Pet para que los enemigos puedan encontrar la mascota y atacarla
                                agent.speed = 7;                                // Agent Speed Growing
                                agent.acceleration = 100;                       // Agent A: Growing
                        }
                }else{
                    //Si el enemigo es distinto al que controla el jugador, el que tiene el jugador se elimina y se pone el nuevo.
                    if(GameConstants.Pet != gameObject){
                        if(!_ControllingEnemy){
                                Destroy(GameConstants.Pet.gameObject);
                                GameConstants._sphereControlling._Movements = -1; // Poniendo la esfera anterior a que vuelva a su posicion
                                GameConstants._sphereControlling = _sphere;       // Poniendo la nueva esfera como la actual controlando el enemigo
                                GameConstants._usingBall = false;               // Poniendo el estado de usando esferas a falso para que las demas puedan ser utilizadas
                                _sphere._Movements = 1;                         // La nueva esfera pasa a estar en estado controlando
                                _ControllingEnemy = true;                       // Enemigo controlado
                                GameConstants._HasPet = true;                   // Pet = true
                                GameConstants.Pet = gameObject;                 // La nueva mascota es el nuevo tranform
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
                GameConstants._sphereControlling._Movements =-1; // La esfera vuelve a su posicion
                GameConstants._sphereControlling._Mode = -1;     // Mientras vuelve se le quita el modo 
                GameConstants._sphereControlling = null;         // Global = null
                _ControllingEnemy = false;                       
                GameConstants._HasPet = false;
                GameConstants.Pet = null;
                //devuelve al enemigo a su estado original
                gameObject.layer = 10;  //   Layer: Enemy
                gameObject.tag = "Enemy";
                agent.speed = 4;
                agent.acceleration = 20;
    }
    

    private void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRangeControlled);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightRangeControlled);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _RangeAwayFromPlayer);
    }
    void Update()
    {
        if(_EnemyControlled){
            // Solo mueve la esfera en la misma posicion si es la que controla el enemigo
            if(GameConstants._sphereControlling._Movements == 1){
                GameConstants._sphereControlling.transform.position = _BallPosition.position;
                //GameConstants._sphereControlling.transform.rotation = _BallPosition.transform.rotation;
            }
            //Si el jugador controla el enemigo se ejecutan estas lineas de codigo            
            _EnemyInSightRange = Physics.CheckSphere(transform.position, _sightRangeControlled, whatIsEnemy);
            _EnemyInAttackRange = Physics.CheckSphere(transform.position, _attackRangeControlled, whatIsEnemy);

            _Enemies = Physics.OverlapSphere(transform.position, 10, whatIsEnemy);
            foreach(Collider c in _Enemies){
                if(c.tag == "Enemy"){
                    _Enemy = c.transform;
                }
            }

            // Si esta fuera de rango del jugador le seguira
            bool playerInRange = Physics.CheckSphere(transform.position, _RangeAwayFromPlayer, whatIsPlayer);
            if(!playerInRange){
                FollowPlayer();
            }else{
                if(!_EnemyInSightRange && !_EnemyInAttackRange) FollowPlayer();
                if(_EnemyInSightRange && !_EnemyInAttackRange)   ChaseEnemy();
                if(_EnemyInSightRange && _EnemyInAttackRange)   AttackEnemy();
            }
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        _sphere = other.gameObject.GetComponent<Esfera>();

        if(_sphere != null){
            //si la bola esta en modo controlar
            if(_sphere._Mode == 2){
                ControlingEnemy();
            }
        }
    }
}
