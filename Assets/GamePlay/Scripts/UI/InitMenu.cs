using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenu : BaseGame
{
    public void NewGame(){
        SceneManager.LoadScene("Demo", LoadSceneMode.Single);
    }

    public void Continue(){
        
    }

    public void Controls(){
        
    }

    public void Exit(){
        Application.Quit();
    }
}
