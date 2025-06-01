using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildrenSettingsWindow : BaseWindow
{
    [Header("Buttons")]
    public Button Invite;
    public Button MainScene;
    public Button AddChildren;

    private void OnEnable()
    {
        Invite.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        AddChildren.onClick.AddListener(WindowController.Instance.PushWindow<AddChildrenPartOneWindow>);
        AddChildren.onClick.AddListener(WindowController.Instance.ForceEnter<AddChildrenMainWindow>);
    }
    private void OnDisable()
    {
        Invite.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        AddChildren.onClick.RemoveListener(WindowController.Instance.PushWindow<AddChildrenPartOneWindow>);
        AddChildren.onClick.RemoveListener(WindowController.Instance.ForceEnter<AddChildrenMainWindow>);
    }
}
