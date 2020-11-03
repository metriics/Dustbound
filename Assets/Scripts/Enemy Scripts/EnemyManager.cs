using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    List<Enemy> enemyList = new List<Enemy>();

    public Enemy SendEnemy(string type = "null")
    {
        return FindEnemy(type);
    }

    public void GetEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    private Enemy FindEnemy(string type)
    {
        foreach(Enemy enemy in enemyList){
            if(enemy.name.ToLower() == type)
            {
                return enemy;
            }
        }

        AddEnemy(type);
        return enemyList[enemyList.Count - 1];
    }

    private void AddEnemy(string type = "null")
    {
        if(type == "null")
        {
            int temp = Random.Range(1, 3);
            switch (temp)
            {
                case 1:
                    type = "berserker";
                    break;
                case 2:
                    type = "lancer";
                    break;
                case 3:
                    type = "marksman";
                    break;
            }
        }

        EnemyFactory.Instance.GenerateEnemy(type);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
