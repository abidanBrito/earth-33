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
    public const string PLAYER_TAG = "Player";
    public const string MOVABLE_OBJECTS_TAG = "MovableObject";
    public static GameObject _collectedObject = null;
    public static GameObject pointMovableObject;
    public static Esfera _sphereObjectControl;
}
