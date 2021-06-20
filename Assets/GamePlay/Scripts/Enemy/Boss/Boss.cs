using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : BaseGame
{
    private BossShiled shield;
    public BossShiled GetShield
    {
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
    public WeaponMelee weaponHability;
    public WeaponMelee weaponBasic;

    //  States
    public float sightRange = 10, attackRange = 5, attackRangeMelee = 3;
    private bool playerInSightRange, playerInAttackRange, petInSightRange, petInAttackRange;
    private Vector3 defaultPosition;

    //  BOSS = ?
    private Transform BossHead;
    private LaserControls laserControls;
    private Animator bossAnimator;

    private RangerShoot rangerSound;
    private DeathMob deathMob;




    private void Awake()
    {
         player = GameObject.Find("Neck").transform;
        BossHead = GameObject.Find("Cabeza_Hueso").transform;
        agent = GetComponent<NavMeshAgent>();
        shootPosition = transform.GetChild(2).transform;
        laserControls = GetComponent<LaserControls>();
        shield = GetComponentInChildren<BossShiled>();
        bossAnimator = GetComponentInChildren<Animator>();
        rangerSound = GetComponent<RangerShoot>();
        deathMob = GetComponent<DeathMob>();
    }
    void Start()
    {
        defaultPosition = transform.position;
    }


    private void chase(Transform objectTransform)
    {
        if (agent)
        {
            transform.LookAt(objectTransform);
            BossHead.LookAt(objectTransform);
            if (!laserControls.getActiveLaser)
            {
                agent.SetDestination(objectTransform.position);
            }
        }
    }

    private void goDefaultPos()
    {
        if (agent)
        {
            agent.SetDestination(defaultPosition);
        }
    }
    private void attack(Transform objectTransform)
    {

        if (shield)
        {
            if (agent)
            {
                transform.LookAt(objectTransform);
                BossHead.LookAt(objectTransform);
                if (!laserControls.getActiveLaser)
                {
                    agent.SetDestination(transform.position);

                    if (!alreadyAttacked)
                    {
                        shootPosition.LookAt(player);
                        //Attack Code
                        GameObject gObj = Instantiate(projectile, shootPosition.position, shootPosition.rotation);
                        if(!laserControls.laserSoundActivated)
                            rangerSound.PlaySound();
                            AudioSource audios = gObj.AddComponent<AudioSource>();
                            ProjectileImpact proyectil = gObj.AddComponent<ProjectileImpact>();
                            AudioClip clip = Resources.Load<AudioClip>("ImpactoProyectil");
                            proyectil.impact = clip;

                            alreadyAttacked = true;
                            bossAnimator.SetBool("AttackDistance", true);
                            Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    }
                    else
                    {
                        bossAnimator.SetBool("AttackDistance", false);
                    }
                }
            }
        }
        else
        {
            transform.LookAt(objectTransform);
            BossHead.LookAt(objectTransform);
            if (!laserControls.getActiveLaser)
            {
                if (agent)
                {
                    agent.SetDestination(transform.position);
                    if (!abilityUsed)
                    {
                        abilityUsed = true;
                        bossAnimator.SetBool("AbilityAttack", true);
                        weaponHability.impacted = false;
                        Invoke(nameof(ResetAbility), 10);
                    }
                    else
                    {
                        bossAnimator.SetBool("AbilityAttack", false);
                        if (!alreadyAttacked)
                        {
                            bossAnimator.SetBool("AttackMelee", true);
                            alreadyAttacked = true;
                            weaponBasic.impacted = false;
                            Invoke(nameof(ResetAttack), timeBetweenAttacks);

                        }
                        else
                        {
                            bossAnimator.SetBool("AttackMelee", false);
                        }
                    }
                }

            }
        }
    }

    private void ResetAbility()
    {
        abilityUsed = false;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void CreateDrop()
    {

        GameObject itemSpaceShip = Instantiate(shipPart, gameObject.transform) as GameObject;
        itemSpaceShip.transform.parent = null;

        Vector3 deadPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        transform.position = deadPosition;
        bossAnimator.SetFloat("Boss_HP", -1);
        Destroy(agent);
        Destroy(GetComponent<Boss>());
        Destroy(gameObject, 15f);
        Destroy(GetComponent<Collider>());
    }

    private void TakeDamage(EnergyBall esfera)
    {
        if (esfera.modes == 0)
        {
            health -= 30f;
        }
        if (health <= 0){
            deathMob.PlaySoundOnDeath();
            CreateDrop();
        } 
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
        if (!shield)
        {
            attackRange = attackRangeMelee;
        }
        if (health <= 0) CreateDrop();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) goDefaultPos();
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
        if (!shield)
        {
            if (other.gameObject.tag == GameConstants.ESFERA_TAG)
            {
                if (!pet)
                {
                    EnergyBall energyBall = other.GetComponent<EnergyBall>();

                    if (energyBall.modes == 0)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactAttack, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }
                    if (energyBall.modes == 1)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactControl, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }
                    if (energyBall.modes == 2)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactPosession, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }

                    TakeDamage(other.GetComponent<EnergyBall>());
                }
                else if (pet != gameObject)
                {
                    EnergyBall energyBall = other.GetComponent<EnergyBall>();

                    if (energyBall.modes == 0)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactAttack, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }
                    if (energyBall.modes == 1)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactControl, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }
                    if (energyBall.modes == 2)
                    {
                        GameObject vfx = GameObject.Instantiate(energyBall.vfxImpactPosession, energyBall.transform.position, transform.rotation);
                        Destroy(vfx, 2f);
                    }
                    TakeDamage(other.GetComponent<EnergyBall>());
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!shield)
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                health -= 2f;
                Destroy(projectile);
            }
        }

        MovableObjects movableObject = other.gameObject.GetComponent<MovableObjects>();
        if (movableObject != null)
        {
            health -= movableObject.ShotDamage + 10;
        }
    }
}
