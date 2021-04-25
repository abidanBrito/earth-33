using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesShop : BaseGame
{
    // Granade Attack
    [SerializeField]
    private int priceGranadeAttack = 5;
    private bool boughtGranadeAttack = false;
    public bool BoughtGranadeAttack
    {
        get => boughtGranadeAttack;
        set{
            boughtGranadeAttack = value;
        }
    }
    
    // Throw Rocks
    [SerializeField]
    private int priceThrowObjects = 10;
    private bool boughtThrowObjects = false;
    public bool BoughtThrowObjects
    {
        get => boughtThrowObjects;
        set{
            boughtThrowObjects = value;
        }
    }
    
    // Explote Enemy
    [SerializeField]
    private int priceExplodeEnemy = 20;
    private bool boughtExplodeEnemy = false;
    public bool BoughtExplodeEnemy{
        get => boughtExplodeEnemy;
        set{
            boughtExplodeEnemy = value;
        }
    }
    public void BuyGranadeAttack()
    {
        if(!BoughtGranadeAttack)
        {
            if(GameManager.Instance.Nuts >= priceGranadeAttack)
            {
                GameManager.Instance.Nuts -= priceGranadeAttack;
                BoughtGranadeAttack = true;
            }
        }
    }
    public void BuyExplodeEnemy()
    {
        if(!BoughtExplodeEnemy)
        {
            if(GameManager.Instance.Nuts >= priceExplodeEnemy)
            {
                GameManager.Instance.Nuts -= priceExplodeEnemy;
                BoughtExplodeEnemy = true;
            }
        }
    }

    public void BuyThrowObjects()
    {
        if(!boughtThrowObjects)
        {
            if(GameManager.Instance.Nuts >= priceThrowObjects)
            {
                GameManager.Instance.Nuts -= priceThrowObjects;
                boughtThrowObjects = true;
            }
        }
    }
}
