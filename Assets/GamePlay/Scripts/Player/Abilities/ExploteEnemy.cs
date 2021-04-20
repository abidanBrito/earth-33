using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExploteEnemy : BaseGame
{
    // Cooldown
    public KeyCode key = KeyCode.F;

    //  Explosion Status
    private float damage = 7f;
    private float radius = 7.0F;
    private float power = 600.0f;
  

    public void ExplodeEnemy()
    {
        if(pet)
        {
            GameObject explosionPrefab = gameObject.GetComponent<AI_Enemy>().explosionEffect;

            // if pet es explotable (hay que añadir)
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            Pet petController = pet.GetComponent<Pet>();
            petController.StopControlingEnemy(); // deja de controlar el enemigo antes de destruirlo
            
            gameObject.GetComponent<AI_Enemy>().CreateDrop();   // lo mata
            
            GameObject explosionFX = Instantiate(explosionPrefab, transform.position, transform.rotation); // crea el efecto de explotar
            Destroy(explosionFX, 1.5f);

            foreach (Collider hit in colliders)
            {
                // para todos los enemigos afectados se crean
                if(hit.tag == GameConstants.ENEMY_TAG)
                {
                    AI_Enemy enemy = hit.GetComponent<AI_Enemy>();
                    NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
                    
                    enemy.health -= damage; //recibe daño de la explosion
                    
                    if(enemy != pet){
                        if(!enemy.GetComponent<Rigidbody>())
                        {
                            //Desactiva algunos componentes para que funcione la explosion
                            enemy.enabled = false;
                            agent.enabled = false; 
                            enemy.GetComponent<Pet>().enabled = false;
                            
                            Rigidbody rb = enemy.gameObject.AddComponent<Rigidbody>();

                            if (rb != null)
                            {
                                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

                                DetectGroundEnemy detectGroundController = enemy.gameObject.AddComponent<DetectGroundEnemy>(); // le añade un script para cuando el enemigo salga por los aires vuelva a su posicion.
                                detectGroundController.Enemy = enemy;
                                detectGroundController.Agent = agent;
                            }
                        }
                    }
                }

                if(hit.gameObject.GetComponent<Boss>())
                {
                    Boss boss = hit.gameObject.GetComponent<Boss>();
                    boss.health -= damage; // hay que cambiarlo para el escudo
                }
            }
        }
    }
}
