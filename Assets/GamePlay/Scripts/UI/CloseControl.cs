using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseControl : MonoBehaviour
{
    // Elementos de la interfaz
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject controles;

    public void Volver(){
        menu.SetActive(true);
        controles.SetActive(false);
    }
}
