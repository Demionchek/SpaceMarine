using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private int dmg;
    private float timer = 0.1f;
    private float currTime;
    private bool isTimerOn = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") & !isTimerOn)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(dmg);
            currTime = Time.time;
            isTimerOn = true;
        }
            Timer();
    }


    private void Timer()
    {
        if (timer < Time.time - currTime)
        {
            isTimerOn = false;
        }
    }
}
