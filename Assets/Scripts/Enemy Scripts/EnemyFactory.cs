using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    //Enemy prefabs
    public GameObject berserker;
    public GameObject lancer;
    public GameObject marksman;
    public static EnemyFactory Instance = null;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = new EnemyFactory();
        }
        else
        {
            Destroy(this);
        }
    }

    public Enemy GenerateEnemy(string enemy = null)
    {
        switch(enemy){
            case "berserker":
                Instantiate(berserker);
                return berserker.GetComponent<Enemy>();
            case "lancer":
                Instantiate(lancer);
                return lancer.GetComponent<Enemy>();
            case "marksman":
                Instantiate(marksman);
                return marksman.GetComponent<Enemy>();
            default:
                Debug.Log("Returned a null enemy? Intentional?");
                return null;
        }
    }
}
