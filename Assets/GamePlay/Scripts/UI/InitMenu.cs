using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenu : BaseGame
{   
    // Elementos de la interfaz
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controles;

    // For Load and Save Status
    private DataSaveLoad saveLoad;
    private InventoryStatus inventoryStatus;
    private const string KEYNAME_NUTS_AND_BOLTS = "inventory_status";

    private void Start()
    {
        // For Load and Save Status
        inventoryStatus = new InventoryStatus();
        saveLoad = new DataSaveLoad();
        Cursor.visible = true;
    }

    public void NewGame()
    {
        saveLoad.Clear(KEYNAME_NUTS_AND_BOLTS);
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void Continue()
    {
        // For Load and Save Status
        NutsAndBoltsLoadStatus();
        Debug.Log("Continuando juego");
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void Controls()
    {
        menu.SetActive(false);
        controles.SetActive(true);
    }

    public void Exit()
    {
        NutsAndBoltsSaveStatus();
        Application.Quit();
    }

    // For Load NutsAndBoltsLoadStatus
    private void NutsAndBoltsLoadStatus()
    {
        saveLoad.Load(KEYNAME_NUTS_AND_BOLTS, ref inventoryStatus);
        Inventory.nutsQuantity = inventoryStatus.nutsQuantity;
        Debug.Log(KEYNAME_NUTS_AND_BOLTS + " cargado");
    }

    // For Save NutsAndBoltsLoadStatus
    private void NutsAndBoltsSaveStatus()
    {
        inventoryStatus.nutsQuantity = Inventory.nutsQuantity;
        saveLoad.Save(KEYNAME_NUTS_AND_BOLTS, inventoryStatus);
        Debug.Log(KEYNAME_NUTS_AND_BOLTS + " guardado");
    }
}
