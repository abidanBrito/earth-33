using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEngine : BaseGame
{
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 3f, 0f) * Time.deltaTime * 10);
    }
}