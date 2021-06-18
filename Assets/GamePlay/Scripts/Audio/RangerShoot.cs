using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerShoot : MonoBehaviour
{
    private AudioSource disparo;
    public AudioClip sonidoDisparo;

    void Awake(){
        disparo = GetComponent<AudioSource>();
    }

    public void PlaySound(){
        disparo.volume = 1;
        disparo.clip = sonidoDisparo;
        disparo.Play(0);
    }
}
