using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class BaseGame : MonoBehaviour
{
    public static Vector3 playerTargetPosition;
    public static Vector3 puntuation;

    // Sphere Modes
    public static Vector3 hitPosition;
    public static Vector3 hitGranade;
    public static List<EnergyBall> esferas = new List<EnergyBall>();
    public static bool usingBall = false;
    public static int sphereModes = 0;
    public static EnergyBall sphereControlling;

    // Pet Control
    public static GameObject pet;
    public static bool hasPet;
    
    // Move Objects
    public static GameObject collectedObject = null;
    public static GameObject pointMovableObject;
    public static EnergyBall sphereObjectControl;
    public static bool collided = false;
    public void ResetEnergyBalls()
    {
        esferas = null;
        esferas = new List<EnergyBall>();
        
    }
    private void Update()
    {
        pointMovableObject = GameObject.Find("pointMovableObject");
        hitPosition = GameObject.Find("hitPosition").transform.position;
        hitGranade = GameObject.Find("hitGranade").transform.position;
        playerTargetPosition = GameObject.Find("Player").transform.position;
    }

    //rotator Engine
    // Suavizar movimiento esferas, tornillos
    public void smoothMovement(Transform objectTransform, Vector3 towardsTarget, float speed, float rotationSpeed)
    {
        Quaternion towardsTargetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);
        objectTransform.rotation = Quaternion.Lerp(objectTransform.rotation, towardsTargetRotation, rotationSpeed);
        objectTransform.position += objectTransform.forward * speed * Time.deltaTime * 2f;
        Debug.DrawLine(objectTransform.position, towardsTarget, Color.green); 
    }

    public void ClearConsoleLogs()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    public void ExplosionVFX(GameObject explosionPrefab, float timeToDestroy = 1.5f)
    {   GameObject explosionFX = null;
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles);
       
        explosionFX = Instantiate(explosionPrefab, transform.position, rotation); // crea el efecto de explotar
        
        Destroy(explosionFX, timeToDestroy);
    }
    public void ExplosionAttack(Transform transform, float radius, float damage, float power)
    {
        // if pet es explotable (hay que añadir)
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    
        foreach (Collider hit in colliders)
        {
            // para todos los enemigos afectados se crean
            if(hit.tag == GameConstants.ENEMY_TAG && !hit.gameObject.GetComponent<Boss>() || hit.tag == GameConstants.HEALER_TAG)
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

            if(hit.tag == GameConstants.MOVABLE_OBJECTS_TAG)
            {
                MovableObjects movableObject = hit.GetComponent<MovableObjects>();
                Rigidbody rb =  movableObject.GetComponent<Rigidbody>();
                power = 200f;
                if(rb)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                }
            }

            if(hit.gameObject.GetComponent<Boss>())
            {
                Boss boss = hit.gameObject.GetComponent<Boss>();
                if(boss.GetShield == null){
                    boss.health -= damage; // hay que cambiarlo para el escudo
                }
            }
        }
    }
    
}
