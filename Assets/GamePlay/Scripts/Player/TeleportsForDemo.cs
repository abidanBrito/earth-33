using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportsForDemo : MonoBehaviour
{

    [SerializeField] private Transform bosszone;
    [SerializeField] private Transform initialZone;
    [SerializeField] private Transform interestPoint1; // cave
    [SerializeField] private Transform interestPoint2; // mountain
    [SerializeField] private Transform interestPoint3; // Desert()

    private Transform player;
    private void Start()
    {
        player = gameObject.transform;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad0)){
            player.position = initialZone.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad1)){
            player.position = bosszone.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)){
            player.position = interestPoint1.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)){
            player.position = interestPoint2.position;
        }
        if(Input.GetKeyDown(KeyCode.Keypad4)){
            player.position = interestPoint3.position;
        }
    }
}
