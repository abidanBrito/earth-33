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
    private DataSaveLoad saveLoad;
    private InventoryStatus inventoryStatus;
    private DateTime start;
    private DateTime end;
    private int minutesToStartSaved = 5;
    private int minutes;
    public bool savedLocked = false;
    //
    private const string KEYNAME_NUTS_AND_BOLTS = "inventory_status";

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
        inventoryStatus = new InventoryStatus();
        saveLoad = new DataSaveLoad();
        start = DateTime.Now;
    }

    private void Update()
    {
        if (ItsTimeToSave())
        {
            SaveNutsAndBoltsStatus();
        }
    }

    // For AutoSave
    public bool ItsTimeToSave()
    {
        end = DateTime.Now;
        minutes = end.Minute - start.Minute;
        if (!savedLocked && (minutes >= minutesToStartSaved))
        {
            Debug.Log("Tiempo de guardar");
            Debug.Log("Pasados " + minutes + " minutos");
            Debug.Log("Desde " + start.Hour + ":" + start.Minute + " Hasta " + end.Hour + ":" + end.Minute);
            start = DateTime.Now;

            return true;
        }

        return false;
    }

    // For Save NutsAndBoltsStatus
    public void SaveNutsAndBoltsStatus()
    {
        inventoryStatus.nutsQuantity = Inventory.nutsQuantity;
        saveLoad.Save(KEYNAME_NUTS_AND_BOLTS, inventoryStatus);
        Debug.Log(KEYNAME_NUTS_AND_BOLTS + " guardado");
    }

}
