using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameEvents.current.PlayerHit();
            Debug.Log("Player was hit");
        }
        if (other.tag == "Enemy")
        {
            GameEvents.current.EnemyHit();
            Debug.Log("Enemy was hit");
        }
    }
}
