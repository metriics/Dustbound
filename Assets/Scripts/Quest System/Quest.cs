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

    bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        waypoint = Instantiate(this.transform.GetComponentInParent<QuestManager>().waypointPrefab, this.transform.parent);
        waypointData = waypoint.GetComponent<Waypoint>();
        waypointData.worldPosition = worldPosition;
        waypointData.offset = offset;
    }

    // Update is called once per frame
    void Update()
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
