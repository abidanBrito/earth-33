using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : BaseGame
{
    //marca los puntos que lleva el jugador
    //recoger tornillos, guardar partidas pasar de nivel, etc.
    private int nuts = 0;
    public int Nuts { get => nuts; set => nuts = value; }
    private int crystals = 0;
    public int Crystals { get => crystals; set => crystals = value; }

    // For AutoSave
    private DataSaveLoad saveLoad;
    private InventoryStatus inventoryStatus;
    private AbilitiesStatus abilitiesStatus;
    private PlayerStatus playerStatus;
    private DateTime start;
    private DateTime end;
    private int minutesToStartSaved = 2; // Minutos que pasan entre guardados autom�ticos
    private int minutes;
    private GameObject player;
    private AbilitiesShop abilitiesShop;
    public bool savedLocked = false;
    public static List<EnergyBall> ListaEsferas = new List<EnergyBall>();
    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Start()
    {
        // For AutoSave
        saveLoad = new DataSaveLoad();
        inventoryStatus = new InventoryStatus();
        abilitiesStatus = new AbilitiesStatus();
        player = GameObject.FindWithTag(GameConstants.PLAYER_TAG);
        abilitiesShop = player.GetComponent<AbilitiesShop>();
        playerStatus = new PlayerStatus();
        start = DateTime.Now;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //se destruye para que no haya mas de un objeto con este script
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(esferas.Count);
        if (ItsTimeToSave())
        {
            NutsAndBoltsSaveStatus();
            AbilitiesShopSaveStatus();
            PlayerSaveStatus();
        }
    }

    // For AutoSave
    public bool ItsTimeToSave()
    {
        end = DateTime.Now;
        minutes = end.Minute - start.Minute;
        if (!savedLocked && (minutes >= minutesToStartSaved))
        {
            // Debug.Log("Tiempo de guardar");
            // Debug.Log("Pasados " + minutes + " minutos");
            // Debug.Log("Desde " + start.Hour + ":" + start.Minute + " Hasta " + end.Hour + ":" + end.Minute);
            start = DateTime.Now;

            return true;
        }

        return false;
    }

    public void LoadScene(string name, string action)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        StartCoroutine(SceneLoadAction(action));
    }

    IEnumerator SceneLoadAction(string action)
    {

        yield return new WaitForSeconds(0.0f);

        switch (action)
        {
            case GameConstants.ACTION_NEW_GAME:
                // Debug.Log(GameConstants.ACTION_NEW_GAME + " ...");
                // Debug.Log("Cargando...");
                ResetEnergyBalls();
                esferas.Add(GameObject.Find("Posicion 1").GetComponentInChildren<EnergyBall>());
                esferas.Add(GameObject.Find("Posicion 2").GetComponentInChildren<EnergyBall>());
                esferas.Add(GameObject.Find("Posicion 3").GetComponentInChildren<EnergyBall>());
                // Debug.Log("Cargado...");
                break;
            case GameConstants.ACTION_CONTINUE:
                // Debug.Log(GameConstants.ACTION_CONTINUE + " ...");
                ResetEnergyBalls();
                esferas.Add(GameObject.Find("Posicion 1").GetComponentInChildren<EnergyBall>());
                esferas.Add(GameObject.Find("Posicion 2").GetComponentInChildren<EnergyBall>());
                esferas.Add(GameObject.Find("Posicion 3").GetComponentInChildren<EnergyBall>());
                // Debug.Log("Cargando...");
                LoadAll();
                // Debug.Log("Cargado...");
                break;
            default:
                // Debug.Log("Default ...");
                yield return null;
                break;
        }
    }

    public void SaveAll()
    {
        NutsAndBoltsSaveStatus();
        AbilitiesShopSaveStatus();
        PlayerSaveStatus();
    }

    public void LoadAll()
    {
        NutsAndBoltsLoadStatus();
        AbilitiesShopLoadStatus();
        PlayerLoadStatus();
    }

    // For Load NutsAndBoltsLoadStatus
    public void NutsAndBoltsLoadStatus()
    {
        saveLoad.Load(GameConstants.KEYNAME_NUTS_AND_BOLTS, ref inventoryStatus);
        Inventory.nutsQuantity = inventoryStatus.nutsQuantity;
        Nuts = inventoryStatus.nutsQuantity;
        // Debug.Log(GameConstants.KEYNAME_NUTS_AND_BOLTS + " cargado");
    }

    // For Save NutsAndBoltsSaveStatus
    public void NutsAndBoltsSaveStatus()
    {
        inventoryStatus.nutsQuantity = Inventory.nutsQuantity;
        saveLoad.Save(GameConstants.KEYNAME_NUTS_AND_BOLTS, inventoryStatus);

        // Debug.Log(GameConstants.KEYNAME_NUTS_AND_BOLTS + " guardado");
    }

    // For Load AbilitiesShopLoadStatus
    public void AbilitiesShopLoadStatus()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag(GameConstants.PLAYER_TAG);
            abilitiesShop = player.GetComponent<AbilitiesShop>();
        }
        saveLoad.Load(GameConstants.KEYNAME_ABILITIES, ref abilitiesStatus);
        
        // Debug.Log(abilitiesStatus);
        abilitiesStatus.boughtGranadeAttack = abilitiesShop.BoughtGranadeAttack;
        abilitiesStatus.boughtThrowObjects = abilitiesShop.BoughtThrowObjects;
        abilitiesStatus.boughtExplodeEnemy = abilitiesShop.BoughtExplodeEnemy;
        // Debug.Log(GameConstants.KEYNAME_ABILITIES + " cargado");
    }

    // For Save AbilitiesShopSaveStatus
    public void AbilitiesShopSaveStatus()
    {
        abilitiesStatus.boughtGranadeAttack = abilitiesShop.BoughtGranadeAttack;
        abilitiesStatus.boughtThrowObjects = abilitiesShop.BoughtThrowObjects;
        abilitiesStatus.boughtExplodeEnemy = abilitiesShop.BoughtExplodeEnemy;
        saveLoad.Save(GameConstants.KEYNAME_ABILITIES, abilitiesStatus);
        // Debug.Log(GameConstants.KEYNAME_ABILITIES + " guardado");
    }

    // For Load PlayerLoadStatus
    public void PlayerLoadStatus()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag(GameConstants.PLAYER_TAG);
            abilitiesShop = player.GetComponent<AbilitiesShop>();
        }
        saveLoad.Load(GameConstants.KEYNAME_PLAYER, ref playerStatus);
        Vector3 newPosition = new Vector3(
            (float)playerStatus.positionPlayerX,
            (float)playerStatus.positionPlayerY,
            (float)playerStatus.positionPlayerZ);
        player.transform.position = newPosition;
        Debug.Log(GameConstants.KEYNAME_PLAYER + " cargado");
    }

    // For Save PlayerSaveStatus
    public void PlayerSaveStatus()
    {
        if(playerStatus != null){
            playerStatus.positionPlayerX = player.transform.position.x;
            playerStatus.positionPlayerY = player.transform.position.y;
            playerStatus.positionPlayerZ = player.transform.position.z;
            saveLoad.Save(GameConstants.KEYNAME_PLAYER, playerStatus);
            // Debug.Log(GameConstants.KEYNAME_PLAYER + " guardado");
		}
    }

}
