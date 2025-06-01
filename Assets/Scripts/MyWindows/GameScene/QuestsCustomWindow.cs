using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsCustomWindow : BaseWindow
{
    [Header("Buttons")]
    [SerializeField] private Button RewardButton;
    [SerializeField] private Button CustomizationButton;
    [SerializeField] private Button CollectionButton;
    private void OnEnable()
    {
        RewardButton.onClick.AddListener(WindowController.Instance.PushWindow<RewardWindow>);
        CustomizationButton.onClick.AddListener(WindowController.Instance.PushWindow<CustomizationWindow>);
        CollectionButton.onClick.AddListener(WindowController.Instance.PushWindow<CollectionWindow>);
    }
    private void OnDisable()
    {
        RewardButton.onClick.RemoveListener(WindowController.Instance.PushWindow<RewardWindow>);
        CustomizationButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CustomizationWindow>);
        CollectionButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CollectionWindow>);
    }
}
