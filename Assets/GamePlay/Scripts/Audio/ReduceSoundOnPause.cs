using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ReduceSoundOnPause : MonoBehaviour
{
    [SerializeField] private GameObject UI_Menu;
    [SerializeField] private GameObject UI_Controles;

    public AudioMixer mixer;
    public AudioMixerSnapshot juegoActivo;
    public AudioMixerSnapshot juegoPausado;
    private AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[2];
    private float[] transiciones = new float[2];

    void Awake(){
        snapshots[0] = juegoActivo;
        snapshots[1] = juegoPausado;
    }

    void Update()
    {
        if(UI_Menu.activeSelf || UI_Controles.activeSelf){
            juegoPausado.TransitionTo(.01f);
            /*transiciones[0] = 0.0f;
            transiciones[1] = 1.0f;
            mixer.TransitionToSnapshots(snapshots, transiciones, 1.0f);*/
            mixer.SetFloat("ReducirVolume", Mathf.Log10(0.15f)*20);
            mixer.SetFloat("Lowpass", 800.0f);
        } else {
            juegoActivo.TransitionTo(.01f);
            /*transiciones[0] = 1.0f;
            transiciones[1] = 0.0f;
            mixer.TransitionToSnapshots(snapshots, transiciones, 1.0f);*/
            mixer.SetFloat("ReducirVolume", Mathf.Log10(0.2f)*20);
            mixer.SetFloat("Lowpass", 22000.0f);
        }
    }
}
