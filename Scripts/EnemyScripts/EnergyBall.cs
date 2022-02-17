using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] private int dmg;
    private bool isHit = false;

    private void Awake()
    {
        Invoke("Delete", 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit") | other.CompareTag("Enemy") | other.CompareTag("Ignore"))
        {
            return;
        }
        else if (!(other.CompareTag("Hit") | other.CompareTag("Enemy")) & !isHit)
        {
            isHit = true;
            if (other.gameObject.GetComponent<Health>() != null)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(dmg);
            }
            Delete();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Hit") | collision.rigidbody.CompareTag("Enemy"))
        {
            return;
        }
        else
        {
            Delete();
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
