using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMob : MonoBehaviour
{
    public enum mobType { Healer, Ranger, Golem, Boss }
    public mobType typeOfMob;
    public AudioClip bossDeath;
    public AudioClip mobDeath;
    public AudioClip healerDeath;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundOnDeath()
    {
        Debug.Log("probando");
        switch (typeOfMob)
        {
            case mobType.Healer:
                audioSource.PlayOneShot(healerDeath);
                break;

            case mobType.Golem:
                audioSource.PlayOneShot(mobDeath);
                break;

            case mobType.Ranger:
                audioSource.PlayOneShot(mobDeath);
                break;

            case mobType.Boss:
                audioSource.PlayOneShot(bossDeath);
                break;
        }
    }
}
