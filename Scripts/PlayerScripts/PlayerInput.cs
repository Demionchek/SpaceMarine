using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ActorView))]
[RequireComponent(typeof(Shooting))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Saver _saver;
    [SerializeField] private UIStates _uIStates;
    private PlayerMovement _playerMovement;
    private PlayerSounds _playerSounds;
    private Health _health;
    private ActorView _actorView;
    private Shooting _shooting;
    private Vector3 _mousePos;
    private int _ammoCount;
    private int _grenadesCount;
    private int _fullAmmo;
    private int _maxGrenades;
    private float _shootDelay;
    private float _reloadDelay;
    private float _x = 0;
    private float _y = 0;
    private float currTime;
    private bool _isReloading;
    private bool _canShoot = true;
    private bool _isPauseActive;

    public int AmmoCount => _ammoCount;
    public bool IsReloading => _isReloading;
    public int GrenadesCount
    {
        get { return _grenadesCount; }
        set { _grenadesCount = value; }
    }
    public int FullAmmo
    {
        get { return _fullAmmo; }
        set { _fullAmmo = value; }
    }
    public int MaxGrenades
    {
        get { return _maxGrenades; }
        set { _maxGrenades = value; }
    }
    public float ShootDelay
    {
        get { return _shootDelay; }
        set { _shootDelay = value; }
    }
    public float ReloadDelay
    {
        get { return _reloadDelay; }
        set { _reloadDelay = value; }
    }

    public delegate void IsPaused();
    public static event IsPaused PauseActive;

    public delegate void IsDead();
    public static event IsDead PlayerIsDead;

    private void Awake()
    {
        _playerSounds = GetComponent<PlayerSounds>();
        _playerMovement = GetComponent<PlayerMovement>();
        _health = GetComponent<Health>();
        _actorView = GetComponent<ActorView>();
        _shooting = GetComponent<Shooting>();
    }

    private void Start()
    {
        _isPauseActive = _uIStates.IsPauseActivate;
        _saver.LoadAmmo();
        _saver.LoadShootDelay();
        _saver.LoadReloadDelay();
        _saver.LoadGrenades();
        _fullAmmo = _saver.AmmoCountToSave;
        _shootDelay = _saver.ShootDelayToSave;
        _reloadDelay = _saver.ReloadDelayToSave;
        _maxGrenades = _saver.GrenadesCountToSave;
        CheckLoadResult();
        _ammoCount = _fullAmmo;
       
    }

    private void Update()
    {
        if (_health.IsAlive)
        {
            _x = Input.GetAxis(GameData.HorizontalAxis);
            _y = Input.GetAxis(GameData.VerticalAxis);

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _mousePos = hit.point;
            }

            if (Input.GetKey(KeyCode.Mouse0) & _ammoCount > 0 & _canShoot)
            {
                _canShoot = false;
                _ammoCount--;
                currTime = Time.time;
                _shooting.Shoot(_mousePos);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) & _grenadesCount > 0 & Time.timeScale == 1f)
            {
                _canShoot = false;
                _grenadesCount--;
                currTime = Time.time;
                _shooting.GrenadeLaunch(_mousePos);
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseActive != null)
                {
                    PauseActive();
                }

                if (_canShoot)
                {
                    _canShoot = false;
                }
                else
                {
                    _canShoot = true;
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _isReloading = true;
                _canShoot = false;
                currTime = Time.time;
                _playerSounds.PlayReloadSound();
                ReloadDelayCounter();
            }

            if (!_isPauseActive)
            {
                if (_x != 0 || _y != 0)
                {
                    _playerMovement.Move(_x, _y);

                }
                _playerMovement.MouseLook(_mousePos);
            }
        }
        else
        {
            PlayerIsDead();
        }

        if (!_isReloading)
        {
            ShootDelayCounter();
        }
        else
        {
            ReloadDelayCounter();
        }
        _actorView.AnimationState(_x, _y);
    }

    private void ShootDelayCounter()
    {
        if (_shootDelay < Time.time - currTime)
        {
            _canShoot = true;
        }
    }

    private void ReloadDelayCounter()
    {
        if (_reloadDelay < Time.time - currTime)
        {
            _playerSounds.StopReloadSound();
            _ammoCount = _fullAmmo;
            _canShoot = true;
            _isReloading = false;
        }
    }

    private void CheckLoadResult()
    {
        if (_fullAmmo == 0)
        {
            _fullAmmo = 10;
        }
        if (_shootDelay == 0f)
        {
            _shootDelay = 0.35f;
        }
        if (_reloadDelay == 0)
        {
            _reloadDelay = 2f;
        }
    }
}
