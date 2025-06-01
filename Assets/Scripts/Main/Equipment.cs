using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment", order = 2)]
public class Equipment : ScriptableObject
{
    public string Name = "Equipment";
    public Sprite Sprite;
    public float Size = 1;
    public EquipmentSlot EquipmentSLot;

    [Header("Ideas")]
    public string Description; // An idea
}
