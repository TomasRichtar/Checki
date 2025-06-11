using JSG.FortuneSpinWheel;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class RewardManager : SingletonMonoBehaviour<RewardManager>
{
    public string Title;
    public int ImageId;
    public int Price;
    public string ValidUntil;
    public int ChildrenId;
    public int ProfileId;

    public Reward RewardData;

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

    public void AddReward()
    {
        RewardData.Title = Title;
        RewardData.Price = Price;
        RewardData.ValidUntil = ValidUntil;
        RewardData.ProfileId = ProfileId;
        RewardData.ChildrenId = ChildrenId;
        RewardData.ImageId = ImageId;

        CreateRewardDatabase.Instance.CreateReward(RewardData, (response) =>
        {
            RewardAdminCollection.Instance.AddNewReward(ProfileManager.Instance.ProfileData.Id);
        });
    }

    public void DeleteReward(Reward reward)
    {
        CreateRewardDatabase.Instance.Delete(reward.Id, (response) =>
        {
            RewardAdminCollection.Instance.DeleteReward(reward);
        });
    }
}
