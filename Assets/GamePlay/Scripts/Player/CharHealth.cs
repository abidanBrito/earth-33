using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] private int health = 100;
    private int hitDamage;
    private bool isFirstCollision = true;

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag(GameConstants.PROJECTILE_TAG)) 
        {
            // Get projectile hit damage on first collision
            if (isFirstCollision == true) 
            {
                hitDamage = (int) other.gameObject.GetComponent<Projectile>().HitDamage;
                isFirstCollision = false;
            }
            
            // Update health level
            health -= hitDamage;
            Debug.Log("Health level: " + health);
            
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
