using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardAdminCollectionButton : MonoBehaviour
{
    [Header("Button UI Elements")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Button _delete;
    [Header("Status Variants")]
    [SerializeField] private GameObject _failed;
    [SerializeField] private GameObject _completed;

    private Reward _reward;

    public void CreateButton(Reward reward)
    {
        _reward = reward;

        _nameText.text = reward.Title;


        SwitchState(reward);
    }

    public void SelectThis()
    {
        if (RewardAdminCollection.Instance.CheckIfExists(_reward))
        {

        }
    }
    public void DeleteThis()
    {
        if (RewardAdminCollection.Instance.CheckIfExists(_reward))
        {
            RewardManager.Instance.DeleteReward(_reward);
        }
    }
    public void SwitchState(Reward questStatus)
    {
        _completed.SetActive(false);
        _failed.SetActive(false);
        if (questStatus.Collected == 1)
        {
            _completed.SetActive(true);
        }
        else
        {
            _failed.SetActive(true);
        }
    }
}
