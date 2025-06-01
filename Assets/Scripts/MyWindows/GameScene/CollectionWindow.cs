using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionWindow : BaseWindow
{
    [Header("Buttons")]
    public Button SpinLuckyWheelButton;

    private void OnEnable()
    {
        SpinLuckyWheelButton.onClick.AddListener(WindowController.Instance.PushWindow<LuckyWheelWindow>);
    }
    private void OnDisable()
    {
        SpinLuckyWheelButton.onClick.RemoveListener(WindowController.Instance.PushWindow<LuckyWheelWindow>);
    }
}
