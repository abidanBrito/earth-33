using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBossUI : BaseGame
{
    [SerializeField]
    private GameObject bossEntity;
    private float bossHealth;
    
    [SerializeField]
    private PilarsController pilarsController;
    private HudController hudController;

    private void Start()
    {
        hudController = GameObject.Find("HudController").GetComponent<HudController>();
        bossHealth = bossEntity.GetComponent<Boss>().health;
    }
    private void CounterCrystalsHud()
    {
        if(pilarsController.crystals.Count == 3)
        {
            // if(!hudController.crystal3.enabled)
            // {
            //     hudController.crystal3.enabled = true;
            //     hudController.crystal2.enabled = true;
            //     hudController.crystal1.enabled = true;
            // }
        }
        else if(pilarsController.crystals.Count == 2)
        {
            // if(hudController.crystal3.enabled)
            // {
            //     hudController.crystal3.enabled = false;
            // }
        }
        else if(pilarsController.crystals.Count == 1)
        {
            // if(hudController.crystal2.enabled)
            // {
            //     hudController.crystal2.enabled = false;
            // }
        }else{
            // if(hudController.crystal1.enabled)
            // {
            //     hudController.crystal1.enabled = false;
            // }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // if(!hudController.healthBarBoss.enabled)
        // {
        //     hudController.healthBarBoss.enabled = true;
        // }
        CounterCrystalsHud();
    }
    private void OnTriggerExit(Collider other)
    {
        // if(hudController.healthBarBoss.enabled)
        // {
        //     hudController.healthBarBoss.enabled = false;
        // }
    }
        
}
