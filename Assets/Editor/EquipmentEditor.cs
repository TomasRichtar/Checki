using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Equipment equipment = (Equipment)target;

        if (equipment.Name != equipment.name)
        {
            string assetPath = AssetDatabase.GetAssetPath(equipment);
            AssetDatabase.RenameAsset(assetPath, equipment.Name);
            AssetDatabase.SaveAssets();
        }
    }
}
