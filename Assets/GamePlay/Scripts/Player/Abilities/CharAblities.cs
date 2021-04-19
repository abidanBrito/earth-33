using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAblities : BaseGame
{

    private ExploteEnemy explodeEnemyController;

    // Update is called once per frame
    void Update()
    {
        if(pet)
        {
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
