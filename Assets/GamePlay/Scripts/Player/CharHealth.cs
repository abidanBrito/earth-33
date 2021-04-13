using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] public int health = 100;
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
            Debug.Log("Health level: " + health);
        }
            // Reload the scene upon death
            if (health <= 0) 
            { 
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                ClearConsoleLogs();
                Debug.Log("--- GAME OVER! ---");
                health = 100;
                this.transform.position = respawnPosition.transform.position;
                Debug.Log("--- NEW GAME! ---");
            }
        }
    }
}
