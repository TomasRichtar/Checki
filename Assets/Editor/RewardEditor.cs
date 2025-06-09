using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Reward))]
public class RewardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Reward reward = (Reward)target;

        if (reward.Title != reward.name)
        {
            string assetPath = AssetDatabase.GetAssetPath(reward);
            AssetDatabase.RenameAsset(assetPath, reward.Title);
            AssetDatabase.SaveAssets();
        }
    }
}
