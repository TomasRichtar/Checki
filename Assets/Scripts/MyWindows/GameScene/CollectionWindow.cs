using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectionWindow : BaseWindow
{
    [Header("Buttons")]
    public Button SpinLuckyWheelButton;

    private UnityAction _onClickOpenLuckyWheel;

    private void OnEnable()
    {
        SpinLuckyWheelButton.onClick.AddListener(OpenLuckyWheel);
    }
    private void OnDisable()
    {
        SpinLuckyWheelButton.onClick.RemoveListener(OpenLuckyWheel);
    }

    private void OpenLuckyWheel()
    {
        WindowController.Instance.PushWindow<LuckyWheelWindow>();
    }
}
