using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageMob : MonoBehaviour
{
    public AudioClip sphereDamageSFX;
    public AudioClip projectileDamageSFX;
    public AudioClip rockDamageSFX;
    public AudioClip meleeDamageSFX;
    private AudioSource audioSource;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other) //Comprobado
    {
        if(BaseGame.pet == null){
            //Si se detecta una colision...
            switch (other.gameObject.tag) 
            {
                //Si la colision es un proyectil de un Enemy...
                case GameConstants.PROJECTILE_TAG:
                    audioSource.clip = projectileDamageSFX;
                    audioSource.Play(0);
                    break;
                
                //Si la colision es un objeto movible por el jugador
                case GameConstants.MOVABLE_OBJECTS_TAG:
                    audioSource.clip = rockDamageSFX;
                    audioSource.Play(0); 
                    break;

                case GameConstants.ESFERA_TAG:
                    EnergyBall esfera = other.gameObject.GetComponent<EnergyBall>();
                    if(esfera.modes == 0 && esfera.movements != -1 && esfera.movements != -2){
                        audioSource.clip = sphereDamageSFX;
                        audioSource.Play(0); 
                    }
                    break;
                    
                case GameConstants.MELEE_WEAPON:
                    audioSource.clip = meleeDamageSFX;
                    audioSource.Play(0); 
                    break;
            }
        }
    }
}
