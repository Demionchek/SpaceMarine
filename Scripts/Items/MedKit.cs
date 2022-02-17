using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private GameObject PlayerObj;

    public void GetPlayerObject(GameObject Player)
    {
        PlayerObj = Player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & PlayerObj.GetComponent<Health>().CurrentHealth != 3)
        {
            PlayerObj.GetComponent<Health>().CurrentHealth++;
            Destroy(gameObject);
        }
    }


}
