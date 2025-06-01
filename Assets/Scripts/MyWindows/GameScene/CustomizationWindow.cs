using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationWindow : BaseWindow
{
    [Header("Buttons")]
    public Button SelectMonsterButton;

    private void OnEnable()
    {
        SelectMonsterButton.onClick.AddListener(WindowController.Instance.PushWindow<CollectionWindow>);
    }
    private void OnDisable()
    {
        SelectMonsterButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CollectionWindow>);
    }
}
