using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTutorial : MonoBehaviour
{
    // Tutorial status
    public static bool shopActivated = false;
    public static bool buyActivated = false;
    public static bool abilityActivated = false;
    public bool onTriggerShop = false;

    // Shop GameObject
    private GameObject UI_Shop;
    private AbilitiesShop AbilityShop;
    
    // SerializeFields
    [SerializeField] private Text ayuda;
    [SerializeField] private Transform player;

    void Start(){
        UI_Shop = GameObject.Find("UI_Shop");
        AbilityShop = player.GetComponent<AbilitiesShop>();
        ClearText();
    }

    void Update()
    {
        if(shopActivated == false && onTriggerShop == true){
            shopActivated = true;
            ClearText();
            ChangeText("Pulsa 'B' para abrir la tienda.");
        }
        if(shopActivated == true && onTriggerShop == true && UI_Shop.activeSelf && buyActivated == false){
            buyActivated = true;
            ClearText();
            ChangeText("Aquí puedes comprar habilidades a cambio de tornillos.");
        }
        if(onTriggerShop == true && abilityActivated == false){
            if(AbilityShop.BoughtExplodeEnemy == true || AbilityShop.BoughtThrowObjects == true || AbilityShop.BoughtGranadeAttack == true){
                abilityActivated = true;
                ClearText();
                ChangeText("Pulsa 'F' en el modo correspondiente para utilizar su habilidad especial.");
            }
        }
        if(onTriggerShop == true && abilityActivated == true && Input.GetKeyDown(KeyCode.F)){
            ClearText();
        }
    }

    private void ClearText(){
        ayuda.text = "";
    }

    private void ChangeText(string txt){
        ayuda.text = txt;
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == GameConstants.PLAYER_TAG){
            ayuda.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == GameConstants.PLAYER_TAG){
            ClearText();
            ayuda.enabled = true;
            onTriggerShop = true;
        }
    }
}
