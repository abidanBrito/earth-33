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
    int petHealth;

    private GameObject UI_ModeAttack;
    private GameObject UI_ModeControl;
    private GameObject UI_ModePosesion;

    List<GameObject> sphereController = new List<GameObject>();
    void Start()
    {
        // text = GameObject.Find("ContadorTornillos").GetComponent<Text>();
        // playerHealth = GameObject.Find("playerHealthHUD").GetComponent<Text>();
        // sphereMode = GameObject.Find("sphereModeHUD").GetComponent<Text>();
        // itemSpaceship = GameObject.Find("itemSpaceshipHUD").GetComponent<Text>();

        UI_ModeAttack = GameObject.Find("UI_ModeAttack");
        UI_ModeControl = GameObject.Find("UI_ModeControl");
        UI_ModePosesion = GameObject.Find("UI_ModePosesion");

    }

    void Update()
    {
        // text.text = "Tornillos: " + GameManager.Instance.Nuts.ToString();

        // health = GameObject.Find("Player").GetComponent<CharHealth>();
        // playerHealth.text = "HP: "+ health.health;

        // //counter sphere remaining
        // foreach(EnergyBall sphere in esferas){
        //     if(sphere.movements == -2){
        //         if(!sphereController.Contains(sphere.gameObject))
        //         {
        //             sphereController.Add(sphere.gameObject);
        //         }
        //     }else{
        //        if(sphereController.Contains(sphere.gameObject))
        //        {
        //            sphereController.Remove(sphere.gameObject);
        //        }
        //     }
        // }
       
        // if(pet){
        //   petHealth= (int) pet.GetComponent<AI_Enemy>().health;
        // }

        // if(BaseGame.sphereModes == 0){
        //     sphereMode.text = "Modo: Ataque "+sphereController.Count;
        // }else if(BaseGame.sphereModes == 1){
        //     sphereMode.text = "Modo: Controlar Objetos ";
        // }else if(BaseGame.sphereModes == 2){
        //     sphereMode.text = "Modo: Controlar Enemigos "+petHealth;
        // }

        // itemSpaceship.text = "ItemNave: " + Inventory.quantity.ToString();

        if(sphereModes == 0)
        {
            UI_ModeAttack.SetActive(true);
            if(UI_ModeControl)
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(false);

        }
        else if(sphereModes == 1)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(true);
            UI_ModePosesion.SetActive(false);
        }
        else if(sphereModes == 2)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(true);
        }


    }
}
