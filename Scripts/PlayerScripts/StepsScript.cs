using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StepsScript : MonoBehaviour
{
    [SerializeField] private AudioSource step1;
    [SerializeField] private AudioSource step2;
    [SerializeField] private AudioSource step3;
    [SerializeField] private AudioSource step4;
    [SerializeField] private AudioSource step5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") )
        {
            int r = Random.Range(0, 5);

            if (r == 0)
            {
                step1.Play();
            }
            else if (r == 1)
            {
                step2.Play();
            }
            else if (r == 2)
            {
                step3.Play();
            }
            else if (r == 3)
            {
                step4.Play();
            }
            else if (r == 4)
            {
                step5.Play();
            }
        }
    }
}
