using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveStartBackground : MonoBehaviour
{
    public const float TRANSITION_TIME = 0.5f;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceCave;
    private bool exited = false;
    private void Start()
    {
        audioSourceCave.Play(0);
        audioSourceCave.volume = 1;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            if(!exited){
                audioSourceAmbient.Play(0);
                StartCoroutine(CrossFadeIn());
                exited = true;
            }else{
                audioSourceCave.Play(0);
                StartCoroutine(CrossFadeOut());
                exited = false;
            }
            
        }
    }
 
    IEnumerator CrossFadeIn()
    {
        while (audioSourceCave.volume > 0)
        {
            audioSourceCave.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceAmbient.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceCave.volume <= 0){
            audioSourceCave.Stop();
        }
    }
    IEnumerator CrossFadeOut()
    {
        while (audioSourceAmbient.volume > 0)
        {
            audioSourceAmbient.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceCave.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceAmbient.volume <= 0){
            audioSourceAmbient.Stop();
        }
    }
}
