using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFactory : MonoBehaviour
{
    //Enemy prefabs
    public GameObject berserker = null;
    public GameObject lancer;
    public GameObject marksman;
    public static EnemyFactory Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
    }

    public Enemy GenerateEnemy(string enemy = null)
    {
        GameObject temp = null;
        switch (enemy) {
            case "berserker":
                temp = Instantiate(berserker, EnemyManager.Instance.gameObject.transform);
                return temp.GetComponent<Enemy>();
            case "lancer":
                temp = Instantiate(lancer, EnemyManager.Instance.gameObject.transform);
                return temp.GetComponent<Enemy>();
            case "marksman":
                temp = Instantiate(marksman, EnemyManager.Instance.gameObject.transform);
                return temp.GetComponent<Enemy>();
            default:
                Debug.Log("Returned a null enemy? Intentional?");
                return null;
        }
    }
}
