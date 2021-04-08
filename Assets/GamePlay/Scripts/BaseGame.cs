using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame : MonoBehaviour
{
    public static Vector3 PlayerTargetPosition;
    public static Vector3 Puntuation;

    // Sphere Modes
    public static Vector3 MirillaPosition;
    public static List<Esfera> Esferas = new List<Esfera>();
    public static bool _usingBall = false;
    public static int _sphereModes = 0;
    public static Esfera _sphereControlling;

    // Pet Control
    public static GameObject Pet;
    public static bool _HasPet;
    
    // Move Objects
    public static GameObject _collectedObject = null;
    public static GameObject pointMovableObject;
    public static Esfera _sphereObjectControl;

    // Suavizar movimiento esferas, tornillos
    public void suavizarMovimiento(Transform objectTransform, Vector3 _towardsTarget, float _speed, float _rotationSpeed)
    {
        Quaternion towardsTargetRotation = Quaternion.LookRotation(_towardsTarget, Vector3.up);
        objectTransform.rotation = Quaternion.Lerp(objectTransform.rotation, towardsTargetRotation, _rotationSpeed);
        objectTransform.position += objectTransform.forward * _speed * Time.deltaTime * 2f;
        Debug.DrawLine(objectTransform.position, _towardsTarget, Color.green); 
    }

}
