using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSound : MonoBehaviour
{
    public AudioClip ultimate;
    private static AudioSource audioUltimate;

    void Awake()
    {
        audioUltimate = GetComponent<AudioSource>();
    }

    public void PlayUltimateSound(){
        audioUltimate.PlayOneShot(ultimate);
        //StartCoroutine(WaitSeconds(secs));
    }

    IEnumerator WaitSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        audioUltimate.PlayOneShot(ultimate);
    }
}
