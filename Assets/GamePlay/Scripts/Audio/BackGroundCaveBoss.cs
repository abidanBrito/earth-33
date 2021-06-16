using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCaveBoss : MonoBehaviour
{
    public const float TRANSITION_TIME = 0.5f;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceCave;
    private bool entered = false;
    
    private void Awake()
    {
        audioSourceCave.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            if(!entered){
                audioSourceCave.Play(0);
                StartCoroutine(CrossFadeIn());
                entered = true;
            }else{
                audioSourceAmbient.Play(0);
                StartCoroutine(CrossFadeOut());
                entered = false;
            }
            
        }
    }
 
    IEnumerator CrossFadeIn()
    {
        while (audioSourceAmbient.volume > 0)
        {
            audioSourceAmbient.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceCave.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceAmbient.volume <= 0){
            audioSourceAmbient.Pause();
        }
    }
    IEnumerator CrossFadeOut()
    {
        while (audioSourceCave.volume > 0)
        {
            audioSourceCave.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceAmbient.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceCave.volume <= 0){
            audioSourceCave.Pause();
        }
    }
}
