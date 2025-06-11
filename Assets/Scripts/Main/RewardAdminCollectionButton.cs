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

    private Reward _reward;

    public void CreateButton(Reward reward)
    {
        _reward = reward;

        _nameText.text = reward.Title;
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
}
