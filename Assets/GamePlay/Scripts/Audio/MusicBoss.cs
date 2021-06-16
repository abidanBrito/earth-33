using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoss : MonoBehaviour
{
    public const float TRANSITION_TIME = 0.5f;
    public AudioSource audioSourceCave;
    public AudioSource audioSourceBossMusic;
    private bool entered = false;
    [SerializeField] Boss boss;
    private void Awake()
    {
        audioSourceBossMusic.volume = 0;
    }
    private void Update() {
        if(boss.health <= 0){
            audioSourceCave.UnPause();
            CrossFadeOut();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            if(!entered){
                audioSourceBossMusic.Play(0);
                StartCoroutine(CrossFadeIn());
                entered = true;
            }else{
                audioSourceCave.Play(0);
                StartCoroutine(CrossFadeOut());
                entered = false;
            }
        }
    }
 
    IEnumerator CrossFadeIn()
    {
        while (audioSourceCave.volume > 0)
        {
            audioSourceCave.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceBossMusic.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceCave.volume <= 0){
            audioSourceCave.Pause();
        }
    }
    IEnumerator CrossFadeOut()
    {
        while (audioSourceBossMusic.volume > 0)
        {
            audioSourceBossMusic.volume -= Time.deltaTime * TRANSITION_TIME; 
            audioSourceCave.volume += Time.deltaTime * TRANSITION_TIME; 
            yield return null;
        }
        if(audioSourceBossMusic.volume <= 0){
            audioSourceBossMusic.Pause();
        }
    }
}
