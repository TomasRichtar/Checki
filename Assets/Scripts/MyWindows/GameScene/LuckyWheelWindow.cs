using JSG.FortuneSpinWheel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelWindow : BaseWindow
{
    [SerializeField] FortuneSpinWheel _fortuneSpinWheel;

    [Header("Buttons")]
    public Button SpinLuckyWheel;

    private void OnEnable()
    {
        SpinLuckyWheel.onClick.AddListener(_fortuneSpinWheel.StartSpin);
    }
    private void OnDisable()
    {
        SpinLuckyWheel.onClick.RemoveListener(_fortuneSpinWheel.StartSpin);
    }
}
