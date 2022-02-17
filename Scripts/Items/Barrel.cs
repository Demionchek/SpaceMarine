using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject barrel;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float timeToSpawn;
    private BoxCollider boxCollider;
    private float currTime;
    private bool isTimerOn;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        hit.SetActive(false);
        isTimerOn = false;
    }

    private void Start()
    {
        explosion.Stop();
        barrel.SetActive(true);
    }

    private void Update()
    {
        if (timeToSpawn < Time.time - currTime)
        {
            isTimerOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") & !isTimerOn)
        {
            barrel.SetActive(false);
            hit.SetActive(true);
            boxCollider.enabled = false;
            explosion.Play();
            isTimerOn = true;
            currTime = Time.time;
            Invoke("TurnHitOff", 0.1f);
            StartCoroutine(SpawnCorutine());
        }
    }

    private void TurnHitOff()
    {
        hit.SetActive(false);
    }

    private IEnumerator SpawnCorutine()
    {
        
        while (Vector3.Distance(transform.position, player.position) < spawnDistance | isTimerOn)
        {

            yield return new WaitForSeconds(5f);
        }
        barrel.SetActive(true);
        boxCollider.enabled = true;
        StopCoroutine(SpawnCorutine());
    }

}
