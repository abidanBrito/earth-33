using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilitySoundEffect : MonoBehaviour
{
    private AudioSource audioUltimate;

    void Awake()
    {
        audioUltimate = GetComponent<AudioSource>();
    }
    private void Start() {
        PlayUltimateSound();
    }

    public void PlayUltimateSound(){
        audioUltimate.Play(0);
    }

    
}
