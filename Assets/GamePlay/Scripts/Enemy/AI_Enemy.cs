using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_Enemy : BaseGame
{

    private NavMeshAgent agent;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsPet;

    // Enemy 
    [SerializeField]
    public float health = 10f;
    private Transform shootPosition;

    public GameObject tornillo;
    
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
    private bool playerInSightRange, playerInAttackRange, petInSightRange, petInAttackRange;

    //  BOSS = ?
    public bool isBossEnemy = false;
    public GameObject explosionEffect;

    private void Awake()
    {
        player = GameObject.Find("Neck").transform;
        agent = GetComponent<NavMeshAgent>();
        shootPosition = transform.GetChild(2).transform;
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet){
            if(agent){
                agent.SetDestination(walkPoint);
            }
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

    public void chase(Transform objectTransform)
    {
        if(agent){
            agent.SetDestination(objectTransform.position);
            transform.LookAt(objectTransform);
        }

    }
    public void attack(Transform objectTransform)
    {
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
    
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void CreateDrop()
    {
        if(!isBossEnemy)
        {
            for(int i = 0; i <= Random.Range(3f, 6f); i++)
            {   
                // Lo creo para que no coja el prefab.
                GameObject tornilloCreado =  Instantiate(tornillo, gameObject.transform) as GameObject;
                tornilloCreado.transform.parent = null;
            }
            if(pet == gameObject)
            {
                Pet petController = pet.GetComponent<Pet>();
                petController.StopControlingEnemy();
            }
        }else if(isBossEnemy)
        {
            GameObject itemSpaceShip =  Instantiate(tornillo, gameObject.transform) as GameObject;
            itemSpaceShip.transform.parent = null;
        }
        
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
        player = GameObject.Find("Neck").transform;
        if(health <= 0) CreateDrop();  
        //si no tiene mascota la intelgencia aritificial siempre va a pegar al jugador   
        if(!pet){
            //checks if player is in sight range and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
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
                if (!playerInSightRange && !playerInAttackRange) Patroling();
                    enemyFunctions(playerInSightRange, playerInAttackRange, player);   
                }
            }
        }    
    }

    public void enemyFunctions(bool inSightRange, bool inAttackRange, Transform objectTransform)
    {
        if (inSightRange && !inAttackRange)
        {
            // ChasePlayer();
            chase(objectTransform);
        } 
        if (inSightRange && inAttackRange)
        {
            // AttackPlayer();
            attack(objectTransform);
        }    
    }
    public void OnTriggerEnter(Collider other) 
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
    private void OnCollisionEnter(Collision other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            health -= 2f;
            Destroy(projectile);
        }
    }
}
