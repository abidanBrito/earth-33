using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBoss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            // Debug.Log("Guardando en zona boos");
            GameManager.Instance.SaveAll();
        }
    }
}
