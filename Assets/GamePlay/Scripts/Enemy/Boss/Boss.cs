using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : BaseGame
{
    private BossShiled shield;
    
private NavMeshAgent agent;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsPet;

    // Enemy 
    [SerializeField]
    public float health = 20f;
    private Transform shootPosition;

    public GameObject tornillo;
    
    //  Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 0;

    //  Attacking
    public float timeBetweenAttacks = 2;
    bool alreadyAttacked;
    public GameObject projectile;

    //  States
    public float sightRange = 10, attackRange = 5;
    private bool playerInSightRange, playerInAttackRange, petInSightRange, petInAttackRange;
    private Vector3 defaultPosition;

    //  BOSS = ?

    void Start()
    {
        shield = GetComponentInChildren<BossShiled>();
        defaultPosition = transform.position;
    }
    private void Awake()
    {
        player = GameObject.Find("Neck").transform;
        agent = GetComponent<NavMeshAgent>();
        shootPosition = transform.GetChild(2).transform;
    }

    private void chase(Transform objectTransform)
    {
        if(agent){
            agent.SetDestination(objectTransform.position);
            transform.LookAt(objectTransform);
        }
    }

    private void goDefaultPos(){
        agent.SetDestination(defaultPosition);
    }
    private void attack(Transform objectTransform)
    {

        if(shield){
            if(agent){
                agent.SetDestination(transform.position);
            
                transform.LookAt(objectTransform);
                if(!alreadyAttacked)
                {
                    //Attack Code
                    Rigidbody rb = Instantiate(projectile, shootPosition.position ,Quaternion.identity).GetComponent<Rigidbody>();

                    rb.AddForce(transform.forward *32f, ForceMode.Impulse);

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }    
        }
        
    }
    
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void CreateDrop()
    {
        
        GameObject itemSpaceShip =  Instantiate(tornillo, gameObject.transform) as GameObject;
        itemSpaceShip.transform.parent = null;

        agent = null;
        Destroy(gameObject);
    }
    private void TakeDamage(EnergyBall esfera)
    {
        if(esfera.modes == 0){
            health -= 2.5f;
        }
        if(health <= 0) CreateDrop();  
    }

    //Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
    void Update()
    {   
        if(!shield){
            attackRange = 2;
        }
        player = GameObject.Find("Neck").transform;
        if(health <= 0) CreateDrop();  
        //si no tiene mascota la intelgencia aritificial siempre va a pegar al jugador   
        if(!pet){
            //checks if player is in sight range and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if(!playerInSightRange && !playerInAttackRange) goDefaultPos();
            enemyFunctions(playerInSightRange, playerInAttackRange, player);   

        }else{
            //Si tiene mascota, comprueba que si es distinta a la mascota elegida pegara al jugador y la mascota
            if(pet != gameObject){
                petInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPet);
                petInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPet);

                enemyFunctions(petInSightRange, petInAttackRange, pet.transform);   

                // Same Code, with diferent layers, Siempre va a pegar al jugador si esta mas cerca
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                if(!petInSightRange || playerInSightRange)
                {
                    if(!playerInSightRange && !playerInAttackRange) goDefaultPos();
                    enemyFunctions(playerInSightRange, playerInAttackRange, player);   
                }
            }
        }    
    }

    public void enemyFunctions(bool inSightRange, bool inAttackRange, Transform objectTransform)
    {
        if (inSightRange && !inAttackRange)
        {
            chase(objectTransform);
        } 
        if (inSightRange && inAttackRange)
        {
            
            attack(objectTransform);
        }    
    }
    public void OnTriggerEnter(Collider other) 
    {
        if(!shield)
        {
            if(other.gameObject.tag == GameConstants.ESFERA_TAG)
            {
                if(!pet)
                {
                    TakeDamage(other.GetComponent<EnergyBall>());
                }else if(pet != gameObject)
                {
                    TakeDamage(other.GetComponent<EnergyBall>());
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(!shield)
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if(projectile != null)
            {
                health -= 2f;
                Destroy(projectile);
            }
        }
    }
}
