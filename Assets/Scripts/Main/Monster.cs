using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Monster", order = 1)]
public class Monster : ScriptableObject
{
    public string Name = "Monster";
    public Sprite Sprite;
    public Color GlowColor = Color.red;
    public float Size = 1;

    [Header("Ideas")]
    public string Description; // An idea
}
