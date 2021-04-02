using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_Enemy : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    //  Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //  Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //  States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    
    //  Enemy under control by player
    public Transform _PositionGuard;
    private bool _EnemyControlled;
    public  bool _ControllingEnemy{
        get => _EnemyControlled;                 // el => es == a {}
        set {
            _EnemyControlled = value;            //le pasas un valor concreto, y hace la misma funcion que los 2 if de arriba
        }
    }
        //  Enemigo dentro del campo de vision, Enemigo dentro del campo de ataque
    private bool _EnemyInSightRange, _EnemyInAttackRange;
        //  Rango de vision y ataque en modo mascota.
    public float _sightRangeControlled, _attackRangeControlled;
    private Transform _Enemy;
    public LayerMask whatIsEnemy, whatIsPet;
    private Collider[] _Enemies;
    public Transform _BallPosition;
    private BallScript ball;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _PositionGuard = GameObject.Find("PositionPet").transform;
        
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude <= 0f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    
        if(Physics.Raycast(walkPoint,-transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
        
    }

    private void ChasePlayer()
    {
       agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //  Enemy Doesnt Move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

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
    // Part of code to attack the player's
    private void ChasePet()
    {
        agent.SetDestination(ControlEnemies.Pet.transform.position);
    }

    private void AttackPet()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(ControlEnemies.Pet.transform);

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
        if(!_EnemyControlled){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, walkPointRange);
        }else{
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRangeControlled);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightRangeControlled);
        }
        
    }
    void Update()
    {
        //if not controlled by player
        if(!_EnemyControlled){

            //checks if player is in sight range and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange &&!playerInAttackRange) Patroling();
            if (playerInSightRange &&!playerInAttackRange)  ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();    

            //si el jugador tiene una mascota, tambien busca la mascota
            if(ControlEnemies._HasPet){
                // Same Code, with diferent layers
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPet);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPet);

                if (playerInSightRange && !playerInAttackRange)  ChasePet();
                if (playerInSightRange && playerInAttackRange) AttackPet(); 
            }

        }else{
            //Si el jugador controla el enemigo se ejecutan estas lineas de codigo            
            _EnemyInSightRange = Physics.CheckSphere(transform.position, _sightRangeControlled, whatIsEnemy);
            _EnemyInAttackRange = Physics.CheckSphere(transform.position, _attackRangeControlled, whatIsEnemy);

            _Enemies = Physics.OverlapSphere(transform.position, 10, whatIsEnemy);
            foreach(Collider c in _Enemies){
                if(c.tag == "Enemy"){
                    _Enemy = c.transform;
                }
            }

            if(!_EnemyInSightRange && !_EnemyInAttackRange) FollowPlayer();
            if(_EnemyInSightRange && !_EnemyInAttackRange)   ChaseEnemy();
            if(_EnemyInSightRange && _EnemyInAttackRange)   AttackEnemy();
            
            if(ball != null ){
                ball.gameObject.transform.position = _BallPosition.position;
            }

            if(Input.GetKeyDown(KeyCode.Mouse1)){
                _ControllingEnemy = false;
                ControlEnemies._HasPet = false;
                //turning layer and tag into enemy;
                gameObject.layer = 10;  //   Layer: Enemy
                gameObject.tag = "Enemy";
                Destroy(ball);
            }

        }
        
    
    }

    private void OnTriggerEnter(Collider other)
    {
        ball =  other.gameObject.GetComponent<BallScript>();

        //si la bola esta en modo controlar
        if(ball._controlling){
                //si no hay ningun enemigo controlado por el jugador lo hace directamente
            if(!ControlEnemies._HasPet){
                if(!_ControllingEnemy){
                        if(ball != null){
                            //controlando el enemigo
                            _ControllingEnemy = ball._controlling;
                            ControlEnemies._HasPet = true;
                            ControlEnemies.Pet = gameObject;
                            //turning layer into pet
                            gameObject.layer = 11;  //   Layer: Pet
                            gameObject.tag = "Pet";
                        }
                    }
            }else{
                //Si el enemigo es distinto al que controla el jugador, el que tiene el jugador se elimina y se pone el nuevo.
                if(ControlEnemies.Pet != gameObject){
                    if(!_ControllingEnemy){
                        if(ball != null){
                            Destroy(ControlEnemies.Pet.gameObject);
                            _ControllingEnemy = ball._controlling;
                            ControlEnemies._HasPet = true;
                            ControlEnemies.Pet = gameObject;
                            //turning layer into pet
                            gameObject.layer = 11;  //   Layer: Pet
                            gameObject.tag = "Pet";
                        } //if
                    } //if
                } //if
            }// fin Else
        }
    
        
    }
}
