using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    public string questName;
    public string questDesc;
    public int reward;
    public int exp;
    public int levelReq;
}
