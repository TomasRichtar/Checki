using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

[InitializeOnLoad]
public class PlayMode : MonoBehaviour
{
    static PlayMode()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    static void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.EnteredPlayMode)
        {
            ShortcutManager.instance.activeProfileId = "PlayMode";
        }
        else if (playModeState == PlayModeStateChange.EnteredEditMode)
        {
            ShortcutManager.instance.activeProfileId = "Default";
        }
    }
}
