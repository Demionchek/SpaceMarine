using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int dmg;
    private bool isHit = false;

    private void Awake()
    {
        Invoke("Delete", 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit"))
        {
            return;
        }
        else if (!other.CompareTag("Hit") & !(other.CompareTag("Player")) &!(other.CompareTag("Ignore")) & !isHit)
        {
            isHit = true;
            if (other.gameObject.GetComponent<Health>() != null)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(dmg);
            }

            Delete();
        }
        else if (other.CompareTag("Player") || other.CompareTag("Ignore"))
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
