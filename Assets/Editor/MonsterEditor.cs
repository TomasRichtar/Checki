using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Monster))]
public class MonsterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Monster monster = (Monster)target;

        if (monster.Name != monster.name)
        {
            string assetPath = AssetDatabase.GetAssetPath(monster);
            AssetDatabase.RenameAsset(assetPath, monster.Name);
            AssetDatabase.SaveAssets();
        }
    }
}
