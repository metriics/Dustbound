using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

public enum QuestType { Location, Action, Progress};

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    public QuestType type;
    public string questName;
    public string questDesc;
    public int reward;
    public int exp;
    public int levelReq;
    public bool showWaypoint;

    public GameEvents.Type subscribedTo;
    public int amountNeeded;
    public int currentAmount = 0;
}

[CustomEditor(typeof(QuestData))]
public class QuestDataEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var questData = target as QuestData;

        // always show this stuff
        questData.type = (QuestType)EditorGUILayout.EnumPopup("Quest type", questData.type);
        questData.questName = EditorGUILayout.TextField("Name", questData.questName);
        questData.questDesc = EditorGUILayout.TextField("Description", questData.questDesc);
        questData.reward = EditorGUILayout.IntField("Reward", questData.reward);
        questData.exp = EditorGUILayout.IntField("Experience", questData.exp);
        questData.levelReq = EditorGUILayout.IntField("Level Requirement", questData.levelReq);
        questData.showWaypoint = EditorGUILayout.Toggle("Show Waypoint", questData.showWaypoint);

        if (questData.type == QuestType.Action) // if the type is action, show event subscription list
        {
            questData.subscribedTo = (GameEvents.Type)EditorGUILayout.EnumPopup("Action type", questData.subscribedTo);
        }

        else if (questData.type == QuestType.Location)
        {
            
        }

        else if (questData.type == QuestType.Progress)
        {
            questData.subscribedTo = (GameEvents.Type)EditorGUILayout.EnumPopup("Progress type", questData.subscribedTo);
            questData.amountNeeded = EditorGUILayout.IntField("Amount needed", questData.amountNeeded);
            questData.currentAmount = EditorGUILayout.IntField("Current amount", questData.currentAmount);
        }
    }
}