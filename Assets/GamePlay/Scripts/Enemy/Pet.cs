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
    private BallScript ball;


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

    //Gizmos
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
            
            if(ball != null ){
                ball.gameObject.transform.position = _BallPosition.position;
            }

            //Si se pulsa click derecho el enemigo deja de controlarse
            if(Input.GetKeyDown(KeyCode.Mouse1)){
                _ControllingEnemy = false;
                ControlEnemies._HasPet = false;
                ControlEnemies.Pet = null;

                //devuelve al enemigo a su estado original
                gameObject.layer = 10;  //   Layer: Enemy
                gameObject.tag = "Enemy";
                agent.speed = 4;
                agent.acceleration = 20;
                Destroy(ball);
            }
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        ball =  other.gameObject.GetComponent<BallScript>();

        if(ball != null){
            //si la bola esta en modo controlar
            if(ball._controlling){
                    //si no hay ningun enemigo controlado por el jugador lo hace directamente
                if(!ControlEnemies._HasPet){
                    if(!_ControllingEnemy){
                                //controlando el enemigo
                                _ControllingEnemy = ball._controlling;
                                ControlEnemies._HasPet = true;
                                ControlEnemies.Pet = gameObject;
                                gameObject.layer = 11;  //   Layer: Pet
                                gameObject.tag = "Pet";
                                agent.speed = 7;
                                agent.acceleration = 100;
                        }
                }else{
                    //Si el enemigo es distinto al que controla el jugador, el que tiene el jugador se elimina y se pone el nuevo.
                    if(ControlEnemies.Pet != gameObject){
                        if(!_ControllingEnemy){
                                Destroy(ControlEnemies.Pet.gameObject);
                                _ControllingEnemy = ball._controlling;
                                ControlEnemies._HasPet = true;
                                ControlEnemies.Pet = gameObject;
                                //turning layer into pet
                                gameObject.layer = 11;  //   Layer: Pet
                                gameObject.tag = "Pet";
                                agent.speed = 7;
                                agent.acceleration = 100;
                        } //if
                    } //if
                }// fin Else
            }
        }
    }
}
