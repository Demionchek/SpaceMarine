using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private AudioSource _reloadEndSound;
    [SerializeField] private AudioSource shot1;
    [SerializeField] private AudioSource shot2;
    [SerializeField] private AudioSource shot3;
    [SerializeField] private AudioSource launch;

    public void PlayReloadSound()
    {
        _reloadSound.Play();
    }

    public void StopReloadSound()
    {
        _reloadSound.Stop();
        _reloadEndSound.Play();
    }

    public void LaunchSound()
    {
        launch.Play();
    }

    public void ShotSound()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            shot1.Play();
        }
        else if (r == 1)
        {
            shot2.Play();
        }
        else if (r == 2)
        {
            shot3.Play();
        }
    }
}
