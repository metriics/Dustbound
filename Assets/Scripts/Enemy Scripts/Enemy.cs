using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make enemy class
public abstract class Enemy : MonoBehaviour
{
    //Variables + Getters and Setters
    public string enemyName { get; set; }
    public int ID { get; set; }
    public float damage { get; set; }
    public bool alive { get; set; }

    public Enemy(GameObject prefab = null, string initName = null, int initID = 0, float initDamage = 1.0f)
    {
        enemyName = initName;
        ID = initID;
        damage = initDamage;
        alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(Attack _attack)
    {
  
    }
}

public class Berserker : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Berserker";
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Lancer : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Lancer";
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Marksman : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Marksman";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
