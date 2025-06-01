using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.UI;

public class RewardCollection : SingletonMonoBehaviour<RewardCollection>
{
    public List<Reward> AllRewards = new List<Reward>();
    public List<Reward> MyRewards = new List<Reward>();

    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private RewardCollectionButton _collectionButton;

    public event Action OnRewardsLoaded;
    public event Action OnNewMonsterUnlocked;

    public List<RewardCollectionButton> RewardButtons = new List<RewardCollectionButton>();


    private void OnEnable()
    {
        OnNewMonsterUnlocked += LoadColletionLayout;
    }
    private void OnDisable()
    {
        OnNewMonsterUnlocked -= LoadColletionLayout;
    }

    private void Start()
    {
        UpdateMonsterData();
    }

    public void UpdateMonsterData()
    {
        SetAllRewards();
        LoadColletionLayout();
    }

    public void SetAllRewards()
    {
        var unlockedSet = new HashSet<string>(MyGameManager.Instance.RewardsList);

        foreach (var reward in AllRewards)
        {
            if (unlockedSet.Contains(reward.Name))
            {
                MyRewards.Add(reward);
            }
        }

        OnRewardsLoaded?.Invoke();
    }

    public void LoadColletionLayout()
    {
        float viewportContentHeigh = 0;

        foreach (Transform item in _layout)
        {
            Destroy(item.gameObject);
        }

        foreach (var monster in MyRewards)
        {
            RewardCollectionButton button = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
            button.CreateButton(monster);
            viewportContentHeigh += 142;
            RewardButtons.Add(button);
        }

        float gapHeight = (MyRewards.Count - 1) * 32;
        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
    }

    public bool CheckIfExists(Reward reward)
    {
        if (AllRewards.Contains(reward))
        {
            return true;
        }
        return false;
    }
}
