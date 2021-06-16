using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundCave : MonoBehaviour
{
    public const float TRANSITION_TIME = 0.5f;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceCave;
    public AudioClip rabo;

    private void Start()
    {
        audioSourceCave.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            audioSourceCave.Play(0);
            StartCoroutine(CrossFadeIn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            audioSourceAmbient.Play(0);
            StartCoroutine(CrossFadeOut());

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
            audioSourceAmbient.Stop();
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
            audioSourceCave.Stop();
        }
    }
}
