using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    [Header("Required Objects")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;
    [Header("Upgrade Panel Elements")]
    [SerializeField] private GameObject _speedIncreaseButtonObj;
    [SerializeField] private GameObject _ammoIncreaseButtonObj;
    [SerializeField] private GameObject _shootDelayReduceButtonObj;
    [SerializeField] private GameObject _reloadDelayReduceButtonObj;
    [SerializeField] private GameObject _grenadesIncreaserButtonObj;
    [SerializeField] private GameObject _speedIncreaseTextObj;
    [SerializeField] private GameObject _ammoIncreaseTextObj;
    [SerializeField] private GameObject _shootDelayReduceTextObj;
    [SerializeField] private GameObject _reloadDelayReduceTextObj;
    [SerializeField] private GameObject _grenadesIncreaserTextObj;
    [SerializeField] private int _firstLvlUpgrade = 500;
    [SerializeField] private int _secondLvlUpgrade = 1000;
    [SerializeField] private int _thirdLvlUpgrade = 1500;
    private UIStates _uIStates;
    private Text _speedIncreaseText;
    private Text _ammoIncreaseText;
    private Text _shootDelayReduceText;
    private Text _reloadDelayReduceText;
    private Text _grenadesIncreaserText;
    private int _totalScoreCount;

    private void Awake()
    {     
        _speedIncreaseText = _speedIncreaseTextObj.GetComponent<Text>();
        _ammoIncreaseText = _ammoIncreaseTextObj.GetComponent<Text>();
        _shootDelayReduceText = _shootDelayReduceTextObj.GetComponent<Text>();
        _reloadDelayReduceText = _reloadDelayReduceTextObj.GetComponent<Text>();
        _grenadesIncreaserText = _grenadesIncreaserTextObj.GetComponent<Text>();
    }

    public void GetTotalScore()
    {
        _uIStates = GetComponent<UIStates>();
        _totalScoreCount = _uIStates.TotalScoreCount;
    }


    public void AmmoIncreaserButtonText()
    {
        int ammo = _playerInput.FullAmmo;

        if (ammo == 10 & _totalScoreCount >= _firstLvlUpgrade)
        {
            _ammoIncreaseText.text = "" + _firstLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = true;

        }
        else if (ammo == 10 & _totalScoreCount < _firstLvlUpgrade)
        {
            _ammoIncreaseText.text = "" + _firstLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = false;

        }
        else if (ammo == 20 & _totalScoreCount >= _secondLvlUpgrade)
        {

            _ammoIncreaseText.text = "" + _secondLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (ammo == 20 & _totalScoreCount < _secondLvlUpgrade)
        {
            _ammoIncreaseText.text = "" + _secondLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (ammo == 30 & _totalScoreCount >= _thirdLvlUpgrade)
        {
            _ammoIncreaseText.text = "" + _thirdLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (ammo == 30 & _totalScoreCount < _thirdLvlUpgrade)
        {
            _ammoIncreaseText.text = "" + _thirdLvlUpgrade.ToString();
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (ammo == 40)
        {
            _ammoIncreaseText.text = "MAX";
            _ammoIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }

    }

    public void GrenadesIncreaserButtonText()
    {
        float grenades = _playerInput.MaxGrenades;

        if (grenades == 0 & _totalScoreCount >= _firstLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _firstLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (grenades == 0 & _totalScoreCount < _firstLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _firstLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (grenades == 1 & _totalScoreCount >= _secondLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _secondLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (grenades == 1 & _totalScoreCount < _secondLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _secondLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (grenades == 2 & _totalScoreCount >= _thirdLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _thirdLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (grenades == 2 & _totalScoreCount < _thirdLvlUpgrade)
        {
            _grenadesIncreaserText.text = "" + _thirdLvlUpgrade.ToString();
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (grenades == 3)
        {
            _grenadesIncreaserText.text = "MAX";
            _grenadesIncreaserButtonObj.GetComponent<Button>().interactable = false;
        }
    }


    public void ShootIncreaserButtonText()
    {
        float shootDelay = _playerInput.ShootDelay;

        if (shootDelay == 0.35f & _totalScoreCount >= _firstLvlUpgrade)
        {

            _shootDelayReduceText.text = "" + _firstLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (shootDelay == 0.35f & _totalScoreCount < _firstLvlUpgrade)
        {
            _shootDelayReduceText.text = "" + _firstLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (shootDelay == 0.25f & _totalScoreCount >= _secondLvlUpgrade)
        {
            _shootDelayReduceText.text = "" + _secondLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (shootDelay == 0.25f & _totalScoreCount < _secondLvlUpgrade)
        {
            _shootDelayReduceText.text = "" + _secondLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (shootDelay == 0.15f & _totalScoreCount >= _thirdLvlUpgrade)
        {
            _shootDelayReduceText.text = "" + _thirdLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (shootDelay == 0.15f & _totalScoreCount < _thirdLvlUpgrade)
        {
            _shootDelayReduceText.text = "" + _thirdLvlUpgrade.ToString();
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (shootDelay <= 0.06f)
        {
            _shootDelayReduceText.text = "MAX";
            _shootDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
    }

    public void ReloadIncreaserButtonText()
    {
        float reloadTime = _playerInput.ReloadDelay;

        if (reloadTime == 2f & _totalScoreCount >= _firstLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _firstLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (reloadTime == 2f & _totalScoreCount < _firstLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _firstLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (reloadTime == 1.5f & _totalScoreCount >= _secondLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _secondLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (reloadTime == 1.5f & _totalScoreCount < _secondLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _secondLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (reloadTime == 1f & _totalScoreCount >= _thirdLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _thirdLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (reloadTime == 1f & _totalScoreCount < _thirdLvlUpgrade)
        {
            _reloadDelayReduceText.text = "" + _thirdLvlUpgrade.ToString();
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (reloadTime == 0.5f)
        {
            _reloadDelayReduceText.text = "MAX";
            _reloadDelayReduceButtonObj.GetComponent<Button>().interactable = false;
        }
    }

    public void SpeedIncreaserButtonText()
    {
        float speed = _playerMovement.Speed;

        if (speed == 2.5f & _totalScoreCount >= _firstLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _firstLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (speed == 2.5f & _totalScoreCount < _firstLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _firstLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = false;

        }
        else if (speed == 3f & _totalScoreCount >= _secondLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _secondLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (speed == 3f & _totalScoreCount < _secondLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _secondLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (speed == 3.5f & _totalScoreCount >= _thirdLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _thirdLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = true;
        }
        else if (speed == 3.5f & _totalScoreCount < _thirdLvlUpgrade)
        {
            _speedIncreaseText.text = "" + _thirdLvlUpgrade.ToString();
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }
        else if (speed == 4f)
        {
            _speedIncreaseText.text = "MAX";
            _speedIncreaseButtonObj.GetComponent<Button>().interactable = false;
        }
    }

    public void SpeedIncrease()
    {
        float _playerSpeed = _playerMovement.Speed;
        if (_playerSpeed == 2.5f)
        {
           _uIStates.TotalScoreCount -= _firstLvlUpgrade;
        }
        else if (_playerSpeed == 3f)
        {
            _uIStates.TotalScoreCount -= _secondLvlUpgrade;
        }
        else if (_playerSpeed == 3.5f)
        {
            _uIStates.TotalScoreCount -= _thirdLvlUpgrade;
        }
        _playerMovement.Speed += 0.5f;
    }

    public void ShootDelayReduce()
    {
        float shootDelay = _playerInput.ShootDelay;
        if (shootDelay == 0.35f)
        {
            _uIStates.TotalScoreCount -= _firstLvlUpgrade;
        }
        else if (shootDelay == 0.25f)
        {
            _uIStates.TotalScoreCount -= _secondLvlUpgrade;
        }
        else if (shootDelay == 0.15f)
        {
            _uIStates.TotalScoreCount -= _thirdLvlUpgrade;
        }
        _playerInput.ShootDelay -= 0.1f;
    }

    public void ReloadDelayReduce()
    {
        float reloadDelay = _playerInput.ReloadDelay;
        if (reloadDelay == 2f)
        {
            _uIStates.TotalScoreCount -= _firstLvlUpgrade;
        }
        else if (reloadDelay == 1.5f)
        {
            _uIStates.TotalScoreCount -= _secondLvlUpgrade;
        }
        else if (reloadDelay == 1f)
        {
            _uIStates.TotalScoreCount -= _thirdLvlUpgrade;
        }
        _playerInput.ReloadDelay -= 0.5f;
    }

    public void AmmoIncrease()
    {
        int ammo = _playerInput.FullAmmo;
        if (ammo == 10)
        {
            _uIStates.TotalScoreCount -= _firstLvlUpgrade;
        }
        else if (ammo == 20)
        {
            _uIStates.TotalScoreCount -= _secondLvlUpgrade;
        }
        else if (ammo == 30)
        {
            _uIStates.TotalScoreCount -= _thirdLvlUpgrade;
        }
        _playerInput.FullAmmo += 10;
    }

    public void GrenadesIncrease()
    {
        int grenades = _playerInput.MaxGrenades;
        if (grenades == 0)
        {
            _uIStates.TotalScoreCount -= _firstLvlUpgrade;
        }
        else if (grenades == 1)
        {
            _uIStates.TotalScoreCount -= _secondLvlUpgrade;
        }
        else if (grenades == 2)
        {
            _uIStates.TotalScoreCount -= _thirdLvlUpgrade;
        }
        _playerInput.MaxGrenades++;
    }
}

