using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGrenade : MonoBehaviour
{
    [SerializeField] private int dmg;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(dmg);
        }
    }
}
