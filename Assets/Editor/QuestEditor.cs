using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Quest quest = (Quest)target;

        if (quest.Title != quest.name)
        {
            string assetPath = AssetDatabase.GetAssetPath(quest);
            AssetDatabase.RenameAsset(assetPath, quest.Title);
            AssetDatabase.SaveAssets();
        }
    }
}
