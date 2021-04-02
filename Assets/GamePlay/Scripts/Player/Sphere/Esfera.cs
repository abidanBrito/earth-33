using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour
{
    private float _speed = 4f;
    private float _rotationSpeed = 10f;
    private Vector3 _towardTarget;
    private int _mode = -2;
    public int _Mode
    {
        get => _mode;
        set => _mode = value;
    }

    void Start(){}

    void Update()
    {
        if(_mode == 0) calcularRuta(GameConstants.MirillaPosition);
        if(_mode == -1) calcularRuta(transform.parent.position);
    }

    public void calcularRuta(Vector3 p)
    {
        _towardTarget = p - transform.position;
        Quaternion towardsTargetRotation = Quaternion.LookRotation(_towardTarget, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, towardsTargetRotation, _rotationSpeed);
        transform.position += transform.forward * _speed * Time.deltaTime * 2f;
        Debug.DrawLine(transform.position, _towardTarget, Color.green);      

        if(_towardTarget.magnitude < 0.1f)
        {
            if (_mode == 0)
            {
                _mode = -1;
                GameConstants._usingBall = false;
            }
            else if(_mode == -1) _mode = -2;
        } 
    }
}