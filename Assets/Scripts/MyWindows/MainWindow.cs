using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseWindow
{
    [Header("Buttons")]
    [SerializeField] private Button RewardButton;
    [SerializeField] private Button CustomizationButton;
    [SerializeField] private Button CollectionButton;

    [SerializeField] private Button QuestsCustom;

    private void OnEnable()
    {
        RewardButton.onClick.AddListener(WindowController.Instance.PushWindow<RewardWindow>);
        CustomizationButton.onClick.AddListener(WindowController.Instance.PushWindow<CustomizationWindow>);
        CollectionButton.onClick.AddListener(WindowController.Instance.PushWindow<CollectionWindow>);
        QuestsCustom.onClick.AddListener(WindowController.Instance.PushWindow<QuestsCustomWindow>);
    }
    private void OnDisable()
    {
        try
        {
            RewardButton.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardWindow>);
            CustomizationButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CustomizationWindow>);
            CollectionButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CollectionWindow>);
            QuestsCustom.onClick.RemoveListener(WindowController.Instance.PushWindow<QuestsCustomWindow>);
        }
        catch (System.Exception)
        {
            return;
        }
        
    }
}
