using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDemoScreen : MonoBehaviour
{
    public GameObject UI_DemoScene; 
    
    public void CloseMenu(){
            Cursor.visible = false;
            Time.timeScale = 1;
            UI_DemoScene.SetActive(false);
    }
}
