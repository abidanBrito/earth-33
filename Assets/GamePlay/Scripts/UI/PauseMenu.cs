using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject UI_Menu;
    [SerializeField] private GameObject UI_Shop;
    public static bool menuOpened = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            OpenCloseMenu();
        
    }

    public void Continue(){
        OpenCloseMenu();
        Cursor.visible = false;

    }
    public void Controls(){
        // Nuevo Hud Controls
    }
    public void ReturnIntroMenu(){
        ResumeGame();
        //faltaria guardar partida
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
        Cursor.visible = true;
    }
    public void ExitGame(){
        
        Application.Quit();
    }


    private void OpenCloseMenu(){
        if(!UI_Menu.activeSelf){
            
            PauseGame();
        }else{
            ResumeGame();
        }
    }

    private void PauseGame(){
        Cursor.visible = true;
        UI_Shop.SetActive(false);
        UI_Menu.SetActive(true);
        menuOpened = true;
    }
    private void ResumeGame(){
        Cursor.visible = false;
        UI_Shop.SetActive(false);
        UI_Menu.SetActive(false);
        menuOpened = false;

    }
}
