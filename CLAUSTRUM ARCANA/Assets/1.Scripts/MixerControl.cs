using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerControl : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider fxVol;
    public Slider musicVol;


    void Start()
    {
        
    }

    public void MusicVolChange()
    {
        mixer.SetFloat("MusicVol", musicVol.value);
    }

    public void FXVolChange()
    {
        mixer.SetFloat("FXVol", fxVol.value);
    }
}
