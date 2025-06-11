using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 4)]
public class QuestScriptableObject : ScriptableObject
{
    public int Id;
    public string Title;
    public string ComplitionTime;
    public string Days;
    public int Repeatable;
    public int Credit;
    public string QuestStatus;
    public int ProfileId;
    public int ChildrenId;
    public int ImageId;

    //public int Id;
    //public string Title;
    //public string ComplitionTime; // 11:00; 21:00
    //public List<DayOfWeek> Days;
    //public int Repeatable;
    //public int Credit;
    //public QuestStatus QuestStatus;
    //public int UserId;
    //public int ChildrenId;

    //public Sprite Image;
    ////public QuestType QuestType;
}
