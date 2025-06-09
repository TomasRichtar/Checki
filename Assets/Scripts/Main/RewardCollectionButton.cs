using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardCollectionButton : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public Image BackgroundImage;
    public TextMeshProUGUI Price;
    public Image ImagePrice;
    [SerializeField] private Image _image;


    private Reward _reward;

    public void CreateButton(Reward reward)
    {
        _reward = reward;

        NameText.text = reward.Title;
        Price.text = reward.Price.ToString();
        _image.sprite = SpriteManager.Instance.RewardSprites[reward.ImageId];
    }

    public void SelectThis()
    {
        if (RewardCollection.Instance.CheckIfExists(_reward))
        {
            RewardManager.Instance.SelectReward(_reward, this);
        }
    }
}
