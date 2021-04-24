using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBossUI : BaseGame
{
    [SerializeField]
    private GameObject bossEntity;
    private float bossHealth;
    private GameObject hudBossHealth;
    private float maxHealth = 100f;
    
    [SerializeField]
    private PilarsController pilarsController;
    private HudController hudController;

    //controlador hud crystales
    private GameObject[] crystalHud = new GameObject[3];

    
    private void Start()
    {
        hudController = GameObject.Find("HudController").GetComponent<HudController>();
        hudBossHealth = GameObject.Find("BossHealth");
    }
    private void Update()
    {
        
    }
    private void CounterCrystalsHud()
    {
        if(pilarsController.crystals.Count == 3)
        {
            crystalHud[0].SetActive(true);
            crystalHud[1].SetActive(true);
            crystalHud[2].SetActive(true);
        }
        else if(pilarsController.crystals.Count == 2)
        {
            if(crystalHud[2] != null)
            {
                crystalHud[2].SetActive(false);
            }
        }
        else if(pilarsController.crystals.Count == 1)
        {
            if(crystalHud[1] != null)
            {
                crystalHud[1].SetActive(false);
            }
        }
        else
        {
            if(crystalHud[0] != null)
            {
                crystalHud[0].SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == GameConstants.PLAYER_TAG){
            hudController.UI_HealthBarBoss.SetActive(true);
            crystalHud[0] = GameObject.Find("ShieldLeft");
            crystalHud[1] = GameObject.Find("ShieldCenter");
            crystalHud[2] = GameObject.Find("ShieldRight");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(bossEntity != null)
        {
            if(other.tag == GameConstants.PLAYER_TAG){
                CounterCrystalsHud();
                bossHealth = bossEntity.GetComponent<Boss>().health;
                hudBossHealth.transform.localScale = new Vector3(bossHealth/maxHealth,1f,1f);
            }
        }else
        {
            hudController.UI_HealthBarBoss.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hudController.UI_HealthBarBoss.SetActive(false);
    }
        
}
