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
        _onClickOpenLuckyWheel = () => WindowController.Instance.PushWindow<LuckyWheelWindow>();
        SpinLuckyWheelButton.onClick.AddListener(_onClickOpenLuckyWheel);
    }
    private void OnDisable()
    {
        SpinLuckyWheelButton.onClick.RemoveListener(WindowController.Instance.PushWindow<LuckyWheelWindow>);
    }
}
