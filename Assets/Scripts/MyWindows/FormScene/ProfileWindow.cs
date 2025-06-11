using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileWindow : BaseWindow
{
    [Header("Buttons")]
    public Button MainScene;
    public Button Quests;
    public Button Rewards;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        Quests.onClick.AddListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.AddListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        Quests.onClick.RemoveListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
    }

    public void SetProfileData()
    {

    }
}
