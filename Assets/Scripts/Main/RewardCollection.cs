using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RewardCollection : SingletonMonoBehaviour<RewardCollection>
{
    public List<Reward> AllRewards = new List<Reward>();

    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;
    [SerializeField] private RewardCollectionButton _collectionButton;

    public event Action OnRewardsLoaded;
    public event Action OnDataUpdate;

    public List<RewardCollectionButton> RewardButtons = new List<RewardCollectionButton>();


    private void OnEnable()
    {
        OnDataUpdate += LoadColletionLayout;
    }
    private void OnDisable()
    {
        OnDataUpdate -= LoadColletionLayout;
    }

    public void UpdateData()
    {
        SetAllRewards();
        LoadColletionLayout();
    }

    public void SetAllRewards()
    {
        OnRewardsLoaded?.Invoke();
    }

    public void LoadColletionLayout()
    {
        float viewportContentHeigh = 0;

        foreach (Transform item in _layout)
        {
            Destroy(item.gameObject);
        }
        if (MyGameManager.Instance.RewardList.Count > 0)
        {
            foreach (var monster in MyGameManager.Instance.RewardList)
            {
                RewardCollectionButton button = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
                button.CreateButton(monster);
                viewportContentHeigh += 142;
                RewardButtons.Add(button);
            }
        }

        float gapHeight = (MyGameManager.Instance.RewardList.Count - 1) * 32;
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
