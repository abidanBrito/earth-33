using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField]
    [Range(0, 100)]
    public float health = 100;
    private int hitDamage;
    GameObject respawnPosition;
    private Animator playerAnimator;
    private TakeDamagePlayer playerSound;

    //reseting scene
    private bool resetingScene = false;
    private CharController characterController;

    void Awake()
    {
        respawnPosition = GameObject.FindGameObjectWithTag(GameConstants.RESPAWN_POSITION_TAG);
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharController>();
        playerSound = GetComponent<TakeDamagePlayer>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            health = 0;
            playerAnimator.SetBool("DEAD", true);
            if (!resetingScene)
                Invoke("ResetScene", 3f);//this will happen after 2 seconds
            resetingScene = true;
            characterController.enabled = false;
        }
        // FOR DEMO CHANGE
        if (Input.GetKeyDown(KeyCode.Y))
        {
            health = 100;
        }
    }

    private void ResetScene()
    {
        ResetEnergyBalls();
        SceneManager.LoadScene(SceneManager.GetSceneByName("Demo").buildIndex);
        Debug.Log("--- GAME OVER! ---");
        Debug.Log("--- NEW GAME! ---");
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag(GameConstants.PROJECTILE_TAG))
        {
            Projectile projectileController = other.gameObject.GetComponent<Projectile>();
            if (projectileController.firstCollision)
            {
                projectileController.firstCollision = false;

                // Get projectile hit damage on first collision
                hitDamage = (int)other.gameObject.GetComponent<Projectile>().HitDamage;
                // Update health level
                health -= hitDamage;
                checkHP();
            }
        }
        if (other.gameObject.tag == GameConstants.BULLET)
        {
            ProjectileMoveScript projectile = other.gameObject.GetComponent<ProjectileMoveScript>();
            if (!projectile.impacted)
            {
                health -= projectile.damage;
                projectile.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
                checkHP();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstants.MELEE_WEAPON)
        {
            WeaponMelee weapon = other.gameObject.GetComponent<WeaponMelee>();
            if (!weapon.impacted)
            {
                health -= weapon.damage;
                weapon.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
                checkHP();
            }
        }
    }

    private void checkHP()
    {
        if(health <= 0){
            playerSound.PlayDieSound();
        } 
    }
}
