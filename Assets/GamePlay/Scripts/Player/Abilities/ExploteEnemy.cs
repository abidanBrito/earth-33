using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExploteEnemy : BaseGame
{
    public float cooldownInSeconds;
    public KeyCode key = KeyCode.F;
    //  Explosion
    public float damage = 7f;
    public float radius = 7.0F;
    public float power = 800.0F;

    public void ExplodeEnemy(){
        if(pet)
        {
            // if pet es explotable (hay que añadir)
            Vector3 explosionPos = transform.position;
            explosionPos.y = explosionPos.y-3f; // se crea la esfera un poco mas hacia abajo para que los enemigos salgan volando
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            Pet petController = pet.GetComponent<Pet>();
            petController.StopControlingEnemy(); // deja de controlar el enemigo antes de destruirlo
            
            gameObject.GetComponent<AI_Enemy>().CreateDrop();   // lo mata
            
            foreach (Collider hit in colliders)
            {
                // para todos los enemigos afectados se crean
                if(hit.GetComponent<AI_Enemy>())
                {
                    AI_Enemy enemy = hit.GetComponent<AI_Enemy>();
                    NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
                    
                    enemy.health -= damage; //recibe daño de la explosion
                    
                    if(enemy != pet){
                        if(!enemy.GetComponent<Rigidbody>())
                        {
                            //Desactiva algunos componentes para que funcione la explosion
                            agent.enabled = false; 
                            enemy.enabled = false;
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
            }
        }
    }
    
}
