using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    [SerializeField]
    public float _health = 10f;
    public GameObject _objectToCreate;
    private GameObject _newTornillo;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    { 
        if(_health <= 0) CreateDrope();
    }

    //Creo un número aleatorio de Tornillod (1-6)
    //los desvinculo del padre y destruyo el padre (Enemigo)
    public void CreateDrope()
    {
        for(int i = 0; i <= Random.Range(3f, 6f); i++)
        {   
            _newTornillo = Instantiate(_objectToCreate, gameObject.transform);
        }  

        transform.DetachChildren();
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Esfera") _health -= 2.5f;
    }
}