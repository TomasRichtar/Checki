using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsSettingsWindow : BaseWindow
{
    [Header("Imputs")]
    [SerializeField] private CustomDropDown CustomDropDown = new CustomDropDown();

    [Header("Buttons")]
    public Button MainScene;
    public Button CreateReward;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(CloseDropDown);
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateReward.onClick.AddListener(WindowController.Instance.ForceEnter<CreateRewardMainWindow>);
        CreateReward.onClick.AddListener(WindowController.Instance.PushWindow<CreateRewardOneWindow>);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(CloseDropDown);
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateReward.onClick.RemoveListener(WindowController.Instance.ForceEnter<CreateRewardMainWindow>);
        CreateReward.onClick.AddListener(WindowController.Instance.PushWindow<CreateRewardOneWindow>);
    }
    public void CloseDropDown()
    {
        CustomDropDown.Close();
    }
}
