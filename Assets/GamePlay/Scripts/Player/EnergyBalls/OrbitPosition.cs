using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPosition : BaseGame
{
    public GameObject _objectToCreate;

    // Start is called before the first frame update
    void Start()
    {
        EnergyBall esfera = Instantiate(_objectToCreate, transform).GetComponent<EnergyBall>();
        esferas.Add(esfera);
    }

    // Update is called once per frame
    void Update(){}
}