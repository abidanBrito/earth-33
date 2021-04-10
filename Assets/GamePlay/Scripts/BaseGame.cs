using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame : MonoBehaviour
{
    public static Vector3 playerTargetPosition;
    public static Vector3 puntuation;

    // Sphere Modes
    public static Vector3 hitPosition;
    public static List<EnergyBall> esferas = new List<EnergyBall>();
    public static bool usingBall = false;
    public static int sphereModes = 0;
    public static EnergyBall sphereControlling;

    // Pet Control
    public static GameObject pet;
    public static bool hasPet;
    
    // Move Objects
    public static GameObject collectedObject = null;
    public static GameObject pointMovableObject;
    public static EnergyBall sphereObjectControl;

    // Suavizar movimiento esferas, tornillos
    public void smoothMovement(Transform objectTransform, Vector3 towardsTarget, float speed, float rotationSpeed)
    {
        Quaternion towardsTargetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);
        objectTransform.rotation = Quaternion.Lerp(objectTransform.rotation, towardsTargetRotation, rotationSpeed);
        objectTransform.position += objectTransform.forward * speed * Time.deltaTime * 2f;
        Debug.DrawLine(objectTransform.position, towardsTarget, Color.green); 
    }
}
