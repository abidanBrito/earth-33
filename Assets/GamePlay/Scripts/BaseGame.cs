using System.Collections.Generic;
using System.Reflection;
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
    public static bool collided = false;
    public void ResetEnergyBalls()
    {
        esferas = null;
        esferas = new List<EnergyBall>();
    }
    private void Update()
    {
        pointMovableObject = GameObject.Find("pointMovableObject");
        hitPosition = GameObject.Find("hitPosition").transform.position;
        playerTargetPosition = GameObject.Find("Player").transform.position;
    }

    //rotator Engine
    // Suavizar movimiento esferas, tornillos
    public void smoothMovement(Transform objectTransform, Vector3 towardsTarget, float speed, float rotationSpeed)
    {
        Quaternion towardsTargetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);
        objectTransform.rotation = Quaternion.Lerp(objectTransform.rotation, towardsTargetRotation, rotationSpeed);
        objectTransform.position += objectTransform.forward * speed * Time.deltaTime * 2f;
        Debug.DrawLine(objectTransform.position, towardsTarget, Color.green); 
    }

    public void ClearConsoleLogs()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    
}
