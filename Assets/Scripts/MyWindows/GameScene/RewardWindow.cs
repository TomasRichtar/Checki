using Richi;
using System.Collections;
using System.Collections.Generic;
using TastyCore._Examples.UiManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindow : BaseWindow
{
    [Header("Buttons")]
    public Button SelectRewardButton;
    public Button ExchangeButton;
    public Button WalletButton;

    private void OnEnable()
    {
        SelectRewardButton.onClick.AddListener(RewardManager.Instance.CollectReward);
        ExchangeButton.onClick.AddListener(WindowController.Instance.PushWindow<CreditExchangeWindow>);
        WalletButton.onClick.AddListener(WindowController.Instance.PushWindow<WalletWindow>);
    }
    private void OnDisable()
    {
        SelectRewardButton.onClick.RemoveListener(RewardManager.Instance.CollectReward);
        ExchangeButton.onClick.RemoveListener(WindowController.Instance.PushWindow<CreditExchangeWindow>);
        WalletButton.onClick.RemoveListener(WindowController.Instance.PushWindow<WalletWindow>);
    }
}

