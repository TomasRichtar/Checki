using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "ScriptableObjects/Reward", order = 3)]
public class Reward : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public int Price;

    // Admin specific
    public string ValidUntil;
    public int UserId;
}
