using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment", order = 2)]
public class Equipment : ScriptableObject
{
    public int Id;
    public string Name = "Equipment";
    public Sprite Sprite;
    public Sprite OffSprite;
    public float Size = 1;
    public EquipmentSlot EquipmentSlot;

    [Header("Ideas")]
    public string Description; // An idea
}
