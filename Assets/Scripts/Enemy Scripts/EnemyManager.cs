using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    int enemyID = 0;
    List<Enemy> enemyList = new List<Enemy>();

    public Enemy SpawnEnemy(string type = "null")
    {
        return FindEnemy(type);
    }

    private Enemy FindEnemy(string type)
    {
        foreach(Enemy enemy in enemyList){
            if(enemy.enemyName.ToLower() == type)
            {
                return enemy;
            }
        }

        AddEnemy(type);
        enemyList[enemyList.Count - 1].ID = enemyID;
        enemyID++;
        return enemyList[enemyList.Count - 1];
    }

    private Enemy FindEnemy(int ID)
    {
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.ID == ID)
            {
                return enemy;
            }
        }

        Debug.Log("Enemy with ID " + ID + " does not exist");
        return null;
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
