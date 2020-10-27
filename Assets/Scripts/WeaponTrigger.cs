using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player was hit");
        }
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy was hit");
        }
    }
}
