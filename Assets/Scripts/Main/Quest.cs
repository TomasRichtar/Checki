using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 4)]
public class Quest : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public QuestType QuestType;
    public QuestStatus QuestStatus;
    public string ComplitionTime; // 11:00; 21:00
    public Days Day;
    public int RepeatCount;
    public int Credit;
    public int UserId;
}
