using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePlayer : MonoBehaviour
{
    public AudioClip damageSFX;
    public AudioClip dieSFX;
    private AudioSource playerSource;

    void Awake()
    {
        playerSource = GetComponent<AudioSource>();
    }

    public void PlayDamageSound()
    {
        playerSource.PlayOneShot(damageSFX);
    }

    public void PlayDieSound()
    {
        playerSource.PlayOneShot(dieSFX);
    }
}
