using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BaseGame
{
    public float healingValue = 10;
    public float timeBetweenHeals = 2f;
    private float auxTimeBetweenHeals;
    private Pet petController;
    private int playerHealth;
    AI_Enemy aiController;
    
    void Start()
    {
        petController = GetComponent<Pet>();
        aiController = GetComponent<AI_Enemy>();
        aiController.health = 50f;
        auxTimeBetweenHeals = timeBetweenHeals;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(auxTimeBetweenHeals);
        if(gameObject.tag == GameConstants.HEALER_TAG)
        {
            // si el enemigo es healer
            if(pet != gameObject)
            {
                aiController.Patroling();
            }else{
                playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;
                petController.FollowPlayer();
                if(playerHealth < 90){
                    // Temporizador si puede curar
                    if(CanHeal()){
                        HealPlayer();
                    }
                }
            }
        }

    }
    private bool CanHeal()
    {
        // si el temporizador esta a 0 podra curar
        if(auxTimeBetweenHeals >= 0){
            auxTimeBetweenHeals = auxTimeBetweenHeals-Time.fixedDeltaTime;
            return false;
        }
        auxTimeBetweenHeals = 0; // cada vez que cura se resetea el cooldown de curar
        return true;
    }
    private void HealPlayer()
    {
        auxTimeBetweenHeals = timeBetweenHeals; // cada vez que cura se resetea el cooldown de curar
        aiController.health -= 10;
        playerHealth += 10;
        GameObject.Find("Player").GetComponent<CharHealth>().health = playerHealth;
        Debug.Log(playerHealth+"player");
        Debug.Log(aiController.health+"ia");
    }
}
