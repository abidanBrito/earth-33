using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOutlineScript : BaseGame
{
    [SerializeField] private Outline outlineManager;
    public enum typeOfGameObject { Mob, MovableObject }
    public typeOfGameObject gameObjectType;
    private Transform player;
    private int sphereModesToCheck = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if(gameObjectType == typeOfGameObject.Mob){ //tipo de objeto asignado al script
            sphereModesToCheck = 2; // si esta en modo posesion
        }else{
            sphereModesToCheck = 1; //si esta en modo control
        }
        outlineManager.enabled = false;
        player = GameObject.FindGameObjectWithTag(GameConstants.PLAYER_TAG).transform;
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == player.tag){
            if(sphereModes == sphereModesToCheck){
                if(!outlineManager.enabled){
                    outlineManager.enabled = true;
                }
            }else{
                if(outlineManager.enabled){
                    outlineManager.enabled = false;
                }
            }
        } 
    }
    private void OnTriggerExit(Collider other) {
       if(other.tag == player.tag){
            if(outlineManager.enabled){
                    outlineManager.enabled = false;
            }
        } 
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == player.tag){
            if(!outlineManager.enabled){
                if(sphereModes == sphereModesToCheck) // si es modo control
                    outlineManager.enabled = true;
            }
        }
    }
}
