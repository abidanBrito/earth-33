using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopButtons : BaseGame
{
    // Skills Shop
    AbilitiesShop abilitiesShopController;

    private void Start()
    {
        abilitiesShopController = GameObject.Find("Player").GetComponent<AbilitiesShop>();
    }
    public void BuyGranadeAttack(){
        abilitiesShopController.BuyGranadeAttack();
    }
    public void BuyTrhowObjects(){
        abilitiesShopController.BuyThrowObjects();
    }
    public void BuyExplodeEnemy(){
        abilitiesShopController.BuyExplodeEnemy();
    }
}
