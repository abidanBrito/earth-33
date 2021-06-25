using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDemoUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI_DemoScreen;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == GameConstants.PLAYER_TAG){
            Cursor.visible = true;
            Time.timeScale = 0;
            UI_DemoScreen.SetActive(true);
        }
    }
}
