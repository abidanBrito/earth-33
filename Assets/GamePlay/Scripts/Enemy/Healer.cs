using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BaseGame
{
    public float mobHealth = 50f;
    public float healingValue = 10;
    public float timeBetweenHeals = 2f;
    private bool alreadyHealed = false;
    private Pet petController;
    private int playerHealth;
    AI_Enemy aiController;
    ParticleSystem particlesController;
    
    void Start()
    {
        petController = GetComponent<Pet>();
        aiController = GetComponent<AI_Enemy>();
        particlesController = GetComponent<ParticleSystem>();
        particlesController.Pause();
        aiController.health = mobHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == GameConstants.HEALER_TAG)
        {
            // si el enemigo es healer
            if(pet != gameObject)
            {
                aiController.Patroling();
            }else{
                playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;
                petController.FollowPlayer();
                if(playerHealth < 100){
                    if(!alreadyHealed){
                        particlesController.Play();
                        HealPlayer();
                    }
                }else{
                    particlesController.Stop();
                }
            }
        }
    }
    
    private void ResetHeal(){
        alreadyHealed = false;
    }
    private void HealPlayer()
    {
        aiController.health -= healingValue;
        playerHealth += (int)healingValue;
        if(playerHealth >= 100) playerHealth = 100;
        GameObject.Find("Player").GetComponent<CharHealth>().health = playerHealth;
        Debug.Log(aiController.health+"ia");
        alreadyHealed = true;
        Invoke(nameof(ResetHeal),timeBetweenHeals);
    }

}