using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "ScriptableObjects/Reward", order = 3)]
public class Reward : ScriptableObject
{
    public string Title;
    public int ImageId;
    public int Price;

    // Admin specific
    public string ValidUntil;
    public int ChildrenId;
    public int ProfileId;
}
