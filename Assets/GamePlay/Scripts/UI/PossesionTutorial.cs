using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossesionTutorial : MonoBehaviour
{
    public static bool possesionActivated = false;
    public static bool enemyControledActivated = false;
    public static bool attackActivated = false;
    public bool onTrigger = false;
    
    [SerializeField] private Text ayuda;
    [SerializeField] private Transform player;

    void Start(){
        ClearText();
    }

    void Update()
    {
        if(possesionActivated == false && onTrigger == true){
            possesionActivated = true;
            ClearText();
            ChangeText("Utiliza el modo Posesión para capturar seres y aprovechar sus habilidades.");
        }
        if(possesionActivated == true && onTrigger == true && enemyControledActivated == false && BaseGame.pet != null){
            enemyControledActivated = true;
            ClearText();
            ChangeText("Utiliza el modo Ataque para derrotar al enemigo.");
        }
        if(enemyControledActivated == true && onTrigger && attackActivated == false && Input.GetMouseButtonDown(0) && BaseGame.sphereModes == 0){
            attackActivated = true;
            ClearText();
        }
    }

    private void ClearText(){
        ayuda.text = "";
    }

    private void ChangeText(string txt){
        ayuda.text = txt;
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == GameConstants.PLAYER_TAG){
            ayuda.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == GameConstants.PLAYER_TAG){
            ayuda.enabled = true;
            onTrigger = true;
        }
    }
}
