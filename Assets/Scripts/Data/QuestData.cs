using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public int Id;

    public string Name;
    public QuestType QuestType;
    public QuestStatus QuestStatus;
    public string ComplitionTime; // 11:00; 21:00
    public Days Day;
    public string ValidUntil;
    public int RepeatCount;
    public int Credit;
    public int UserId;
}
