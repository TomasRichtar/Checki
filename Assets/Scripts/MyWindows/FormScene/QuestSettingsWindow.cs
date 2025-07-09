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
        CreateQuest.onClick.AddListener(Create);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(CloseDropDown);
        CreateQuest.onClick.RemoveListener(Create);
    }
    public void Create()
    {
        if (MyGameManager.Instance.ChildrenId == 0)
        {
            WindowController.Instance.PushPopUpWindow(
                  "FirstCreateChildTitle",
                  "FirstCreateChild",
                  "Continue",
                  null);
            return;
        }
        else
        {
            WindowController.Instance.ForceEnter<CreateQuestMainWindow>();
            WindowController.Instance.PushWindow<CreateQuestOneWindow>();
        }
    }

    public void CloseDropDown()
    {
        CustomDropDown.Close();
        StartCoroutine(OpenAdminWindowAfterDelay());
    }

    private IEnumerator OpenAdminWindowAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        WindowController.Instance.PushWindow<AdminWindow>();
    }

}
