using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make enemy class
public class Enemy : MonoBehaviour
{
    private string name { get; set; }

    public int health { get; set; } 
    private float damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameEvents.current.EnemyKilled();
            Destroy(this.gameObject);
        }
    }

    public void OnHit(Attack _attack)
    {
  
    }
}

//public class Berserker : Enemy
//{

//}

//public class Lancer : Enemy
//{

//}

//public class Marksman : Enemy
//{

//}
