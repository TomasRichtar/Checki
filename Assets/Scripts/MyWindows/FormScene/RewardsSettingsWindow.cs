using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsSettingsWindow : BaseWindow
{
    [Header("Buttons")]
    public Button MainScene;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
    }
}
