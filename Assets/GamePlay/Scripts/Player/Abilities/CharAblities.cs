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
        CheckExploteEnemy();
        if(!canUseExplosionEnemy)
        {
            TimerForCooldowns();
        }
    }
    private void TimerForCooldowns()
    {   
        if(cooldownExplodeEnemy >= 0)
        {
            cooldownExplodeEnemy = cooldownExplodeEnemy-Time.deltaTime*1;
        }else{
            canUseExplosionEnemy = true;
        }
    }
    private void CheckExploteEnemy()
    {
        if(pet && Input.GetKeyDown(KeyCode.F) && canUseExplosionEnemy)
        {
            canUseExplosionEnemy = false;
            cooldownExplodeEnemy = 20;

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
