using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource footSound;
    private bool stepDetector = false;

    public AudioClip[] footprintGround = new AudioClip[3];
    public AudioClip[] footprintGrass = new AudioClip[3];
    public AudioClip[] footprintSand = new AudioClip[3];
    public AudioClip[] footprintMarble = new AudioClip[3];
    
    void Awake()
    {
        footSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other){

        if(other.gameObject.tag == GameConstants.ENVIROMENT_TAG && stepDetector == false){
            footSound.clip = footprintGround[0];
            footSound.volume = 0.3f;
            footSound.Play(0);
            stepDetector = true;
        }
        if(other.gameObject.tag == "Sand" && stepDetector == false){
            int n = Random.Range(0,3);
            footSound.clip = footprintSand[0];
            footSound.Play(0);
            stepDetector = true;
        }
        if(other.gameObject.tag == GameConstants.MOVABLE_OBJECTS_TAG && stepDetector == false){
            int n = Random.Range(0,3);
            footSound.clip = footprintMarble[0];
            footSound.Play(0);
            stepDetector = true;
        }
        if(other.gameObject.tag == "Grass" && stepDetector == false){
            int n = Random.Range(0,3);
            footSound.clip = footprintGrass[0];
            footSound.Play(0);
            stepDetector = true;
        }

    }
    private void OnTriggerStay(Collider other){
        if(stepDetector)
            stepDetector = false;
    }
    private void OnTriggerExit(Collider other){
        if(stepDetector)
            stepDetector = false;
    }
}
