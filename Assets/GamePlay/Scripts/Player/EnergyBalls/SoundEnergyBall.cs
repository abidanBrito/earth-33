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
        if(other.tag != GameConstants.PLAYER_TAG)
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
