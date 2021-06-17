using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BoltSound : MonoBehaviour
{
    public AudioClip FX1;
    public AudioClip FX2;
    public AudioClip FX3;
    private GameObject boltSource;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == GameConstants.TORNILLO_TAG)
        {
            boltSource = new GameObject("Bolt sound");

            AudioSource altavoz = boltSource.AddComponent<AudioSource>();

            AudioMixer audioMixer = Resources.Load<AudioMixer>("NewAudioMixer");
            AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Master/SFX/Efectos de sonido/Player/Tornillos");
            altavoz.outputAudioMixerGroup = audioMixGroup[0];

            int randomSound = Random.Range(0, 2);
            AudioClip[] Fx = { FX1, FX2, FX3 };

            altavoz.PlayOneShot(Fx[randomSound]);

            IEnumerator coroutine = LeafDestruction(boltSource);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator LeafDestruction(GameObject toDestroy)
    {
        yield return new WaitForSeconds(1);
        Destroy(toDestroy);
    }
}
