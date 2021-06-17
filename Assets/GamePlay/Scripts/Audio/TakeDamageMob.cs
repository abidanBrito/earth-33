using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageMob : MonoBehaviour
{
    public enum enemyType { Healer, Ranger, Golem, Boss }
    public enemyType type;
    public AudioClip healerDamageSFX;
    public AudioClip rangerDamageSFX;
    public AudioClip golemDamageSFX;
    public AudioClip bossDamageSFX;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BaseGame.pet == null)
        {

            EnergyBall esfera = other.gameObject.GetComponent<EnergyBall>();
            if (other.gameObject.tag == GameConstants.ESFERA_TAG && esfera.modes == 0 && esfera.movements != -1 && esfera.movements != -2)
            {
                switch (type)
                {
                    case enemyType.Healer:
                        audioSource.clip = healerDamageSFX;
                        audioSource.Play(0);
                        break;

                    case enemyType.Golem:
                        audioSource.clip = golemDamageSFX;
                        audioSource.Play(0);
                        break;

                    case enemyType.Ranger:
                        audioSource.clip = rangerDamageSFX;
                        audioSource.Play(0);
                        break;

                    case enemyType.Boss:
                        audioSource.clip = bossDamageSFX;
                        audioSource.Play(0);
                        break;
                }
            }

        }
    }
}
