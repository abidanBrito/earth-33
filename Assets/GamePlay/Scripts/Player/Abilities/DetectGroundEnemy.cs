using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectGroundEnemy : BaseGame
{
    // Start is called before the first frame update
    private AI_Enemy enemy;
    private NavMeshAgent agent;
    private DetectGroundEnemy detectGround;

    public AI_Enemy Enemy{
        get => this.enemy;
        set{
            enemy = value;
        }
    }
    public NavMeshAgent Agent{
        get => this.agent;
        set{
            agent = value;
        }
    }

    private void Start()
    {
        detectGround = GetComponent<DetectGroundEnemy>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 6){
            // si el transform choca con la capa SUELO despues de recibir la explosion de la habilidad
            if(!agent.enabled){
                CheckIfGrounded();
            }
        }
    }
    
    private void CheckIfGrounded()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll (positionToCheck, new Vector2 (0, -1), 0.01f);
        //if a collider was hit, we are grounded
        if (hits.Length >= 0) {
            if(enemy.health <= 0)
            {
                enemy.CreateDrop();  
            }
            StartCoroutine(Timer(3));
        }
    }

    IEnumerator Timer(int seconds){
        yield return new WaitForSeconds(seconds);
        agent.enabled = true;
        enemy.enabled = true;
        gameObject.GetComponent<Pet>().enabled = true;
        Destroy(gameObject.GetComponent<Rigidbody>());
        if(detectGround){
            Destroy(detectGround);
        }
    }
}
