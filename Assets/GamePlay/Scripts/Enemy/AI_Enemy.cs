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

    // Rock hit push
    [SerializeField] private float enemyStuntTime = 2f;
    public GameObject tornillo;
    
    //  Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //  Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] private bool isArcher = false;
    public GameObject projectile;

    //  States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange, petInSightRange, petInAttackRange;

    //  BOSS = ?
    public GameObject explosionEffect;

    private Animator animatorController;
    private bool isDead = false;
    private WeaponMelee meleeWeapon;

    private void Awake() // Comprobado
    {
        //Guarda la posicion del juegador y el NavMesh
        player = GameObject.Find("Neck").transform;
        agent = GetComponent<NavMeshAgent>();
        
        //Comprueba que no es ubn Healer
        if(gameObject.tag != GameConstants.HEALER_TAG)
        {
            shootPosition = transform.GetChild(2).transform;
        }

        animatorController = GetComponentInChildren<Animator>();
        meleeWeapon = GetComponentInChildren<WeaponMelee>();
    }

    void Update() //Comprobado
    {   
        //Comprueva la posicion del jugador
        // player = GameObject.Find("Neck").transform;

        //Comprueba si esta muerto. Si lo esta llama a la funcion de dropeo
        if(!isDead){
            if(health <= 0) CreateDrop();  

            //Si no tiene mascota la intelgencia aritificial siempre va a pegar al jugador   
            if(gameObject.tag == GameConstants.ENEMY_TAG)
            {
                CheckEnemiesNear();
            }
        }
    }

    private void CheckEnemiesNear() //Comprobado
    {
        if(!pet) // Comprovado
        {
            //Detecta si el jugador esta dentro del rango de visión y de ataque
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //Si el jugador no se encuentra en ningun rango, sigue patrullando
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            enemyFunctions(playerInSightRange, playerInAttackRange, player);   
        }
        else
        {
            //Si es la mascota...
            if(pet != gameObject)
            {
                //Detecta si la mascota esta dentro del rango de visión y de ataque    
                petInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPet);
                petInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPet);

                //Detecta si el jugador esta dentro del rango de visión y de ataque
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                //Si no encuentra al jugador...
                if(!playerInSightRange && !playerInAttackRange)
                {
                    enemyFunctions(petInSightRange, petInAttackRange, pet.transform); 
                }

                //Si no detecta a la mascota pero el jugador esta a rango, ira directamente a por el
                if(!petInSightRange || playerInSightRange)
                {
                    //Si no detecta al jugador, continua patrullando
                    if (!playerInSightRange && !playerInAttackRange) Patroling();

                    enemyFunctions(playerInSightRange, playerInAttackRange, player);   
                }
            }
        }    
    }

    public void Patroling() //Comprobado
    {
        //Si aun no tiene un punto de ruta, la busca
        if(!walkPointSet) SearchWalkPoint();

        //Si lo tiene...
        if(walkPointSet)
        {
            //Utiliza el NavMesh y marca el punto como destino
            if(agent) agent.SetDestination(walkPoint); 
        }

        //Distancia entre el Enemy y el punto de ruta
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Si ya ha llegado al punto de ruta resetea el punto
        if(distanceToWalkPoint.magnitude <= 0f) walkPointSet = false;
    }

    public void enemyFunctions(bool inSightRange, bool inAttackRange, Transform objectTransform) //Comprobado
    {
        //Si detecta el objetivo pero no esta en el rango de ataque va hacia el objetivo
        if (inSightRange && !inAttackRange)
        {
            chase(objectTransform);
        } 
        //Si lo detecta y esta a rango lo ataca
        else if (inSightRange && inAttackRange)
        {
            attack(objectTransform);
        }    
    }

    public void chase(Transform objectTransform) //Comprobado
    {
        //Convierte al punto de destino en el punto donde esta el objeto detectado
        if(agent)
        {
            agent.SetDestination(objectTransform.position);
            transform.LookAt(objectTransform);
        }
    }

    private void SearchWalkPoint() //comprobado
    {
        //Busca un punto aleatorio dentro del rango de ruta
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    
        //Avisa de que ya hay establecido un nuevo punto de ruta
        if(Physics.Raycast(walkPoint,-transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    public void attack(Transform objectTransform) //Comprobado
    {
        if(agent)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(objectTransform);

            //Si ya puede atacar...
            if(!alreadyAttacked)
            {
                if(!isArcher)
                {
                    animatorController.SetBool("ATTACKING", true);
                    AI_Enemy enemyToAttack = objectTransform.GetComponent<AI_Enemy>();
                    if(enemyToAttack != null && enemyToAttack != gameObject){
                        enemyToAttack.health -=2f;
                    }
                    // Señala que acaba de atacar
                    meleeWeapon.impacted = false;
                    alreadyAttacked = true;
                    //Al cabo de un tiempo resetea el ataque
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                } 
                else
                {
                    shootPosition.LookAt(objectTransform);
                    //Instancia un proyectil y le aplica una fuerza con direccion al objetivo
                    GameObject rb = Instantiate(projectile, shootPosition.position ,shootPosition.rotation);
                    //Señala que acaba de atacar
                    alreadyAttacked = true;

                    //Al cabo de un tiempo resetea el ataque
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                
            }else{
                if(!isArcher){
                    animatorController.SetBool("ATTACKING", false);
                }
            }
        }
    }

    private void ResetAttack() //Comprobado
    {
        //Resetea el ataque
        alreadyAttacked = false;
    }

    public void CreateDrop()//Comprobado
    {
        if(!isDead){
            isDead = true;
            animatorController.SetBool("DEAD", true);
            //Creamos un numero aleatorio de tornillos como dropeo
            for(int i = 0; i <= Random.Range(3f, 6f); i++)
            {   
                //Instancia un tornillo
                GameObject tornilloCreado = Instantiate(tornillo, gameObject.transform) as GameObject;

                //Le quita el parent
                tornilloCreado.transform.parent = null;
            }

            //Si es una mascota, le quita el control al jugador
            if(pet == gameObject)
            {
                Pet petController = pet.GetComponent<Pet>();
                petController.StopControlingEnemy();
            }
            
            //Elimina el NavMesh y destruye el objeto
            agent = null;
            Destroy(GetComponent<Pet>());
            Destroy(GetComponent<AI_Enemy>());
            Destroy(gameObject, 5f);
        }
        
    }

    private void takeDamage(EnergyBall sphere) //Comprobado
    {
        //Si las esfera que le impacta está en modo de ataque
        //le quita salud al Enemy
        if (sphere.modes == 0)
        {
            health -= 2.5f;

            //Crea el drope (Tornillos) si la salud es 0
            if (health <= 0) CreateDrop(); 
        } 
    }
    private void takeDamage(float damage) //Comprobado
    {
        health -= damage;

        //Crea el drope (Tornillos) si la salud es 0
        if (health <= 0) CreateDrop(); 
    }

    private void takeDamage(MovableObjects rock)  //Comprobado
    {
        //El Enemy pierde salud equivalente al daño de la roca 
        //que le han lanzado
        health -= rock.ShotDamage; 

        //Crea el drop (Tornillos) si la salud es 0
        if (health <= 0) CreateDrop();  
    }

    private void OnDrawGizmosSelected() //Comprobado
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }

    private void OnTriggerEnter(Collider other) //Comprobado
    {
        switch (other.gameObject.tag) 
        {
            //Si el trigger detectada es una esfera...
            case GameConstants.ESFERA_TAG:

                //Si no es una mascota o el no es la mascota...
                if (!pet || pet != gameObject) 
                {
                    //Sufre daño
                    takeDamage(other.GetComponent<EnergyBall>());
                }

                break;

            default: break;
        }
    }
    
    private void OnCollisionEnter(Collision other) //Comprobado
    {
        //Si se detecta una colision...
        switch (other.gameObject.tag) 
        {
            //Si la colision es un proyectil de un Enemy...
            case GameConstants.PROJECTILE_TAG:

                if (other.gameObject.GetComponent<Projectile>() != null)
                {
                    //Le quita vida y se destruye
                    health -= 2f;
                    Destroy(other.gameObject.GetComponent<Projectile>());
                }

                break;
            
            //Si la colision es un objeto movible por el jugador
            case GameConstants.MOVABLE_OBJECTS_TAG:

                if (collectedObject == null) 
                {
                    MovableObjects rock = other.gameObject.GetComponent<MovableObjects>();

                    //Si no ha sido golpeado con ella le hace daño
                    if(!rock.AlreadyHitted)
                    {
                        takeDamage(rock);
                        rock.AlreadyHitted = true;
                    }

                    launchEnemy(rock);
                }

                break;

            case GameConstants.BULLET:
                ProjectileMoveScript projectile = other.gameObject.GetComponent<ProjectileMoveScript>();
                if(!projectile.impacted){
                    health -= 2f;
                    projectile.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
                }

                break;
                
            case GameConstants.MELEE_WEAPON:
                WeaponMelee weapon =  other.gameObject.GetComponent<WeaponMelee>();
                    if(!weapon.impacted){
                        health -= 2f;
                        weapon.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
                    }
                break;
            default: break;
        }
    }

    private void launchEnemy(MovableObjects rock) //Comprobado
    {
        if (!gameObject.GetComponent<Rigidbody>())
        {
            // Disable certain enemy components
            gameObject.GetComponent<AI_Enemy>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false; 
            gameObject.GetComponent<Pet>().enabled = false;
            
            // Add rigidbody for collisions
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(20f * rock.ShotDirection, ForceMode.Impulse);
                StartCoroutine(delay2Seconds(rb));
            }
        }
    }

    IEnumerator delay2Seconds(Rigidbody rb) //Comprobado
    {
        yield return new WaitForSecondsRealtime(enemyStuntTime);
        Destroy(rb);

        // Reset enemy components
        gameObject.GetComponent<AI_Enemy>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true; 
        gameObject.GetComponent<Pet>().enabled = true;
    }
}

