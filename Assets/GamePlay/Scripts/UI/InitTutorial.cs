using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitTutorial : MonoBehaviour
{
    public static bool tabActivated = false;
    public static bool controlModeActivated = false;
    public static bool releaseActivated = false;
    public bool exit = false;

    [SerializeField] private Text ayuda;
    [SerializeField] private Transform player;

    void Start()
    {

    }

    void Update()
    {
        if (exit == false)
        {
            if (tabActivated == false)
            {
                ChangeText("Pulsa 'Tab' para alternar entre el modo de las esferas.");
            }
            if (Input.GetKeyDown(KeyCode.Tab) && tabActivated == false)
            {
                tabActivated = true;
                ClearText();
                ChangeText("En modo Control, puedes disparar sobre objetos para moverlos.");
            }
            if (tabActivated == true && BaseGame.collectedObject != null && controlModeActivated == false)
            {
                controlModeActivated = true;
                ClearText();
                ChangeText("Pulsa 'E' para soltar el objeto.");
            }
            if (controlModeActivated == true && Input.GetKeyDown(KeyCode.E) && releaseActivated == false)
            {
                releaseActivated = true;
                ClearText();
            }
        }
    }

    private void ClearText()
    {
        ayuda.text = "";
    }

    private void ChangeText(string txt)
    {
        ayuda.text = txt;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GameConstants.PLAYER_TAG)
        {
            ayuda.enabled = false;
            exit = true;
        }
    }
}
