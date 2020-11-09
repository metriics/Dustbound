using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Transform player;
    public int questsCompleted = 0;
    public float completedTimer = 0.0f;
    public Quest curQuest;
    public List<Quest> quests = new List<Quest>();
    public GameObject waypointPrefab;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI questDesc;
    public GameObject checkmark;
    public GameObject checkbox;

    public static QuestManager current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            quests.Add(this.transform.GetChild(i).GetComponent<Quest>());
        }

        curQuest = quests[questsCompleted];
        checkmark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (curQuest != null)
        {
            checkbox.SetActive(true);

            if (curQuest.completed && curQuest.active)
            {
                checkmark.SetActive(true);
                completedTimer += Time.deltaTime;
                if (completedTimer >= 1.0f)
                {
                    questsCompleted++;
                    curQuest.active = false;
                    completedTimer = 0.0f;
                    checkmark.SetActive(false);
                    if (questsCompleted < quests.Count)
                    {
                        curQuest = quests[questsCompleted];
                    }
                    else
                    {
                        questText.text = "No current quests";
                        questDesc.text = "";
                        checkbox.SetActive(false);
                        curQuest = null;
                    }
                }
            }
            else if (!curQuest.active)
            {
                curQuest.active = true;
                questText.text = curQuest.questData.questName;
                questDesc.text = curQuest.questData.questDesc;
            }
        }

    }
}
