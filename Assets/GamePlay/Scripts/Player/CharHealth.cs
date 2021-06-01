using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] 
    [Range(0, 100)]
    public float health = 100;
    private int hitDamage;
    GameObject respawnPosition;

    void Awake()
    {
        respawnPosition = GameObject.FindGameObjectWithTag(GameConstants.RESPAWN_POSITION_TAG);
    }
    private void Update()
    {
        if (health <= 0) 
        { 
            ResetEnergyBalls();
            SceneManager.LoadScene(SceneManager.GetSceneByName("Demo").buildIndex);
            ClearConsoleLogs();
            Debug.Log("--- GAME OVER! ---");
            Debug.Log("--- NEW GAME! ---");
        }



        // FOR DEMO CHANGE
        if(Input.GetKeyDown(KeyCode.Y)){
            health = 100;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag(GameConstants.PROJECTILE_TAG)) 
        {
            Projectile projectileController = other.gameObject.GetComponent<Projectile>();
            if(projectileController.firstCollision){
                projectileController.firstCollision = false;
                
                // Get projectile hit damage on first collision
                hitDamage = (int) other.gameObject.GetComponent<Projectile>().HitDamage;
                // Update health level
                health -= hitDamage;
            }
        }
        if(other.gameObject.tag == GameConstants.BULLET){
            ProjectileMoveScript projectile = other.gameObject.GetComponent<ProjectileMoveScript>();
            if(!projectile.impacted){
                health -= projectile.damage;
                projectile.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
            }
        }

    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == GameConstants.MELEE_WEAPON){
            WeaponMelee weapon = other.gameObject.GetComponent<WeaponMelee>();
            if(!weapon.impacted){
                health -= weapon.damage;
                weapon.impacted = true; //cuando impacta se pone a true para evitar daño duplicado
            }
        }
    }
}
