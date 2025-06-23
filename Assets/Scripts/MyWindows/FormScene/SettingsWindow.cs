using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : BaseWindow
{
    [Header("Buttons")]
    public Button MainScene;
    public Button Edit;
    public Button PasswordChange;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        Edit.onClick.AddListener(OpenEdit);
        PasswordChange.onClick.AddListener(OpenPasswordChangeWindow);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        Edit.onClick.AddListener(OpenEdit);
        PasswordChange.onClick.AddListener(OpenPasswordChangeWindow);
    }

    private void OpenEdit()
    {
        WindowController.Instance.ForceEnter<AboutFamilyMainWindow>();
        WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>();
        WindowController.Instance.GetWindow<AboutFamilyPartOneWindow>().FillUpData();
        WindowController.Instance.GetWindow<AboutFamilyPartTreeWindow>().IsCreatingNew = false;
    }
    public void OpenPasswordChangeWindow()
    {
        WindowController.Instance.PushPopUpWindow(
               "PasswordChange",
               "Continue",
               (oldPass, newPass) => ProfileManager.Instance.ChangePassword(oldPass, newPass),
               "Exit",
               null,
               "OldPassword",
               "NewPassword");
    }
}
