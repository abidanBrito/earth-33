using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : BaseGame
{
    Text text;
    
    [SerializeField] private CharHealth charHealth;
    [SerializeField] private CharAblities charAblities;
    float playerHealth;
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
    [SerializeField] private Text UI_Attack_Ammo_Number;
    [SerializeField] private Text UI_Control_Ammo_Number;
    [SerializeField] private Text UI_Posesion_Ammo_Number;

    // UI Health Player % / HealthBar
    private int maxHealthPlayer = 100;
    [SerializeField] private Text UI_Attack_Percentage;
    [SerializeField] private Text UI_Control_Percentage;
    [SerializeField] private Text UI_Posesion_Percentage;

    private GameObject UI_Attack_HealthBar;
    private GameObject UI_Control_HealthBar;
    private GameObject UI_Posesion_HealthBar;

    // UI Counter Bolts

    [SerializeField] private Text UI_Attack_Bolt_Counter;
    [SerializeField] private Text UI_Control_Bolt_Counter;
    [SerializeField] private Text UI_Posesion_Bolt_Counter;
    private int boltsCounter;

    // Pet Health Object && HealthBar
    private GameObject UI_Attack_Pet_Health;
    private GameObject UI_Control_Pet_Health;
    private GameObject UI_Posesion_Pet_Health;
    
    [SerializeField] private Text UI_Attack_Pet_Life;
    [SerializeField] private Text UI_Control_Pet_Life;
    [SerializeField] private Text UI_Posesion_Pet_Life;

    // Abilitie Cooldown
    
    private GameObject UI_Attack_Ability;
    private GameObject UI_Control_Ability;
    private GameObject UI_Posesion_Ability;
    private CharAblities charAblitiesController;

    // Skills Shop
    private GameObject UI_Shop;
    [SerializeField] private GameObject shop_Attack_Buy_Button;
    [SerializeField] private GameObject shop_Control_Buy_Button;
    [SerializeField] private GameObject shop_Posesion_Buy_Button;
    private Text shop_Attack_Button_Price;
    private Text shop_Control_Button_Price;
    private Text shop_Posesion_Button_Price;
    private AbilitiesShop abilitiesShopController;

    List<GameObject> sphereController = new List<GameObject>();
    void Start()
    {
        //UI Modes
        UI_ModeAttack = GameObject.Find("UI_ModeAttack");
        UI_ModeControl = GameObject.Find("UI_ModeControl");
        UI_ModePosesion = GameObject.Find("UI_ModePosesion");

        UI_Attack_HealthBar = GameObject.Find("UI_Attack_HealthBar");
        UI_Control_HealthBar = GameObject.Find("UI_Control_HealthBar");
        UI_Posesion_HealthBar = GameObject.Find("UI_Posesion_HealthBar");

        //UI pet Health
        UI_Attack_Pet_Health = GameObject.Find("UI_Attack_Pet_Health");
        UI_Control_Pet_Health = GameObject.Find("UI_Control_Pet_Health");
        UI_Posesion_Pet_Health = GameObject.Find("UI_Posesion_Pet_Health");

        UI_Attack_Ability = GameObject.Find("UI_Attack_Ability");
        UI_Control_Ability = GameObject.Find("UI_Control_Ability");
        UI_Posesion_Ability = GameObject.Find("UI_Posesion_Ability");

        uI_HealthBarBoss = GameObject.Find("TopSide");
        UI_HealthBarBoss.SetActive(false);

        //Shop Texts
        UI_Shop = GameObject.Find("UI_Shop");
        abilitiesShopController = GameObject.Find("Player").GetComponent<AbilitiesShop>();
        shop_Attack_Button_Price = GameObject.Find("Shop_Attack_Button_Price").GetComponent<Text>();
        shop_Control_Button_Price = GameObject.Find("Shop_Control_Button_Price").GetComponent<Text>();
        shop_Posesion_Button_Price = GameObject.Find("Shop_Posesion_Button_Price").GetComponent<Text>();

        shop_Attack_Button_Price.text = abilitiesShopController.PriceGranadeAttack.ToString() + "   ";
        shop_Control_Button_Price.text = abilitiesShopController.PriceThrowObjects.ToString() + "   ";
        shop_Posesion_Button_Price.text = abilitiesShopController.PriceExplodeEnemy.ToString() + "   ";
        UI_Shop.SetActive(false);
    }

    void Update()
    {
        ChangeUIModes();
        CheckingSphereQuantityUI();
        UpdatingPlayerHealth();
        UpdatingBoltsCounter();
        UpdatingPetHealth();
        ShopControllerInformationShown();
    }

    private void ShopControllerInformationShown()
    {
        KeysShop(KeyCode.B, KeyCode.Escape);
        
        if(UI_Shop.activeSelf)
        {
            if(abilitiesShopController.BoughtGranadeAttack) shop_Attack_Buy_Button.SetActive(false);
            if(abilitiesShopController.BoughtThrowObjects) shop_Control_Buy_Button.SetActive(false);
            if(abilitiesShopController.BoughtExplodeEnemy) shop_Posesion_Buy_Button.SetActive(false);
            
        }
    }

    private void KeysShop(KeyCode open, KeyCode close)
    {
        if(Input.GetKeyDown(open))
        {
            OpenSkillShop();
        }

        if(UI_Shop.activeSelf)
        {
            if(Input.GetKeyDown(close)) OpenSkillShop();
        }
    }

    private void OpenSkillShop()
    {
        if(!UI_Shop.activeSelf)
        {
            UI_Shop.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            UI_Shop.SetActive(false);
            Cursor.visible = false;
        }
    }

    private void UpdatingPetHealth()
    {
        if(pet)
        {
            petHealth = (int) pet.GetComponent<AI_Enemy>().health;
        }

        if(sphereModes == 0)
        {
            if(pet)
            {
                UI_Attack_Pet_Health.SetActive(true);
                UI_Attack_Pet_Life.text = petHealth + "HP";
            }
            else
            {
                UI_Attack_Pet_Health.SetActive(false);
            }
        }

        if(sphereModes == 1)
        {
            if(pet)
            {
                UI_Control_Pet_Health.SetActive(true);
                UI_Control_Pet_Life.text = petHealth + "HP";
            }
            else
            {
                UI_Control_Pet_Health.SetActive(false);
            }
        }

        if(sphereModes == 2)
        {
            if(pet)
            {
                UI_Posesion_Pet_Health.SetActive(true);
                UI_Posesion_Pet_Life.text = petHealth + "HP";
            }
            else
            {
                UI_Posesion_Pet_Health.SetActive(false);
            }
        }
    }

    private void UpdatingBoltsCounter()
    {
        boltsCounter = GameManager.Instance.Nuts;
        if(UI_Attack_Bolt_Counter != null)
        UI_Attack_Bolt_Counter.text = boltsCounter.ToString();
        
        if(UI_Control_Bolt_Counter != null)
        UI_Control_Bolt_Counter.text = boltsCounter.ToString();

        if(UI_Posesion_Bolt_Counter != null)
        UI_Posesion_Bolt_Counter.text = boltsCounter.ToString();
    }

    private void UpdatingPlayerHealth()
    {
        playerHealth = charHealth.health;
        if(UI_Attack_Percentage != null)
        {
            UI_Attack_Percentage.text = Mathf.Round(playerHealth).ToString() + "%";
            UI_Attack_HealthBar.transform.localScale = new Vector3((float)playerHealth/(float)maxHealthPlayer,1f,1f);
        }
        
        if(UI_Control_Percentage != null)
        {
            UI_Control_Percentage.text = Mathf.Round(playerHealth).ToString() + "%";

            UI_Control_HealthBar.transform.localScale = new Vector3((float)playerHealth/(float)maxHealthPlayer,1f,1f);

        }

        if(UI_Posesion_Percentage != null)
        {
            UI_Posesion_Percentage.text = Mathf.Round(playerHealth).ToString() + "%";
            UI_Posesion_HealthBar.transform.localScale = new Vector3((float)playerHealth/(float)maxHealthPlayer,1f,1f);
        }
    }

    private void ChangeUIModes()
    {
        charAblitiesController = charAblities;

        if(sphereModes == 0)
        {
            UI_ModeAttack.SetActive(true);
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(false);
            
            if(abilitiesShopController.BoughtGranadeAttack)
            {
                if(charAblitiesController.canUseGranadeAttack)
                {
                    UI_Attack_Ability.SetActive(true);
                }
                else
                {
                    UI_Attack_Ability.SetActive(false);
                }
            }
            else
            {
                if(UI_Attack_Ability != null)
                UI_Attack_Ability.SetActive(false);
            }
            
        }
        else if(sphereModes == 1)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(true);
            UI_ModePosesion.SetActive(false);

            if(abilitiesShopController.BoughtThrowObjects)
            {
                if(charAblitiesController.canUseTrhowObjects)
                {
                    UI_Control_Ability.SetActive(true);
                }
                else
                {
                    UI_Control_Ability.SetActive(false);
                }
            }
            else
            {
                if(UI_Control_Ability != null)
                UI_Control_Ability.SetActive(false);
            }

        }
        else if(sphereModes == 2)
        {
            UI_ModeAttack.SetActive(false);
            UI_ModeControl.SetActive(false);
            UI_ModePosesion.SetActive(true);
        
            if(abilitiesShopController.BoughtExplodeEnemy)
            {
                if(charAblitiesController.canUseExplosionEnemy)
                {
                    UI_Posesion_Ability.SetActive(true);
                }
                else
                {
                    UI_Posesion_Ability.SetActive(false);
                }
            }
            else
            {
                if(UI_Posesion_Ability != null)
                UI_Posesion_Ability.SetActive(false);
            }
        }
    }

    private void CheckingSphereQuantityUI()
    {
        //counter sphere remaining
        foreach(EnergyBall sphere in esferas)
        {
            if(sphere.movements == -2){
                if(!sphereController.Contains(sphere.gameObject))
                {
                    sphereController.Add(sphere.gameObject);
                }
            }
            else
            {
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
