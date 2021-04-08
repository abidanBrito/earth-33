using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour
{
    private float _speed = 4f;
    private float _rotationSpeed = 10f;
    private Vector3 _towardTarget;
    private int _movements = -2;
    public int _Movements
    {
        get => _movements;
        set => _movements = value;
    }
    private int _mode = 0;
    public int _Mode
    {
        get => _mode;
        set => _mode = value;
    }

    void Start(){
        //not doing anything
        _Mode = -1;
    }

    void Update()
    {
        //si la esfera en concreto no esta siendo utilizada para controlar, no hara el movimiento de volver
        if(_movements != 1 || _movements != 2){
            if(_movements == 0) calcularRuta(BaseGame.MirillaPosition);
            if(_movements == -1) calcularRuta(transform.parent.position);
        }
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
            if (_movements == 0)
            {
                _movements = -1;        // volviendo
                BaseGame._usingBall = false;   // mientras vuelve puede usar otras esferas
            }
            else if(_movements == -1)
            {
                _movements = -2;      // Estado estatico
                _Mode = -1;           
            } 
        } 
    }
}