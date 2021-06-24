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
        mixer.SetFloat("Musica", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("MusicaVol", Mathf.Log10(SliderValue) * 20);

    }

    public void SetLevelEfectos(float SliderValue)
    {
        mixer.SetFloat("EfectosVolTornillos", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolSpheres", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolProjectile", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolShot", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolEnemies", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolHealer", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolOtros", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolSonidos", Mathf.Log10(SliderValue) * 20);
        mixer.SetFloat("EfectosVolAbilidades", Mathf.Log10(SliderValue) * 20);
    }
}