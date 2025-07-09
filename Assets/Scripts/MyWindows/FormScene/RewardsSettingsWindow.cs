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
        CreateReward.onClick.AddListener(Create);
    }
    private void OnDisable()
    {
        MainScene.onClick.RemoveListener(CloseDropDown);
        CreateReward.onClick.RemoveListener(Create);
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
            WindowController.Instance.ForceEnter<CreateRewardMainWindow>();
            WindowController.Instance.PushWindow<CreateRewardOneWindow>();
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
