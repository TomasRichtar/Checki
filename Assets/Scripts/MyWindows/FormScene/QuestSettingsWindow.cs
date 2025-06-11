using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestSettingsWindow : BaseWindow
{
    [Header("Imputs")]
    [SerializeField] private CustomDropDown CustomDropDown = new CustomDropDown();

    [Header("Buttons")]
    public Button MainScene;
    public Button CreateQuest;

    private void OnEnable()
    {
        MainScene.onClick.AddListener(CloseDropDown);
        MainScene.onClick.AddListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateQuest.onClick.AddListener(WindowController.Instance.ForceEnter<CreateQuestMainWindow>);
        CreateQuest.onClick.AddListener(WindowController.Instance.PushWindow<CreateQuestOneWindow>);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(CloseDropDown);
        MainScene.onClick.RemoveListener(WindowController.Instance.PushWindow<AdminWindow>);
        CreateQuest.onClick.RemoveListener(WindowController.Instance.ForceEnter<CreateQuestMainWindow>);
        CreateQuest.onClick.RemoveListener(WindowController.Instance.PushWindow<CreateQuestOneWindow>);
    }
    public void CloseDropDown()
    {
        CustomDropDown.Close();
    }

}
