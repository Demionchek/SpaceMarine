using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGrenade : MonoBehaviour
{
    private GameObject PlayerObj;

    public void GetPlayerObject(GameObject Player)
    {
        PlayerObj = Player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerObj.GetComponent<PlayerInput>().GrenadesCount != 
                   PlayerObj.GetComponent<PlayerInput>().MaxGrenades)
            {

                PlayerObj.GetComponent<PlayerInput>().GrenadesCount++;
                Destroy(gameObject);
            }
        }
    }
}
