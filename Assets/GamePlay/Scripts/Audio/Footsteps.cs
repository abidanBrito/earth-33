using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource footSound;
    private bool stepDetector = true;

    public GameObject foot;
    public AudioClip[] footprintGround = new AudioClip[3];
    public AudioClip[] footprintGrass = new AudioClip[3];
    public AudioClip[] footprintSand = new AudioClip[3];
    public AudioClip[] footprintMarble = new AudioClip[3];
    
    void Awake()
    {
        footSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.tag);

        //if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG && stepDetector == false){
        if(other.gameObject.tag != "Player"){
            footSound.clip = footprintGround[0];
            footSound.Play(0);
            stepDetector = true;
        }
    }

    private void OnTriggerExit(Collider other){
        stepDetector = false;
    }
}
