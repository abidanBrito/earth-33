using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_Enemy : BaseGame
{

    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsPet;

    // Enemy 
    [SerializeField]
    public float health = 10f;
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

    //  Enemy under control by player

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
        agent.SetDestination(pet.transform.position);
    }

    private void AttackPet()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(pet.transform);
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

    public void CreateDrop()
    {
        for(int i = 0; i <= Random.Range(3f, 6f); i++)
        {   
            // Lo creo para que no coja el prefab.
            GameObject tornilloCreado =  Instantiate(tornillo, gameObject.transform) as GameObject;
            tornilloCreado.transform.parent = null;
        }

        Destroy(gameObject);
    }
    private void TakeDamage(Esfera esfera)
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
        //si no tiene mascota la intelgencia aritificial siempre va a pegar al jugador   
        if(!hasPet){
            //checks if player is in sight range and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange &&!playerInAttackRange) Patroling();
            if (playerInSightRange &&!playerInAttackRange)  ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();    

        }else{
            //Si tiene mascota, comprueba que si es distinta a la mascota elegida pegara al jugador y la mascota
            if(pet != gameObject){
                petInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPet);
                petInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPet);

                if (petInSightRange && !petInAttackRange) ChasePet(); 
                if (petInSightRange  && petInAttackRange) AttackPet(); 

                // Same Code, with diferent layers, Siempre va a pegar al jugador si esta mas cerca
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                if(!petInSightRange || playerInSightRange)
                {
                    if (!playerInSightRange && !playerInAttackRange) Patroling();
                    if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                    if (playerInSightRange && playerInAttackRange) AttackPlayer();    
                }
            }
        }    
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == GameConstants.ESFERA_TAG)
        {
            TakeDamage(other.GetComponent<Esfera>());
        }
    }
}
