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
        GameObject explosionPrefab = gameObject.GetComponent<AI_Enemy>().explosionEffect;
        ExplosionAttack(transform, radius, damage, power, explosionPrefab);
    }
}
