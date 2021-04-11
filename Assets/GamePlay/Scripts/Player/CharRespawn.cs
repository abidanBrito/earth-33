using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRespawn : BaseGame
{
    GameObject respawnPosition, respawnTrigger;
    BoxCollider respawnTriggerCollider;

    void Awake()
    {
        respawnPosition = GameObject.FindGameObjectWithTag(GameConstants.RESPAWN_POSITION_TAG);
        respawnTrigger = GameObject.FindGameObjectWithTag(GameConstants.RESPAWN_TRIGGER_TAG);
        respawnTriggerCollider = respawnTrigger.GetComponentInParent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for the player as the collider
        if (other.CompareTag(GameConstants.PLAYER_TAG) 
        && this.transform.position.y <= other.transform.position.y) 
        {
            other.transform.position = respawnPosition.transform.position;
        } 
    }
}