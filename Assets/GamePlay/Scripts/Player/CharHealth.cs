using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] public int health = 100;
    private int hitDamage;
    
    
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
                Debug.Log("--- GAME OVER! ---");
                ClearConsoleLogs();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("--- NEW GAME! ---");
            }
        }
    }
}
