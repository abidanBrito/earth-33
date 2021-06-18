using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnergyBall : MonoBehaviour
{
    private EnergyBall energyBall;
    private AudioSource audioSource;
    private void Awake() {
        energyBall = GetComponent<EnergyBall>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG || other.GetComponent<BossShiled>() || other.gameObject.tag == "Grass" || other.gameObject.tag == "Sand" || other.GetComponent<AI_Enemy>())
        {
            if(energyBall.movements != 1)
            {
                if(energyBall.movements != 2)
                {
                    if(energyBall.movements != -2)
                    {
                        audioSource.Play(0);
                    }
                }
            }
        }
        
    }
}
