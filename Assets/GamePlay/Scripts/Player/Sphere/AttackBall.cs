using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBall : MonoBehaviour
{
    public GameObject _energyBall;
    public Transform _player;
    // Start is called before the first frame update
    void Start()
    {
       _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = _player.rotation;

            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                Rigidbody rb = Instantiate(_energyBall, transform.position,transform.rotation).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward *10f, ForceMode.Impulse);
            }   
    }
    
}
