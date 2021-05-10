using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BaseGame
{
    public float mobHealth = 50f;
    public float healingValue = 10;
    public float timeBetweenHeals = 2f;
    public GameObject explosionEffect;
    private bool alreadyHealed = false;
    private Pet petController;
    private float playerHealth;
    AI_Enemy aiController;
    ParticleSystem particlesController;
    
    void Start()
    {
        petController = GetComponent<Pet>();
        aiController = GetComponent<AI_Enemy>();
        particlesController = GetComponent<ParticleSystem>();
        particlesController.Stop();
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
                if(aiController.GetComponent<AI_Enemy>().enabled){
                    aiController.Patroling();
                }
                if(particlesController.isPlaying)
                particlesController.Stop();
            }else{
                playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;
                petController.FollowPlayer();
                if(playerHealth < 100){
                    if(!alreadyHealed){
                        if(!particlesController.isPlaying)
                        particlesController.Play();
                        HealPlayer();
                    }
                }else{
                    if(particlesController.isPlaying)
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
        alreadyHealed = true;
        Invoke(nameof(ResetHeal),timeBetweenHeals);
    }
    public int getMobHealerHealth()
    {
        return (int)mobHealth;
    }
}
