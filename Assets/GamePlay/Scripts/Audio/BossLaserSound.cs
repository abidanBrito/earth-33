using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserSound : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip laserSound;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLaserSound(){
        audioSource.clip = laserSound;
        audioSource.volume = 0.75f;
        audioSource.Play(0);
    }
    public void StopLaserSound(){
        audioSource.clip = laserSound;
        StartCoroutine(VolumeFadeOut());

    }

    IEnumerator VolumeFadeOut(){
        while(audioSource.volume >= 0){
            audioSource.volume -= 0.3f*Time.deltaTime;
            yield return null;
        } 
        if(audioSource.volume <= 0){
            audioSource.Stop();
            audioSource.volume = 1;
        }
    }
}
