using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSound : MonoBehaviour
{
    public AudioClip ultimate;
    private AudioSource audioUltimate;

    void Awake()
    {
        audioUltimate = GetComponent<AudioSource>();
    }

    public void PlayUltimateSound(){
        audioUltimate.clip = ultimate;
        audioUltimate.Play(0);
        //StartCoroutine(WaitSeconds(secs));
    }

    IEnumerator WaitSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        audioUltimate.PlayOneShot(ultimate);
    }
}
