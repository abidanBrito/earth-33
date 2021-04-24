using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : BaseGame
{


    
    Text text;

    int playerHealth;
    Text sphereMode;
    Text itemSpaceship;
    int petHealth;

    // Modes UI && Boss Health
    private GameObject UI_ModeAttack;
    private GameObject UI_ModeControl;
    private GameObject UI_ModePosesion;

    private GameObject uI_HealthBarBoss;
    public GameObject UI_HealthBarBoss{
        get => uI_HealthBarBoss;
    }

    // UI Sphere Counter
    private Text UI_Attack_Ammo_Number;
    private Text UI_Control_Ammo_Number;
    private Text UI_Posesion_Ammo_Number;

    // UI Health Player %
    private Text UI_Attack_Percentage;
    private Text UI_Control_Percentage;
    private Text UI_Posesion_Percentage;

    // UI Counter Bolts

    private Text UI_Attack_Bolt_Counter;
    private Text UI_Control_Bolt_Counter;
    private Text UI_Posesion_Bolt_Counter;
    private int nutsCounter;



    List<GameObject> sphereController = new List<GameObject>();
    void Start()
    {
        // text = GameObject.Find("ContadorTornillos").GetComponent<Text>();
        // playerHealth = GameObject.Find("playerHealthHUD").GetComponent<Text>();
        // sphereMode = GameObject.Find("sphereModeHUD").GetComponent<Text>();
        // itemSpaceship = GameObject.Find("itemSpaceshipHUD").GetComponent<Text>();

        //UI Modes
        UI_ModeAttack = GameObject.Find("UI_ModeAttack");
        UI_ModeControl = GameObject.Find("UI_ModeControl");
        UI_ModePosesion = GameObject.Find("UI_ModePosesion");
        uI_HealthBarBoss = GameObject.Find("TopSide");
        // UI Sphere Counter
        UI_HealthBarBoss.SetActive(false);
    }

    void Update()
    {
        nutsCounter =  GameManager.Instance.Nuts;
        // text.text = "Tornillos: " + GameManager.Instance.Nuts.ToString();

        // health = GameObject.Find("Player").GetComponent<CharHealth>();
        // playerHealth.text = "HP: "+ health.health;

        
       
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
        ChangeUIModes();
        CheckingSphereQuantityUI();
        UpdatingPlayerHealth();

        if(UI_Attack_Bolt_Counter != null)
        UI_Attack_Bolt_Counter.text = nutsCounter.ToString();
        
        if(UI_Control_Bolt_Counter != null)
        UI_Control_Bolt_Counter.text = nutsCounter.ToString();

        if(UI_Posesion_Bolt_Counter != null)
        UI_Posesion_Bolt_Counter.text = nutsCounter.ToString();
        
    }
    private void UpdatingPlayerHealth()
    {
        playerHealth = GameObject.Find("Player").GetComponent<CharHealth>().health;
        if(UI_Attack_Percentage != null)
        UI_Attack_Percentage.text = playerHealth.ToString() + "%";
        
        if(UI_Control_Percentage != null)
        UI_Control_Percentage.text = playerHealth.ToString() + "%";

        if(UI_Posesion_Percentage != null)
        UI_Posesion_Percentage.text = playerHealth.ToString() + "%";
    }

    private void ChangeUIModes()
    {
        if(sphereModes == 0)
        {
            UI_ModeAttack.SetActive(true);
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(false);
            UI_Attack_Ammo_Number = GameObject.Find("UI_Attack_Ammo_Number").GetComponent<Text>();
            UI_Attack_Percentage = GameObject.Find("UI_Attack_Percentage").GetComponent<Text>();
            UI_Attack_Bolt_Counter = GameObject.Find("UI_Attack_Bolt_Counter").GetComponent<Text>();


        }
        else if(sphereModes == 1)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(true);
            UI_ModePosesion.SetActive(false);
            UI_Control_Ammo_Number =  GameObject.Find("UI_Control_Ammo_Number").GetComponent<Text>();
            UI_Control_Percentage = GameObject.Find("UI_Control_Percentage").GetComponent<Text>();
            UI_Control_Bolt_Counter = GameObject.Find("UI_Control_Bolt_Counter").GetComponent<Text>();
        }
        else if(sphereModes == 2)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(true);
            UI_Posesion_Ammo_Number =  GameObject.Find("UI_Posesion_Ammo_Number").GetComponent<Text>();
            UI_Posesion_Percentage = GameObject.Find("UI_Posesion_Percentage").GetComponent<Text>();
            UI_Posesion_Bolt_Counter = GameObject.Find("UI_Posesion_Bolt_Counter").GetComponent<Text>();
        }
    }
    private void CheckingSphereQuantityUI()
    {
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

        if(UI_Attack_Ammo_Number != null)
        UI_Attack_Ammo_Number.text =  sphereController.Count.ToString() + " /3";

        if(UI_Control_Ammo_Number != null)
        UI_Control_Ammo_Number.text =  sphereController.Count.ToString() + " /3";
        
        if(UI_Posesion_Ammo_Number != null)
        UI_Posesion_Ammo_Number.text =  sphereController.Count.ToString() + " /3";
    }
}
