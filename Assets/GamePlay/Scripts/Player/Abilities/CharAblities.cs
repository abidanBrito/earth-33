using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAblities : BaseGame
{

    // Ability Exploting Pet Enemy
    private ExploteEnemy explodeEnemyController;
    [SerializeField]
    private float awaitTimeExplodeEnemy;
    private float cooldownExplodeEnemy = 20;
    public bool canUseExplosionEnemy = true;
    // Ability Granades
    [SerializeField]
    private float awaitTimeGranadeAttack;
    private float cooldownGranadeAttack = 10;
    public bool canUseGranadeAttack = true;

    // Ability Rocks
    [SerializeField]
    private float awaitTimeThrowObjects;
    private float cooldownTrhowObjects = 10;
    public bool canUseTrhowObjects = true;

    // Shop Controller
    private AbilitiesShop shopController;

    private void Start()
    {
        shopController = GetComponent<AbilitiesShop>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(shopController.BoughtGranadeAttack)
            {
                CheckAbilityGranadeAttack();
            }
            if(shopController.BoughtThrowObjects)
            {
                CheckAbilityTrhowObjects();
            }
            if(shopController.BoughtExplodeEnemy) 
            {
                CheckAbilityEnemyControl();
            }
        }
        // si se puede usar
        CheckTimersAbilities();
        
    }
    private void CheckAbilityGranadeAttack()
    {
        if(sphereModes == 0 && canUseGranadeAttack)
        {
            canUseGranadeAttack = false;
            cooldownGranadeAttack = awaitTimeGranadeAttack;

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
    private void CheckAbilityTrhowObjects()
    {
        if(sphereModes == 1 && canUseTrhowObjects)
        {
            if (collectedObject.tag == GameConstants.MOVABLE_OBJECTS_TAG)
            {
                MovableObjects movableObject = collectedObject.GetComponent<MovableObjects>();
                movableObject.shootControlledObject();
                canUseTrhowObjects = false;
                cooldownTrhowObjects = awaitTimeThrowObjects;
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
                    if( GameObject.Find("Player").GetComponent<CharHealth>().health >= 100)  GameObject.Find("Player").GetComponent<CharHealth>().health = 100;
                } else {
                    GameObject.Find("Player").GetComponent<CharHealth>().health = 100;
                }
                ExploteEnemy();
                // ExploteEnemy(); // no deberia gastar la misma funcion
                // Pet petController = pet.GetComponent<Pet>();
                // GameObject healerPet = pet;
                // petController.StopControlingEnemy(); // lo dejo de controlar para que vuelva la esfera
                // Destroy(healerPet); //destruyo el pet anterior guardado, no puedo poner el pet por que no existiria despues de controlarlo
                canUseExplosionEnemy = false;
                cooldownExplodeEnemy = awaitTimeExplodeEnemy;
                
            } else if(pet.gameObject.tag == GameConstants.ENEMY_TAG)
            {
                ExploteEnemy();
                canUseExplosionEnemy = false;
                cooldownExplodeEnemy = awaitTimeExplodeEnemy;
            }
        }
    }
    private void ExploteEnemy()
    {
        // if(pet.tag == GameConstants.ENEMY_TAG){
            if(!pet.GetComponent<ExploteEnemy>())
            {
                explodeEnemyController = pet.AddComponent<ExploteEnemy>();
            }
            if(Input.GetKeyDown(explodeEnemyController.key))
            {
                explodeEnemyController.ExplodeEnemy();
            }
        // }
    }
    private void CheckTimersAbilities()
    {
        if(!canUseGranadeAttack)
        {
            TimerCooldownGranateAttack();
        }
        if(!canUseTrhowObjects)
        {
            TimerCooldownTrhowObjects();
        }
        if(!canUseExplosionEnemy)
        {
            TimerCooldownEnemyExplosion();
        }
    }
    private void TimerCooldownTrhowObjects()
    {   
       if(cooldownTrhowObjects >= 0)
        {
            cooldownTrhowObjects = cooldownTrhowObjects-Time.deltaTime*1;
        }
        else
        {
            canUseTrhowObjects = true;
        }
    }
    private void TimerCooldownGranateAttack()
    {   
       if(cooldownGranadeAttack >= 0)
        {
            cooldownGranadeAttack = cooldownGranadeAttack-Time.deltaTime*1;
        }
        else
        {
            canUseGranadeAttack = true;
        }
    }
    
    // Part Explosion Enemy
    private void TimerCooldownEnemyExplosion()
    {   
        if(cooldownExplodeEnemy >= 0)
        {
            cooldownExplodeEnemy = cooldownExplodeEnemy-Time.deltaTime*1;
        }
        else
        {
            canUseExplosionEnemy = true;
        }
    }

}
