using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXQuitarCristal : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip quitarCristal;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.75f;
    }

    public void PlaySound(){
        audioSource.clip = quitarCristal;
        audioSource.Play(0);
    }
}
