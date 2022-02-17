using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource hurt1;
    [SerializeField] private AudioSource hurt2;
    [SerializeField] private AudioSource hurt3;
    [SerializeField] private AudioSource attack1;
    [SerializeField] private AudioSource attack2;
    [SerializeField] private AudioSource attack3;
    [SerializeField] private AudioSource cast;
    [SerializeField] private AudioSource shouts1;
    [SerializeField] private AudioSource shouts2;
    [SerializeField] private AudioSource shouts3;
    [SerializeField] private AudioSource shouts4;
    private float _currShoutTime;
    private float _shoutTime = 0f;
    private float _currHitTimer;
    private bool _isShouting = false;
    private bool _isHitted = false;

    public bool IsShouting { get { return _isShouting; } }

    public void StopShouting()
    {
        shouts1.Stop();
        shouts2.Stop();
        shouts3.Stop();
        shouts4.Stop();
    }

    public void CastSound()
    {
        cast.Play();
    }

    public void ShoutTimer()
    {
        if (_shoutTime < Time.time - _currShoutTime)
        {
            _isShouting = false;
        }
    }

    public void HitTimer()
    {
        if (0.1f < Time.time - _currHitTimer)
        {
            _isHitted = false;
        }
    }

    public void Shouting()
    {
        int r = Random.Range(0, 4);

        if (r == 0)
        {
            shouts1.Play();
            _shoutTime = shouts1.clip.length;
            _isShouting = true;
            _currShoutTime = Time.time;
        }
        else if (r == 1)
        {
            shouts2.Play();
            _shoutTime = shouts2.clip.length;
            _isShouting = true;
            _currShoutTime = Time.time;
        }
        else if (r == 2)
        {
            shouts3.Play();
            _shoutTime = shouts3.clip.length;
            _isShouting = true;
            _currShoutTime = Time.time;
        }
        else if (r == 3)
        {
            shouts4.Play();
            _shoutTime = shouts4.clip.length;
            _isShouting = true;
            _currShoutTime = Time.time;
        }
    }

    public void AttackSound()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            attack1.Play();
        }
        else if (r == 1)
        {
            attack2.Play();
        }
        else if (r == 2)
        {
            attack3.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") & !_isHitted)
        {
            int r = Random.Range(0, 3);
            if (r == 0)
            {
                hurt1.Play();
                _isHitted = true;
                _currHitTimer = Time.time;
            }
            else if (r == 1)
            {
                hurt2.Play();
                _isHitted = true;
                _currHitTimer = Time.time;
            }
            else if (r == 2)
            {
                hurt3.Play();
                _isHitted = true;
                _currHitTimer = Time.time;
            }
        }
    }
}
