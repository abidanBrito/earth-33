using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesShop : BaseGame
{
    // Granade Attack
    private bool boughtGranadeAttack = false;
    public int priceGranadeAttack = 5;
    public bool BoughtGranadeAttack
    {
        get => boughtGranadeAttack;
        set{
            boughtGranadeAttack = value;
        }
    }
    
    // Throw Rocks
    private bool boughtThrowObjects = false;
    public int priceThrowObjects = 10;
    public bool BoughtThrowObjects
    {
        get => boughtThrowObjects;
        set{
            boughtThrowObjects = value;
        }
    }
    
    // Explote Enemy
    private bool boughtExplodeEnemy = false;
    public int priceExplodeEnemy = 20;
    public bool BoughtExplodeEnemy{
        get => boughtExplodeEnemy;
        set{
            boughtExplodeEnemy = value;
        }
    }

    public bool BuyGranadeAttack()
    {
        if(!BoughtGranadeAttack)
        {
            if(GameManager.Instance.Nuts >= priceGranadeAttack)
            {
                GameManager.Instance.Nuts -= priceGranadeAttack;
                BoughtGranadeAttack = true;
                return true;
            }
        }
        return false;
    }
    public bool BuyExplodeEnemy()
    {
        if(!BoughtExplodeEnemy)
        {
            if(GameManager.Instance.Nuts >= priceExplodeEnemy)
            {
                GameManager.Instance.Nuts -= priceExplodeEnemy;
                BoughtExplodeEnemy = true;
                return true;
            }
        }
        return false;
    }

    public bool BuyThrowObjects()
    {
        if(!boughtThrowObjects)
        {
            if(GameManager.Instance.Nuts >= priceThrowObjects)
            {
                GameManager.Instance.Nuts -= priceThrowObjects;
                boughtThrowObjects = true;
                return true;
            }
        }
        return false;
    }
}
