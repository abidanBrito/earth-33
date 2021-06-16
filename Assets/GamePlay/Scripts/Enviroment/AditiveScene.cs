using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AditiveScene : MonoBehaviour
{
    [SerializeField] private Transform player;
    private static bool loaded = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == player.tag){
            if(!loaded)
                SceneManager.LoadScene("BossScene", LoadSceneMode.Additive);
                loaded = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.tag == player.tag){
            if(loaded)
                SceneManager.UnloadSceneAsync("BossScene");
                loaded = false;
        }
    }
        

}
