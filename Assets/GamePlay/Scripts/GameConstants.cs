using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{   
    public static Vector3 PlayerTargetPosition;
    public static Vector3 Puntuation;

    // Sphere Modes
    public static Vector3 MirillaPosition;
    public static List<Esfera> Esferas = new List<Esfera>();
    public static bool _usingBall = false;

    // Pet Control
    public static GameObject Pet;
    public static bool _HasPet;
}