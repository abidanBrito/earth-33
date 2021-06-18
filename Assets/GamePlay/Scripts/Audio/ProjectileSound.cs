using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(!collided)
            if(other.tag == GameConstants.PLAYER_TAG){
                collided = true;
                audioSource.volume = 1f;
                audioSource.Play(0);
            }
            else{
                collided = true;
                audioSource.volume = 0.3f;
                audioSource.Play(0);
            }
    }
}
