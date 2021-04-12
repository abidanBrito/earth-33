using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{

    Text text;

    CharHealth health;
    Text playerHealth;
    Text sphereMode;
    Text itemSpaceship;

    void Start()
    {
        text = GameObject.Find("ContadorTornillos").GetComponent<Text>();
        playerHealth = GameObject.Find("playerHealthHUD").GetComponent<Text>();
        sphereMode = GameObject.Find("sphereModeHUD").GetComponent<Text>();
        itemSpaceship = GameObject.Find("itemSpaceshipHUD").GetComponent<Text>();

    }

    void Update()
    {
        text.text = "Tornillos: " + GameManager.Instance.Nuts.ToString();

        health = GameObject.Find("Player").GetComponent<CharHealth>();
        playerHealth.text = "HP: "+ health.health;


        if(BaseGame.sphereModes == 0){
            sphereMode.text = "Modo: Ataque";
        }else if(BaseGame.sphereModes == 1){
            sphereMode.text = "Modo: Controlar Objetos";
        }else if(BaseGame.sphereModes == 2){
            sphereMode.text = "Modo: Controlar Enemigos";
        }

        itemSpaceship.text = "ItemNave: " + Inventory.quantity.ToString();


    }
}
