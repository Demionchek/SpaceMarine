using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemyMovement : StateMachine
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject _grenadeDrop;
    [SerializeField] private GameObject _medKitDrop;
    [SerializeField] private bool _isRange;
    [SerializeField] private bool _isArachna;
    [SerializeField] private GameObject _hitBox;
    [SerializeField] private GameObject _energySphere;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private int _dropChance1to;
    [SerializeField] private int _scoreForKill;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _rangeAttackDistance = 10f;
    [SerializeField] private float _burstDistance = 7f;
    [SerializeField] private float _burstCooldown = 4f;
    [SerializeField] private float _burstSpeed = 8f;


    private Health _playerHealth;
    private EnemySounds _enemySounds;
    private Spawner _spawner;
    private NavMeshAgent _navMeshAgent;
    private Health _health;
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    private Transform _target;
    private Animator _animator;
    private Vector3 _lastTargetPos;
    private float _currBurstTime = 0f;
    private float _normalNavSpeed;
    private bool _isDead = false;
    private bool _burstCooled = true;
    private bool _isBursting = false;
    private bool _isPlayerAlive;

    public NavMeshAgent NavMeshAgent
    {
        get
        {
            return _navMeshAgent;
        }
        set
        {
            _navMeshAgent.enabled = value;
            _navMeshAgent.isStopped = value;
        }
    }
    public CapsuleCollider CapsuleCollider { get { return _capsuleCollider; } set { _capsuleCollider.enabled = value; } }
    public BoxCollider BoxCollider { get { return _boxCollider; } set { _boxCollider.enabled = value; } }
    public Animator Animator
    {
        get
        {
            return _animator;
        }
        set
        {
            _animator.SetBool("Idle", value);
            _animator.SetBool("Burst", value);
            _animator.SetBool("Run", value);
            _animator.SetBool("Attack", value);
            _animator.SetBool("Shoot", value);
        }
    }
    public Transform Target { get { return _target; } }
    public Vector3 LastTargetPos { get { return _lastTargetPos; } }
    public bool IsRange { get { return _isRange; } }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public bool BurstCooled { get { return _burstCooled; } set { _burstCooled = value; } }
    public bool IsBursting { get { return _isBursting; } set { _isBursting = value; } }
    public bool IsArachna { get { return _isArachna; } }
    public bool IsPlayerAlive { get { return _isPlayerAlive; } }
    public float NavMeshAgentSpeed { get { return _navMeshAgent.speed; } set { _navMeshAgent.speed = value; } }
    public float BurstSpeed { get { return _burstSpeed; } }
    public float NormalNavSpeed { get { return _normalNavSpeed; } }

    private void Awake()
    {
        _isPlayerAlive = true;
        _enemySounds = GetComponent<EnemySounds>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _boxCollider = GetComponent<BoxCollider>();
        _normalNavSpeed = _navMeshAgent.speed;
        SetState(new IdleState(this));
        if (_isRange)
        {
            _navMeshAgent.stoppingDistance = _rangeAttackDistance;
        }
    }

    public void GetObjects(Spawner spawner, Health playerHealth)
    {
        _spawner = spawner;
        _playerHealth = playerHealth;
    }

    private void Start()
    {
        _target = _playerHealth.gameObject.transform;
    }

    #region EventSubscribe
    private void OnEnable()
    {
        PlayerInput.PlayerIsDead += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerInput.PlayerIsDead -= PlayerDied;
    }

    private void PlayerDied()
    {
        _isPlayerAlive = false;
    }

    #endregion

    public void Update()
    {
        if (!_isDead)
        {
            if (!_enemySounds.IsShouting)
            {
                _enemySounds.Shouting();
            }
        }
        else
        {
            _enemySounds.StopShouting();
        }
        _enemySounds.ShoutTimer();
        _enemySounds.HitTimer();
        StateController();
    }

    private void StateController()
    {
        if (_isRange)
        {
            RangeBehavior();
        }
        else if (_isArachna)
        {
            ArachnaBehaviour();
        }
        else
        {
            MeleeBehavior();
        }

        if (_isDead == false)
        {
            CheckIsAlive();
        }
    }

    private void RangeBehavior()
    {
        if (_isPlayerAlive)
        {
            if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) > _rangeAttackDistance)
            {
                SetState(new RunState(this));

            }
            else if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) < _rangeAttackDistance)
            {
                SetState(new AttackState(this));
            }
        }
        else
        {
            if (_health.IsAlive)
            {
                SetState(new IdleState(this));
            }
        }
    }

    private void ArachnaBehaviour()
    {
        if (_isPlayerAlive)
        {
            if (_burstCooled)
            {
                if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) < _burstDistance)
                {
                    _isBursting = true;
                    _lastTargetPos = _target.position;
                    _currBurstTime = Time.time;
                    SetState(new BurstState(this));
                }
                else if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) > _burstDistance)
                {
                    SetState(new RunState(this));
                }
            }
            else if (!_burstCooled & !_isBursting)
            {
                if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) > _navMeshAgent.stoppingDistance)
                {
                    SetState(new RunState(this));
                }
                else if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) < _navMeshAgent.stoppingDistance)
                {
                    SetState(new AttackState(this));
                }
            }
            CoolDown();
        }
        else
        {
            if (_health.IsAlive)
            {
                SetState(new IdleState(this));
            }
        }
    }

    private void MeleeBehavior()
    {
        if (_isPlayerAlive)
        {
            if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) > _navMeshAgent.stoppingDistance)
            {
                SetState(new RunState(this));
            }
            else if (_health.IsAlive & Vector3.Distance(transform.position, _target.position) < _navMeshAgent.stoppingDistance)
            {
                SetState(new AttackState(this));
            }
        }
        else
        {
            if (_health.IsAlive)
            {
                SetState(new IdleState(this));
            }
        }
    }

    private void CheckIsAlive()
    {
        if (_health.IsAlive == false)
        {
            _spawner.DeadEnemiesCount++;
            _spawner.GetComponent<UIStates>().ScoreCount += _scoreForKill;
            SetState(new DeathState(this));
        }
    }

    private void CoolDown()
    {
        if (_burstCooldown < Time.time - _currBurstTime)
        {
            _burstCooled = true;
        }
    }

    public void BurstFinished()
    {
        _isBursting = false;
        SetState(new IdleState(this));
    }

    public void TurnHitOn()
    {
        _hitBox.SetActive(true);
    }

    public void TurnHitOff()
    {
        _hitBox.SetActive(false);
    }

    public void Drop()
    {
        int r = Random.Range(0, _dropChance1to);

        if (r == 0)
        {
            int r2 = Random.Range(0, 2);

            int maxGrenades = _playerHealth.gameObject.GetComponent<PlayerInput>().MaxGrenades;

            if (r2 == 0 & maxGrenades > 0)
            {
                GameObject grenade = Instantiate(_grenadeDrop, gameObject.transform.position, Quaternion.identity);
                grenade.GetComponent<CollectableGrenade>().GetPlayerObject(_playerHealth.gameObject);
                grenade.transform.parent = null;
            }
            else if (r2 == 0 & maxGrenades == 0)
            {
                GameObject medkit = Instantiate(_medKitDrop, gameObject.transform.position, Quaternion.identity);
                medkit.GetComponent<MedKit>().GetPlayerObject(_playerHealth.gameObject);
                medkit.transform.parent = null;
            }
            else if (r2 == 1)
            {
                GameObject medkit = Instantiate(_medKitDrop, gameObject.transform.position, Quaternion.identity);
                medkit.GetComponent<MedKit>().GetPlayerObject(_playerHealth.gameObject);
                medkit.transform.parent = null;
            }
        }
    }

    public void Shoot()
    {
        Vector3 difference = _target.position - transform.position;
        difference.Normalize();

        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        GameObject currentBullet = Instantiate(_energySphere, _shootPosition.position, Quaternion.Euler(90, rotationY, 0));
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

        rb.AddForce(transform.TransformDirection(Vector3.forward * _shootForce), ForceMode.Impulse);
    }

    public void Revive(Vector3 point)
    {
        transform.position = point;
        gameObject.SetActive(true);
        _health.Revive();
        _isDead = false;
        SetState(new IdleState(this));
        _animator.SetTrigger("Revive");
    }
}
