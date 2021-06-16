using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AditiveScene : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == player.tag){
            SceneManager.LoadScene("BossScene", LoadSceneMode.Additive);
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.tag == player.tag){
            SceneManager.UnloadSceneAsync("BossScene");
        }
    }
        

}
