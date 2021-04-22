using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAblities : BaseGame
{

    // Ability Exploting Pet Enemy
    private ExploteEnemy explodeEnemyController;
    public float cooldownExplodeEnemy = 0;
    private bool canUseExplosionEnemy = true;

    // Ability Exploting Pet Enemy Healer
    public float cooldownGranadeAttack= 0;
    private bool canUseGranadeAttack = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CheckAbilityEnemyControl();
            CheckAbilityGranadeAttack();
        }
        // si se puede usar
        CheckTimersAbilities();
    }
    private void CheckAbilityGranadeAttack()
    {
        if(sphereModes == 0 && canUseGranadeAttack)
        {
            canUseGranadeAttack = false;
            cooldownGranadeAttack = 10;

            List<EnergyBall> sphereHability = new List<EnergyBall>();
            
            foreach(EnergyBall sphere in esferas)
            {
                if(sphere.movements == -2)
                {
                    if(sphereHability.Count < 2) sphereHability.Add(sphere);
                }
            }

            if(sphereHability.Count == 2)
            {
                sphereHability[0].gameObject.AddComponent<GranadeAttack>();
                sphereHability[1].gameObject.AddComponent<GranadeAttack>();
            }

        }
    }
    private void CheckAbilityEnemyControl()
    {
        if(sphereModes == 2 && canUseExplosionEnemy && pet){
            // Reseting cooldown
            if(pet.gameObject.tag == GameConstants.HEALER_TAG)
            {
                if((GameObject.Find("Player").GetComponent<CharHealth>().health += pet.GetComponent<Healer>().getMobHealerHealth()) <= 100 )
                {
                    GameObject.Find("Player").GetComponent<CharHealth>().health += pet.GetComponent<Healer>().getMobHealerHealth();  
                } else {
                    GameObject.Find("Player").GetComponent<CharHealth>().health = 100;
                }
                // ExploteEnemy(); // no deberia gastar la misma funcion
                // canUseExplosionEnemy = false;
                // cooldownExplodeEnemy = 20;
            } else
            {
                ExploteEnemy();
                canUseExplosionEnemy = false;
                cooldownExplodeEnemy = 20;
            }
        }
    }
    private void CheckTimersAbilities()
    {
        if(!canUseExplosionEnemy)
        {
            TimerCooldownEnemyExplosion();
        }
        if(!canUseGranadeAttack)
        {
            TimerCooldownGranateAttack();
        }
    }
    private void TimerCooldownGranateAttack()
    {   
        if(cooldownGranadeAttack >= 0)
        {
            cooldownGranadeAttack = cooldownGranadeAttack-Time.deltaTime*1;
        }else{
            canUseGranadeAttack = true;
        }
    }
    
    // Part Explosion Enemy
    private void TimerCooldownEnemyExplosion()
    {   
        if(cooldownExplodeEnemy >= 0)
        {
            cooldownExplodeEnemy = cooldownExplodeEnemy-Time.deltaTime*1;
        }else{
            canUseExplosionEnemy = true;
        }
    }
    private void ExploteEnemy()
    {
        if(pet.tag == GameConstants.ENEMY_TAG){
            if(!pet.GetComponent<ExploteEnemy>())
            {
                explodeEnemyController = pet.AddComponent<ExploteEnemy>();
            }
            if(Input.GetKeyDown(explodeEnemyController.key))
            {
                explodeEnemyController.ExplodeEnemy();
            }
        }
    }

}
