using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //marca los puntos que lleva el jugador
    //recoger tornillos, guardar partidas pasar de nivel, etc.
    private int nuts = 0;
    public int Nuts { get => nuts; set => nuts = value; }
    
    static GameManager instance;
    
    public static GameManager Instance { get => instance; set => instance = value; }

    void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            //se destruye para que no haya mas de un objeto con este script
            DestroyImmediate(gameObject);
        }
    }

    
}
