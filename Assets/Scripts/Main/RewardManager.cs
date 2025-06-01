using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class RewardManager : SingletonMonoBehaviour<RewardManager>
{
    public Reward SelectedReward;

    public void SelectReward(Reward reward, RewardCollectionButton button)
    {
        SelectedReward = reward;
        foreach (var item in RewardCollection.Instance.RewardButtons)
        {
            item.BackgroundImage.color = new Color(0.541f, 0.239f, 0.592f, 0.8f);
            item.Price.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            item.ImagePrice.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            item.NameText.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
        button.BackgroundImage.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        button.Price.color = new Color(0.541f, 0.239f, 0.592f, 0.8f);
        button.ImagePrice.color = new Color(0.541f, 0.239f, 0.592f, 0.8f);
        button.NameText.color = new Color(0.541f, 0.239f, 0.592f, 0.8f);
    }
    public void CollectReward()
    {
        if (SelectedReward == null)
        {
            WindowController.Instance.PushPopUpWindow(
               "SelectRewardTitle",
               "SelectRewardBody",
               "Continue",
               null);
            return;
        }

        if (CheckIfValid(SelectedReward))
        {
            WindowController.Instance.PushPopUpWindow(
               "CollectRewardTitle",
               "CollectRewardBody",
               "Continue",
               BuyReward,
               "Exit",
               null);

        }
        else
        {
            WindowController.Instance.PushPopUpWindow(
                "CollectMoreCreditTitle",
                "CollectMoreCreditBody",
                "Continue",
                null);
        }
    }

    public bool CheckIfValid(Reward reward)
    {
        if (reward.Price <= MyGameManager.Instance.UserCredit)
        {
            return true;
        }
        return false;
    }

    public void BuyReward()
    {
        Debug.Log("Reward was collected");
    }
}
