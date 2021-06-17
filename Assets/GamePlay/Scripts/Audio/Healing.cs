using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private AudioSource disparoHeal;
    public AudioClip sonidoHeal;

    void Awake(){
        disparoHeal = GetComponent<AudioSource>();
    }

    public void PlaySoundHealer(){
        disparoHeal.clip = sonidoHeal;
        disparoHeal.Play(0);
    }
}
