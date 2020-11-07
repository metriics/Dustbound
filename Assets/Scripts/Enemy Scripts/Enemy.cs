using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make enemy class
public abstract class Enemy : MonoBehaviour
{
    private string name { get; set; }
    private float damage { get; set; }

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

}

public class Lancer : Enemy
{

}

public class Marksman : Enemy
{

}
