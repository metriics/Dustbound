using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    int enemyID = 0;
    List<Enemy> enemyList = new List<Enemy>();
    public EnemyFactory enemyFactory;
    public static EnemyManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<Enemy>().ID = enemyID;
            enemyID++;
        }
        AddEnemy("berserker");
        FindEnemy(enemyList[enemyList.Count - 1].ID).SetPos(new Vector3(109f, 1.5f, -3f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPlayer() 
    { 
        Debug.Log(player.name); 
        return player; 
    }

    public void SetPos(int ID, Vector3 pos) 
    {
        if(FindEnemy(ID) != null) 
        {
            FindEnemy(ID).pos = pos;
        }
    }

    public Enemy SpawnEnemy(string type = "null")
    {
        return FindEnemy(type);
    }

    private Enemy FindEnemy(string type)
    {
        foreach(Enemy enemy in enemyList){
            if(enemy.enemy.charName.ToLower() == type)
            {
                return enemy;
            }
        }
        Debug.Log("No enemy of type " + type);
        return null;
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
        enemyList.Add(EnemyFactory.Instance.GenerateEnemy(type));
        enemyList[enemyList.Count - 1].ID = enemyID;
        enemyID++;
    }
}
