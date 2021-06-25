using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevelGeneral(float SliderValue)
    {
        mixer.SetFloat("GeneralVol", Mathf.Log10(SliderValue) * 20);
    }

    public void SetLevelMusica(float SliderValue)
    {
        mixer.SetFloat("MusicaV", Mathf.Log10(SliderValue) * 30);
        mixer.SetFloat("MusicaVol", Mathf.Log10(SliderValue) * 20);

    }

    public void SetLevelEfectos(float SliderValue)
    {
        mixer.SetFloat("EfectosVolSonidos", Mathf.Log10(SliderValue) * 20);
    }
}