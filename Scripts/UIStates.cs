using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStates : MonoBehaviour
{
    [Header("Required Object")]
    [SerializeField] private ScoreSetScrptblObj _playerData;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Health _playerHealth;
    [Header("Start/End/Pause UI")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _controllsPanel;
    [SerializeField] private GameObject _tryAgainButton;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _darkTolightPanel;
    [SerializeField] private GameObject _lightToDarkPanel;
    [Header("Game UI")]
    [SerializeField] private GameObject _firstBlood;
    [SerializeField] private GameObject _secondBlood;
    [SerializeField] private GameObject _thirdBlood;
    [SerializeField] private GameObject _roundTextObj;
    [SerializeField] private GameObject _scoreTextObj;
    [SerializeField] private GameObject _ammoTextObj;
    [SerializeField] private GameObject _reloadingImage;
    [SerializeField] private GameObject _lowAmmoImage;
    [SerializeField] private GameObject _totalScoreObj;
    [SerializeField] private GameObject _firstGrenadeOn;
    [SerializeField] private GameObject _firstGrenadeOff;
    [SerializeField] private GameObject _secondGrenadeOn;
    [SerializeField] private GameObject _secondGrenadeOff;
    [SerializeField] private GameObject _thirdGrenadeOn;
    [SerializeField] private GameObject _thirdGrenadeOff;
    [Header("Upgrade UI")]
    [SerializeField] private GameObject _lvlUpPanel;

    private Spawner _spawner;
    private Upgrader _upgrader;
    private Saver _saver;
    private Text _totalScoreText;
    private Text _roundText;
    private Text _scoreText;
    private Text _ammoText;
    private bool _isScoreAdded;
    private int _totalScoreCount;
    private int _scoreCount = 0;
    private int _ammoCount;
    private int _grenadeCount;
    private int _maxGrenades;
    private int _health;
    private int _currState;
    private int _roundCount;

    public bool IsPauseActivate { get { return _pausePanel.activeSelf; } }
    public int TotalScoreCount { get { return _totalScoreCount; } set { _totalScoreCount = value; } }
    public int ScoreCount { get { return _scoreCount; } set { _scoreCount = value; } }

    private void Awake()
    {
        _totalScoreText = _totalScoreObj.GetComponent<Text>();
        _roundText = _roundTextObj.GetComponent<Text>();
        _scoreText = _scoreTextObj.GetComponent<Text>();
        _ammoText = _ammoTextObj.GetComponent<Text>();
        _upgrader = GetComponent<Upgrader>();
        _spawner = GetComponent<Spawner>();
        _darkTolightPanel.SetActive(true);
        _lightToDarkPanel.SetActive(false);
        _tryAgainButton.SetActive(false);
        _reloadingImage.SetActive(false);
        _lowAmmoImage.SetActive(false);
        _totalScoreObj.SetActive(false);
        _pausePanel.SetActive(false);
        _controllsPanel.SetActive(false);
        _lvlUpPanel.SetActive(false);
        _winPanel.SetActive(false);
        _isScoreAdded = false;
    }

    private void Update()
    {
        _ammoCount = _playerInput.AmmoCount;
        _grenadeCount = _playerInput.GrenadesCount;
        _maxGrenades = _playerInput.MaxGrenades;
        _health = _playerHealth.CurrentHealth;
        _roundCount = GetComponent<Spawner>().Round;
        _currState = _health;
        InfoOnScreen();
        ReloadingCheck();
        GrenadesInfo();
        LowAmmoCheck();

        if (Time.timeSinceLevelLoad > 1f)
        {
            _darkTolightPanel.SetActive(false);
        }

        if (GetComponent<Spawner>().YouWon)
        {
            _winPanel.SetActive(true);
        }

        switch (_currState)
        {
            case 0:

                _firstBlood.SetActive(true);
                _secondBlood.SetActive(true);
                _thirdBlood.SetActive(true);
                if (!_isScoreAdded)
                {
                    TotalScoreAddition();
                    _isScoreAdded = true;
                }
                Invoke("ShowScoreAndPanels", 2f);
                _upgrader.GetTotalScore();
                _upgrader.AmmoIncreaserButtonText();
                _upgrader.ShootIncreaserButtonText();
                _upgrader.ReloadIncreaserButtonText();
                _upgrader.SpeedIncreaserButtonText();
                _upgrader.GrenadesIncreaserButtonText();
                ShowTotalScore();
                break;
            case 1:
                _firstBlood.SetActive(true);
                _secondBlood.SetActive(true);
                _thirdBlood.SetActive(false);
                break;
            case 2:
                _firstBlood.SetActive(true);
                _secondBlood.SetActive(false);
                _thirdBlood.SetActive(false);
                break;
            case 3:
                _firstBlood.SetActive(false);
                _secondBlood.SetActive(false);
                _thirdBlood.SetActive(false);
                break;
        }
    }

    private void OnEnable()
    {
        PlayerInput.PauseActive += PauseActive;
    }

    private void OnDisable()
    {
        PlayerInput.PauseActive -= PauseActive;
    }

    private void PauseActive()
    {
        if (_pausePanel.activeSelf)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void InfoOnScreen()
    {
        _roundText.text = "Round: " + _spawner.Round.ToString();
        _scoreText.text = "Score: " + _scoreCount.ToString();
        _ammoText.text = "Ammo: " + _ammoCount.ToString();
    }

    private void GrenadesInfo()
    {
        if (_maxGrenades == 1)
        {
            _secondGrenadeOff.SetActive(false);
            _secondGrenadeOn.SetActive(false);
            _thirdGrenadeOff.SetActive(false);
            _thirdGrenadeOn.SetActive(false);
            if (_grenadeCount == 1)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
            }
            else
            {
                _firstGrenadeOn.SetActive(false);
                _firstGrenadeOff.SetActive(true);
            }
        }
        else if (_maxGrenades == 2)
        {
            _thirdGrenadeOff.SetActive(false);
            _thirdGrenadeOn.SetActive(false);
            if (_grenadeCount == 1)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
                _secondGrenadeOn.SetActive(false);
                _secondGrenadeOff.SetActive(true);
            }
            else if (_grenadeCount == 2)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
                _secondGrenadeOn.SetActive(true);
                _secondGrenadeOff.SetActive(false);
            }
            else if (_grenadeCount == 0)
            {
                _firstGrenadeOn.SetActive(false);
                _firstGrenadeOff.SetActive(true);
                _secondGrenadeOn.SetActive(false);
                _secondGrenadeOff.SetActive(true);
            }
        }
        else if (_maxGrenades == 3)
        {
            if (_grenadeCount == 1)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
                _secondGrenadeOn.SetActive(false);
                _secondGrenadeOff.SetActive(true);
                _thirdGrenadeOn.SetActive(false);
                _thirdGrenadeOff.SetActive(true);
            }
            else if (_grenadeCount == 2)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
                _secondGrenadeOn.SetActive(true);
                _secondGrenadeOff.SetActive(false);
                _thirdGrenadeOn.SetActive(false);
                _thirdGrenadeOff.SetActive(true);
            }
            else if (_grenadeCount == 3)
            {
                _firstGrenadeOn.SetActive(true);
                _firstGrenadeOff.SetActive(false);
                _secondGrenadeOn.SetActive(true);
                _secondGrenadeOff.SetActive(false);
                _thirdGrenadeOn.SetActive(true);
                _thirdGrenadeOff.SetActive(false);
            }
            else if (_grenadeCount == 0)
            {
                _firstGrenadeOn.SetActive(false);
                _firstGrenadeOff.SetActive(true);
                _secondGrenadeOn.SetActive(false);
                _secondGrenadeOff.SetActive(true);
                _thirdGrenadeOn.SetActive(false);
                _thirdGrenadeOff.SetActive(true);
            }
        }
        else
        {
            _firstGrenadeOn.SetActive(false);
            _firstGrenadeOff.SetActive(false);
            _secondGrenadeOn.SetActive(false);
            _secondGrenadeOff.SetActive(false);
            _thirdGrenadeOff.SetActive(false);
            _thirdGrenadeOn.SetActive(false);
        }
    }

    private void LowAmmoCheck()
    {
        bool reloading = _playerInput.IsReloading;
        if (_ammoCount <= 3 & !reloading)

        {
            _lowAmmoImage.SetActive(true);
        }
        else
        {
            _lowAmmoImage.SetActive(false);
        }
    }

    private void ReloadingCheck()
    {
        bool reloading = _playerInput.IsReloading;
        if (reloading)
        {
            _reloadingImage.SetActive(true);
        }
        else
        {
            _reloadingImage.SetActive(false);
        }
    }

    private void TotalScoreAddition()
    {
        if (_playerData.SetTotalScore != 0)
        {
            _totalScoreCount = _playerData.SetTotalScore;
        }
        else
        {
            _totalScoreCount = GetComponent<Saver>().TotalScoreToSave;
        }
        _totalScoreCount += _scoreCount;
    }

    private void ShowTotalScore()
    {
        _totalScoreText.text = "Total score: " + _totalScoreCount;
    }

    private void SaveAllData()
    {
        var saver = GetComponent<Saver>();
        saver.SaveAmmo();
        saver.SaveReloadDelay();
        saver.SaveScore();
        saver.SaveSpeed();
        saver.SaveShootDelay();
        saver.SaveScore();
        saver.SaveGrenades();
    }

    public void TryAgain()
    {
        SaveAllData();
        _lightToDarkPanel.SetActive(true);
        Invoke("ReloadGameScene", 1f);
    }

    private void ReloadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ClearData()
    {
        _saver.ClearData();
    }

    private void ShowScoreAndPanels()
    {
        _tryAgainButton.SetActive(true);
        _totalScoreObj.SetActive(true);
        _lvlUpPanel.SetActive(true);
    }

    public void ControllsButton()
    {
        _pausePanel.SetActive(false);
        _controllsPanel.SetActive(true);
    }

    public void BackButton()
    {
        _controllsPanel.SetActive(false);
        _pausePanel.SetActive(true);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        _lightToDarkPanel.SetActive(true);
        Invoke("GoToMainMenu", 1f);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        _lightToDarkPanel.SetActive(true);
        Invoke("Quitting", 1f);
    }

    private void Quitting()
    {
        Application.Quit();
    }
}
