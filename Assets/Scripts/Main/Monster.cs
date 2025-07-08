using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Monster", order = 1)]
public class Monster : ScriptableObject
{
    public int Id;
    public string Name = "Monster";
    public Sprite Sprite;
    public Color GlowColor = Color.red;
    public float Size = 1;
    [Header("Equipment")]
    [Header("Hat")]
    public Vector3 HatOffset;
    public Vector3 HatSize;
    public Vector3 HatRotation;

    [Header("Head")]
    public Vector3 HeadOffset;
    public Vector3 HeadSize;
    public Vector3 HeadRotation;

    [Header("Shoulders")]
    public Vector3 ShouldersOffset;
    public Vector3 ShouldersSize;
    public Vector3 ShouldersRotation;

    [Header("Neck")]
    public Vector3 NeckOffset;
    public Vector3 NeckSize;
    public Vector3 NeckRotation;

    [Header("Glasses")]
    public Vector3 GlassesOffset;
    public Vector3 GlassesSize;
    public Vector3 GlassesRotation;

    [Header("Beard")]
    public Vector3 BeardOffset;
    public Vector3 BeardSize;
    public Vector3 BeardRotation;

    [Header("Gloves Right")]
    public Vector3 GlovesRightOffset;
    public Vector3 GlovesRightSize;
    public Vector3 GlovesRightRotation;

    [Header("Gloves Left")]
    public Vector3 GlovesLeftOffset;
    public Vector3 GlovesLeftSize;
    public Vector3 GlovesLeftRotation;

    [Header("Shoes Right")]
    public Vector3 ShoesRightOffset;
    public Vector3 ShoesRightSize;
    public Vector3 ShoesRightRotation;

    [Header("Shoes Left")]
    public Vector3 ShoesLeftOffset;
    public Vector3 ShoesLeftSize;
    public Vector3 ShoesLeftRotation;

    [Header("Hand")]
    public Vector3 HandOffset;
    public Vector3 HandSize;
    public Vector3 HandRotation;

    [Header("Waist")]
    public Vector3 WaistOffset;
    public Vector3 WaistSize;
    public Vector3 WaistRotation;


    [Header("Ideas")]
    public string Description; // An idea
}
