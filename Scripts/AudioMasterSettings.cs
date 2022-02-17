using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

public class AudioMasterSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume) 
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetVoicesVolume(float volume) 
    {
        audioMixer.SetFloat("Voices", volume);
    }

    public void SetSoundEffectVolume(float volume) 
    {
        audioMixer.SetFloat("Effects", volume); 
    }

}
