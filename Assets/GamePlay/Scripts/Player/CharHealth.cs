using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] 
    [Range(0, 100)]
    public int health = 100;
    private int hitDamage;
    GameObject respawnPosition;

    void Awake()
    {
        respawnPosition = GameObject.FindGameObjectWithTag(GameConstants.RESPAWN_POSITION_TAG);
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
            // Reload the scene upon death
            if (health <= 0) 
            { 
                ResetEnergyBalls();
                SceneManager.LoadScene(SceneManager.GetSceneByName("Demo").buildIndex);
                ClearConsoleLogs();
                Debug.Log("--- GAME OVER! ---");
                Debug.Log("--- NEW GAME! ---");
            }
        }
    }
}
