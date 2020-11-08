using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // waypoint specific
    GameObject waypoint;
    Waypoint waypointData;
    public Vector3 worldPosition;
    public Vector3 offset;

    // action specific


    // counter specific

    
    public QuestData questData;

    public bool active = false;
    public bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (questData.type == QuestType.Waypoint)
        {
            waypoint = Instantiate(QuestManager.current.waypointPrefab, this.transform);
            waypointData = waypoint.GetComponent<Waypoint>();
            waypointData.worldPosition = worldPosition;
            waypointData.offset = offset;
        }

        else if (questData.type == QuestType.Action)
        {
            // subscribe to events here
            GameEvents.current.onPlayerJump += PlayerJumpEvent;
            GameEvents.current.onPlayerMove += PlayerMoveEvent;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (questData.type == QuestType.Waypoint)
        {
            if (active == true)
            {
                if (questData.showWaypoint)
                {
                    waypoint.SetActive(true);
                }
                
                //Quest Behaviour stuff

            }
            else
            {
                waypoint.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            completed = true;
        }
    }

    public void PlayerJumpEvent()
    {
        // check if current event is 
        completed = true;
    }

    public void PlayerMoveEvent()
    {
        // check if current event is 
    }
}
