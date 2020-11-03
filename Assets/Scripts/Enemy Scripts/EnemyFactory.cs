using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    //Enemy prefabs
    public Enemy berserker;
    public Enemy lancer;
    public Enemy marksman;
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
                return Instantiate(berserker);
            case "lancer":
                return Instantiate(lancer);
            case "marksman":
                return Instantiate(marksman);
            default:
                Debug.Log("Returned a null enemy? Intentional?");
                return null;

        }
    }
}
