using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminWindow : BaseWindow
{
    [Header("Buttons")]
    public Button Children;
    public Button Quests;
    public Button Rewards;
    public Button Calendar;
    public Button Settings;


    public Button KidVersion;
    public Button LogOgg;

    private void OnEnable()
    {
        Children.onClick.AddListener(WindowController.Instance.PushWindow<ChildrenSettingsWindow>);
        Quests.onClick.AddListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.AddListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        Calendar.onClick.AddListener(WindowController.Instance.PushWindow<CalendarWindow>);
        Settings.onClick.AddListener(WindowController.Instance.PushWindow<SettingsWindow>);

        KidVersion.onClick.AddListener(() => SceneController.Instance.ChildScene());
        LogOgg.onClick.AddListener(WindowController.Instance.PushWindow<LoginWindow>);
    }
    private void OnDisable()
    {
        Children.onClick.RemoveListener(WindowController.Instance.PushWindow<AddChildrenPartOneWindow>);
        Quests.onClick.RemoveListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        Calendar.onClick.RemoveListener(WindowController.Instance.PushWindow<CalendarWindow>);
        Settings.onClick.RemoveListener(WindowController.Instance.PushWindow<SettingsWindow>);

        KidVersion.onClick.RemoveListener(() => SceneController.Instance.ChildScene());
        LogOgg.onClick.RemoveListener(WindowController.Instance.PushWindow<LoginWindow>);
    }
}
