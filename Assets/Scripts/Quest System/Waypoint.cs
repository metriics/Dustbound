using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{
    Image img;
    TextMeshProUGUI distance;
    public Transform player;
    public Vector3 worldPosition;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        img = this.transform.GetComponent<Image>();
        distance = this.transform.GetComponentInChildren<TextMeshProUGUI>();
        player = QuestManager.current.GetPlayer();
        //this.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
    }

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPosition + offset);

        if (Vector3.Dot((worldPosition - player.position), player.forward) < 0)
        {
            //Target is behind the player
            if(pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        //I didnt like clamping it
        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        distance.text = ((int)Vector3.Distance(worldPosition, player.position)).ToString() + "m";
    }
}
