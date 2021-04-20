using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAblities : BaseGame
{

    // Ability Exploting Pet Enemy
    private ExploteEnemy explodeEnemyController;
    public float cooldownExplodeEnemy = 0;
    private bool canUseExplosionEnemy = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            if(sphereModes == 2 && canUseExplosionEnemy){
                // Reseting cooldown
                ExploteEnemy();
                canUseExplosionEnemy = false;
                cooldownExplodeEnemy = 20;
            }
        }
        // si se puede usar
        if(!canUseExplosionEnemy)
        {
            TimerCooldownEnemyExplosion();
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
        if(pet)
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

}
