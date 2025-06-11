using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class RewardAdminCollection : SingletonMonoBehaviour<RewardAdminCollection>
{
    //public List<Reward> AllData = new List<Reward>();
    //public List<Quest> MyData = new List<Quest>();

    [SerializeField] private RewardAdminCollectionButton _collectionButton;

    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;

    public event Action OnDataLoaded;
    public event Action OnDataUpdate;


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
        SetAllData();
        LoadColletionLayout();
    }

    public void SetAllData()
    {

        OnDataLoaded?.Invoke();
    }

    public void LoadColletionLayout()
    {
        float viewportContentHeigh = 0;
        float gapHeight = 0;

        foreach (Transform item in _layout)
        {
            Destroy(item.gameObject);
        }

        if (MyGameManager.Instance.RewardList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.RewardList)
            {
                RewardAdminCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
                buttonCustom.CreateButton(item);
                viewportContentHeigh += 142;
                gapHeight += 32;
            }
        }
        
        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
    }

    public bool CheckIfExists(Reward data)
    {
        if (MyGameManager.Instance.RewardList.Contains(data))
        {
            return true;
        }
        return false;
    }

    public void AddNewReward(int profileId)
    {
        CreateRewardDatabase.Instance.GetRewardData(profileId, (Reward) =>
        {
            MyGameManager.Instance.RewardList = Reward;
            WindowController.Instance.ForceExit<CreateRewardMainWindow>();
            WindowController.Instance.PushWindow<RewardsSettingsWindow>();
            OnDataUpdate?.Invoke();
        });
    }

    public void DeleteReward(Reward reward)
    {
        MyGameManager.Instance.RewardList.Remove(reward);
        OnDataUpdate?.Invoke();
    }
}
