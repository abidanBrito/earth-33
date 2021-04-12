using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPartShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Inventory.quantity = Inventory.quantity + 1;
            Debug.Log("Partes recogidas: " + Inventory.quantity);
            Destroy(gameObject);
        }
    }
}