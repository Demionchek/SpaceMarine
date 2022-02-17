using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Enemy
{
    public int count;
    public GameObject prefab;
}

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject Player;
    [SerializeField] private SpawnSetScrptblObj _gameSettings;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<GameObject> enemiesOnScene;
    [SerializeField] private float spawnMaxDistance;
    [SerializeField] private float spawnMinDistance;
    [SerializeField] private int enemiesIncreasePerWave;
    [SerializeField] private int lastRoundIs = 21;
    [SerializeField] private float roundDelay;
    [SerializeField] private float spawnDelay;

    private Health _playerHealth;
    private int _deadEnemiesCount = 0;
    private int _round = 0;
    private int _currentEnemiesCount = 0;
    private bool _newWaveStarted = false;
    private bool _youWon;

    public int Round { get { return _round; } }
    public int DeadEnemiesCount { get { return _deadEnemiesCount; } set { _deadEnemiesCount = value; } }
    public bool YouWon { get { return _youWon; } }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void Awake()
    {
        _youWon = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(enemies[i].prefab);
                enemiesOnScene.Add(enemyObject);
                enemyObject.SetActive(false);
            }
        }

        if (_gameSettings.SetRound != 0)
        {
            _round = _gameSettings.SetRound;
        }

        Invoke("StartSpawn", roundDelay);
    }

    private void Start()
    {
        _playerHealth = Player.GetComponent<Health>();
    }

    private void Update()
    {
        if (_currentEnemiesCount+1 <= _deadEnemiesCount & !_newWaveStarted & _round <= lastRoundIs)
        {
            EnemiesEncriser();

            if (_currentEnemiesCount > enemiesOnScene.Count)
            {
                _currentEnemiesCount = enemiesOnScene.Count;
            }
            Invoke("StartSpawn", roundDelay);
            _newWaveStarted = true;
        }

        if (_round >= lastRoundIs & _deadEnemiesCount == enemiesOnScene.Count)
        {
            _youWon = true;
        }
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnerCorutine());
    }


     private IEnumerator SpawnerCorutine()
    {
        _round++;
        _deadEnemiesCount = 0;
        _newWaveStarted = false;
        for (int i = 0; i < enemiesOnScene.Count; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            Vector3 point;
        link1:
            if (RandomPoint(Player.transform.position, spawnMaxDistance, out point) & 
                                Vector3.Distance(Player.transform.position, point) > spawnMinDistance)
            {
                if (i > _currentEnemiesCount)
                {
                    StopCoroutine(SpawnerCorutine());
                    break;
                }
                else
                {
                    enemiesOnScene[i].GetComponent<EnemyMovement>().GetObjects(this, _playerHealth);
                    enemiesOnScene[i].GetComponent<EnemyMovement>().Revive(point);
                }
            }
            else goto link1;
        }
        yield return null; 
    }

    private void EnemiesEncriser()
    {
        if (_round > 0)
        {
            _currentEnemiesCount = _round * enemiesIncreasePerWave;
        }
    }
}
