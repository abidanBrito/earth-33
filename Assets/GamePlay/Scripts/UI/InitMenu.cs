using UnityEngine;

public class InitMenu : BaseGame
{
    // Elementos de la interfaz
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controles;
    private DataSaveLoad saveLoad;

    private void Start()
    {
        Cursor.visible = true;
        saveLoad = new DataSaveLoad();
    }

    public void NewGame()
    {
        saveLoad.Clear(GameConstants.KEYNAME_NUTS_AND_BOLTS);
        saveLoad.Clear(GameConstants.KEYNAME_ABILITIES);
        saveLoad.Clear(GameConstants.KEYNAME_PLAYER);
        GameManager.Instance.LoadScene("Demo", GameConstants.ACTION_NEW_GAME);
    }

    public void Continue()
    {
        GameManager.Instance.LoadScene("Demo", GameConstants.ACTION_CONTINUE);
    }

    public void Controls()
    {
        menu.SetActive(false);
        controles.SetActive(true);
    }

    public void Exit()
    {
        // ForSave NutsAndBoltsSaveStatus
        GameManager.Instance.NutsAndBoltsSaveStatus();
        // For Save NutsAndBoltsSaveStatus
        GameManager.Instance.AbilitiesShopSaveStatus();
        // For Save PlayerSaveStatus
        GameManager.Instance.PlayerSaveStatus();
        Application.Quit();
    }


}
