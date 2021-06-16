using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    //marca los puntos que lleva el jugador
    //recoger tornillos, guardar partidas pasar de nivel, etc.
    private int nuts = 0;
    public int Nuts { get => nuts; set => nuts = value; }
    private int crystals = 0;
    public int Crystals { get => crystals; set => crystals = value; }

    // For AutoSave
    private DateTime start;
    private DateTime end;
    private int minutesToStartSaved = 2;
    private int minutes;
    public  bool savedLocked = false;

    public static GameManager Instance { get => instance; set => instance = value; }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //se destruye para que no haya mas de un objeto con este script
            DestroyImmediate(gameObject);
        }

        // For AutoSave
        start = System.DateTime.Now;
    }

    private void Update()
    {
        // For AutoSave
        end = DateTime.Now;
        minutes = end.Minute - start.Minute;
    }

    // For AutoSave
    public bool ItsTimeToSave()
    {
        if (!savedLocked && (minutes >= minutesToStartSaved))
        {
            Debug.Log("Pasados " + minutes + " minutos");
            start = System.DateTime.Now;

            return true;
        }

        return false;
    }

}
