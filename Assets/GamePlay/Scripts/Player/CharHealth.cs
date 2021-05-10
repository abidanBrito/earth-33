using UnityEngine;
using UnityEngine.SceneManagement;

public class CharHealth : BaseGame
{
    [SerializeField] 
    [Range(0, 100)]
    public float health =  100f;
    private int hitDamage;
    GameObject respawnPosition;

    private bool inmortal = false;

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

        if(Input.GetKeyDown(KeyCode.Y))
        {
            if(inmortal == false) inmortal = true;
            else inmortal = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (inmortal ==false && other.CompareTag(GameConstants.PROJECTILE_TAG)) 
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
        // EN la clase LASER SCRIPT TAMBIEN QUITA VIDA POR RAYCAST
    }
}
