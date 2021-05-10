using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : BaseGame
{
    private BossShiled shield;
    public BossShiled GetShield{
        get => shield;
    }
    private NavMeshAgent agent;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsPet;

    // Enemy 
    [SerializeField]
    public float health = 100f;
    private Transform shootPosition;

    public GameObject shipPart;
    public GameObject bolts;
    
    //  Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 0;

    //  Attacking
    public float timeBetweenAttacks = 2;
    bool alreadyAttacked;
    private bool abilityUsed;
    public GameObject projectile;

    //  States
    public float sightRange = 10, attackRange = 5;
    private bool playerInSightRange, playerInAttackRange, petInSightRange, petInAttackRange;
    private Vector3 defaultPosition;

    //  BOSS = ?
    private Transform BossHead;
    private LaserControls laserControls;

    void Start()
    {
        shield = GetComponentInChildren<BossShiled>();
        laserControls = GetComponent<LaserControls>();
        defaultPosition = transform.position;
    }
    private void Awake()
    {
        player = GameObject.Find("Neck").transform;
        BossHead = player = GameObject.Find("Cabeza_Hueso").transform;
        agent = GetComponent<NavMeshAgent>();
        shootPosition = transform.GetChild(2).transform;
    }

    private void chase(Transform objectTransform)
    {
        if(agent){
            agent.SetDestination(objectTransform.position);
            transform.LookAt(objectTransform);
            BossHead.LookAt(objectTransform);
        }
    }

    private void goDefaultPos(){
        if(agent){
            agent.SetDestination(defaultPosition);
        }
    }
    private void attack(Transform objectTransform)
    {

        if(shield){
            if(agent){
                agent.SetDestination(transform.position);
            
                transform.LookAt(objectTransform);
                BossHead.LookAt(objectTransform);

                if(!laserControls.getActiveLaser){
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
        }else 
        {
            if(!laserControls.getActiveLaser){

                agent.SetDestination(transform.position);
                transform.LookAt(objectTransform);
                BossHead.LookAt(objectTransform);
                if(!alreadyAttacked)
                {
                    float playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;

                    if(!abilityUsed)
                    {
                        playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;
                        playerHealth -= 10;
                        GameObject.Find("Player").GetComponent<CharHealth>().health = playerHealth;
                        abilityUsed = true;
                        Invoke(nameof(ResetAbility), 10);
                    }
                    playerHealth -= 10;
                    GameObject.Find("Player").GetComponent<CharHealth>().health = playerHealth;
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
        }
    }

    private void ResetAbility()
    {
        abilityUsed = false;

    }
    
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void CreateDrop()
    {
        
        GameObject itemSpaceShip =  Instantiate(shipPart, gameObject.transform) as GameObject;
        itemSpaceShip.transform.parent = null;

        for(int i = 0; i <= Random.Range(40f, 50f); i++)
        {   
            // Lo creo para que no coja el prefab.
            GameObject tornilloCreado = Instantiate(bolts, gameObject.transform) as GameObject;
            tornilloCreado.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            tornilloCreado.transform.parent = null;
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
        if(!shield){
            attackRange = 4f;
        }
        player = GameObject.Find("Neck").transform;
        if(health <= 0) CreateDrop();  
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) goDefaultPos();
        enemyFunctions(playerInSightRange, playerInAttackRange, player);   
        
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

        MovableObjects movableObject = other.gameObject.GetComponent<MovableObjects>();
        if(movableObject != null)
        {
            health -= movableObject.ShotDamage+10;
        }
    }
}
