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
    public Button EditOne;
    public Button EditTwo;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        Quests.onClick.AddListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.AddListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        EditOne.onClick.AddListener(OpenEdit);
        EditTwo.onClick.AddListener(OpenEdit);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        Quests.onClick.RemoveListener(WindowController.Instance.PushWindow<QuestSettingsWindow>);
        Rewards.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardsSettingsWindow>);
        EditOne.onClick.RemoveListener(OpenEdit);
        EditTwo.onClick.RemoveListener(OpenEdit);
    }

    public void SetProfileData()
    {

    }
    private void OpenEdit()
    {
        WindowController.Instance.ForceEnter<AddChildrenMainWindow>();
        WindowController.Instance.PushWindow<AddChildrenPartOneWindow>();
        WindowController.Instance.GetWindow<AddChildrenPartOneWindow>().FillUpData();
        WindowController.Instance.GetWindow<AddChildrenPartTwoWindow>().IsCreatingNew = false;
    }
}
