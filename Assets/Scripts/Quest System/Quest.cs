using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    GameObject waypoint;
    Waypoint waypointData;
    public Vector3 worldPosition;
    public Vector3 offset;
    public QuestData questData;

    public bool active = false;
    public bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (questData.hasWaypoint)
        {
            waypoint = Instantiate(this.transform.GetComponentInParent<QuestManager>().waypointPrefab, this.transform.parent);
            waypointData = waypoint.GetComponent<Waypoint>();
            waypointData.worldPosition = worldPosition;
            waypointData.offset = offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (questData.hasWaypoint)
        {
            if (active == true)
            {
                waypoint.SetActive(true);
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
}
