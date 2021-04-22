using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject explosionPrefab = null;
        if(gameObject.tag == GameConstants.ENEMY_TAG){
            explosionPrefab = gameObject.GetComponent<AI_Enemy>().explosionEffect;
            ExplosionAttack(transform, radius, damage, power);
        }else{
            explosionPrefab = gameObject.GetComponent<Healer>().explosionEffect;
        }
        ExplosionVFX(explosionPrefab);

        Pet petController = pet.GetComponent<Pet>();
        petController.StopControlingEnemy(); // deja de controlar el enemigo antes de destruirlo   
        gameObject.GetComponent<AI_Enemy>().CreateDrop();   // lo mata
    }
}
