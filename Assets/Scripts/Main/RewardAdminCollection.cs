using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class RewardAdminCollection : SingletonMonoBehaviour<RewardAdminCollection>
{
    [SerializeField] private RewardAdminCollectionButton _collectionButton;

    [SerializeField] private Transform _layout;
    [SerializeField] private Transform _viewportContent;

    [Header("Profile")]
    private float _completed = 0;
    private float _inCompleted = 0;
    [SerializeField] private LocalizedString _completeLocalization;
    [SerializeField] private LocalizedString _inCompleteLocalization;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _percentage;
    [SerializeField] private TextMeshProUGUI _inComplete;
    [SerializeField] private TextMeshProUGUI _complete;

    public event Action OnDataLoaded;
    public event Action OnDataUpdate;

    private void OnEnable()
    {
        OnDataUpdate += LoadColletionLayout;
        _completeLocalization.Arguments = new object[] { _complete };
        _completeLocalization.StringChanged += UpdateCompleteText;
        _inCompleteLocalization.Arguments = new object[] { _inComplete };
        _inCompleteLocalization.StringChanged += UpdateInCompleteText;
    }
    private void OnDisable()
    {
        OnDataUpdate -= LoadColletionLayout;
        _completeLocalization.StringChanged -= UpdateCompleteText;
        _inCompleteLocalization.StringChanged -= UpdateInCompleteText;
    }

    public void UpdateCompleteText(string value)
    {
        _complete.text = value;
    }
    public void UpdateInCompleteText(string value)
    {
        _inComplete.text = value;
    }

    public void UpdateData()
    {
        SetAllData();
        LoadColletionLayout();
        LoadCollectionSlider();
    }

    public void SetAllData()
    {

        OnDataLoaded?.Invoke();
    }

    public void LoadCollectionSlider()
    {
        float totalValue = MyGameManager.Instance.RewardList.Count;

        _completed = 0;
        _inCompleted = 0;
        foreach (var item in MyGameManager.Instance.RewardList)
        {
            if (item.Collected == 1)
            {
                _completed++;
            }
            else
            {
                _inCompleted++;
            }
        }
        _slider.value = _completed;
        _completeLocalization.Arguments[0] = _completed.ToString();
        _completeLocalization.RefreshString();
        _inCompleteLocalization.Arguments[0] = _inCompleted.ToString();
        _inCompleteLocalization.RefreshString();
        if (totalValue != 0)
        {
            _slider.maxValue = totalValue;
            _percentage.text = (_completed / totalValue) * 100 + "%";
        }
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

    public void AddNewReward(int childId)
    {
        CreateRewardDatabase.Instance.GetRewardData(childId, (Reward) =>
        {
            MyGameManager.Instance.RewardList = Reward;
            WindowController.Instance.ForceExit<CreateRewardMainWindow>();
            WindowController.Instance.PushWindow<RewardsSettingsWindow>();
            UpdateData();
            OnDataUpdate?.Invoke();
        });
    }

    public void DeleteReward(Reward reward)
    {
        MyGameManager.Instance.RewardList.Remove(reward);
        OnDataUpdate?.Invoke();
    }
}
