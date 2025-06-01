using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSettingsWindow : BaseWindow
{
    [Header("Buttons")]
    public Button MainScene;
    public Button CreateQuest;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateQuest.onClick.AddListener(WindowController.Instance.PushWindow<CreateQuestMainWindow>);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateQuest.onClick.RemoveListener(WindowController.Instance.PushWindow<CreateQuestMainWindow>);
    }
}
