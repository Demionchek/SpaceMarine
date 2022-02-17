using UnityEngine;

public class Health : MonoBehaviour
{
    public bool IsAlive { get { return _isAlive; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    [SerializeField] private int _currentHealth;
    private bool _isAlive = true;
    private int _fullHealth;

    private void Awake()
    {
        _fullHealth = _currentHealth;
    }

    public void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isAlive = false;
        }
    }

    public void Revive()
    {
        _currentHealth = _fullHealth;
        _isAlive = true;
    }
}
