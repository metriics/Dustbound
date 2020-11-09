using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TODO make enemy class

public class Enemy : MonoBehaviour
{
    public struct Stats
    {
        public int hp;
        public int dmg;
        public int def;
    }

    //Variables + Getters and Setters
    public CharacterObj enemy; //Name, Desc, Base Health, Base Damage, Base Armour
    public Stats stats = new Stats(); //Current health, dmg, def

    public int ID { get; set; }
    public bool alive { get; set; }
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        stats.hp = enemy.baseHealth;
        stats.dmg = enemy.baseDmg;
        stats.def = enemy.baseDef;
        //this.transform.position = pos;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.transform.position;
        if(stats.hp <= 0)
        {
            Debug.Log(enemy.charName + " has fallen!");
            alive = false;
            Destroy(this.gameObject);
        }
    }

    public void SetPos(Vector3 pos)
    {
        this.GetComponent<NavMeshAgent>().Warp(pos);
        this.gameObject.transform.position = pos;
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
        enemy.charName = "Berserker";
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
        enemy.charName = "Lancer";
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
        enemy.charName = "Marksman";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
