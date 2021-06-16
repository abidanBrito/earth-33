using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public bool playingSound = false;
    public AudioSource audio;

    void Start(){
        audio.Pause();
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == GameConstants.PLAYER_TAG && playingSound == false){
            PlaySound();
        }
    }

    private void PlaySound(){
        audio.Play(0);
        playingSound = true;
    }
}
