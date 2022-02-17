using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject grenade;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce;
    [SerializeField] private float launchMultiplater;
    [SerializeField] private ParticleSystem particle;
    private PlayerSounds _playerSounds;

    private void Awake()
    {
        _playerSounds = GetComponent<PlayerSounds>();
    }

    public void Shoot(Vector3 mousePos)
    {
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();

        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(90, rotationY, 0));
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

        rb.AddForce(transform.TransformDirection(Vector3.forward * shootForce), ForceMode.Impulse);
        particle.Play();
        _playerSounds.ShotSound();
    }



    public void GrenadeLaunch(Vector3 mousePos)
    {
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();

        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        GameObject currentBullet = Instantiate(grenade, firePoint.position, Quaternion.Euler(90, rotationY, 0));
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

        rb.AddForce(transform.TransformDirection(Vector3.forward *
            (Vector3.Distance(gameObject.transform.position, mousePos) * launchMultiplater)), ForceMode.Impulse);
        particle.Play();
        _playerSounds.LaunchSound();
    }
}
