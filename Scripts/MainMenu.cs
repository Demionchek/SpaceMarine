using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject lightsOff;
    [SerializeField] private GameObject lightsOn;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider voicesSlider;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainButtons;

    private void Awake()
    {
        lightsOn.SetActive(true);
        lightsOff.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        lightsOff.SetActive(true);
        Invoke("LoadGame", 1f);
    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void BackButton()
    {
        optionsPanel.SetActive(false);
        mainButtons.SetActive(true);
    }

    public void SaveVolume()
    {
        GetComponent<Saver>().SavePlayerPrefs(musicSlider.value, effectsSlider.value, voicesSlider.value);
    }

    public void ClearData()
    {
        GetComponent<Saver>().ClearData();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
