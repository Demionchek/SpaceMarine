using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _hit;
    [SerializeField] private GameObject _emission;
    [SerializeField] private AudioSource _bamSound;

    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    private SphereCollider _sphereCollider;
    private ParticleSystem _particle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();
        _particle = _explosion.GetComponent<ParticleSystem>();
        _particle.Stop();
        _bamSound.Stop();
        _hit.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = new Vector3(0,0,0);
        _rb.isKinematic = true;
        transform.rotation = Quaternion.Euler(0,0,0);
        _emission.GetComponent<MeshRenderer>().enabled = false;
        _meshRenderer.enabled = false;
        _sphereCollider.enabled = false;
        _particle.Play();
        _bamSound.Play();
        _hit.SetActive(true);
        Invoke("OffHit", 0.1f);
        Invoke("Delete",2f);
    }

    private void OffHit()
    {
        _hit.SetActive(false);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
