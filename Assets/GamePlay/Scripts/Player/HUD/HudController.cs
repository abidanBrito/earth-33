using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : BaseGame
{

    Text text;

    CharHealth health;
    Text playerHealth;
    Text sphereMode;
    Text itemSpaceship;


    List<GameObject> sphereController = new List<GameObject>();
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

        //counter sphere remaining
        foreach(EnergyBall sphere in esferas){
            if(sphere.movements == -2){
                if(!sphereController.Contains(sphere.gameObject))
                {
                    sphereController.Add(sphere.gameObject);
                }
            }else{
               if(sphereController.Contains(sphere.gameObject))
               {
                   sphereController.Remove(sphere.gameObject);
               }
            }
        }

        if(BaseGame.sphereModes == 0){
            sphereMode.text = "Modo: Ataque "+sphereController.Count;
        }else if(BaseGame.sphereModes == 1){
            sphereMode.text = "Modo: Controlar Objetos ";
        }else if(BaseGame.sphereModes == 2){
            sphereMode.text = "Modo: Controlar Enemigos ";
        }

        itemSpaceship.text = "ItemNave: " + Inventory.quantity.ToString();


    }
}
