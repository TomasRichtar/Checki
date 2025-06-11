using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class ChildrenCollection : SingletonMonoBehaviour<ChildrenCollection>
{
    public List<Children> MyData = new List<Children>();

    [SerializeField] private ChildrenCollectionButton _collectionButton;
    [SerializeField] private ChildrenCollectionAddButon _addButton;

    [SerializeField] private Transform _layoutChecki;
    [SerializeField] private Transform _viewportContentChecki;

    [SerializeField] private ChildrenQuestCollectionButton _collectionQuestButton;

    [SerializeField] private Transform _layoutQuestSettings;
    [SerializeField] private Transform _viewportContentQuestSettings;

    [SerializeField] private Transform _layoutRewardsSettings;
    [SerializeField] private Transform _viewportContentRewardsSettings;

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
        Debug.Log("Event that does nothing?");
        OnDataLoaded?.Invoke();
    }

    public void LoadColletionLayout()
    {
        foreach (Transform item in _layoutChecki)
        {
            Destroy(item.gameObject);
        }

        if (MyGameManager.Instance.ChildrenList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.ChildrenList)
            {
                ChildrenCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutChecki);
                buttonCustom.CreateButton(item);
            }
        }
        ChildrenCollectionAddButon buttonAddCustom = Instantiate(_addButton, Vector3.zero, Quaternion.identity, _layoutChecki);
        buttonAddCustom.CreateButton();


        float viewportContentHeighChecki = 0;
        float viewportContentHeighCustom = 0;
        float gapHeightChecki = 0;
        float gapHeightCustom = 0;

        foreach (Transform item in _layoutQuestSettings)
        {
            Destroy(item.gameObject);
        }
        if (MyGameManager.Instance.ChildrenList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.ChildrenList)
            {
                ChildrenQuestCollectionButton buttonCustom = Instantiate(_collectionQuestButton, Vector3.zero, Quaternion.identity, _layoutQuestSettings);
                buttonCustom.CreateButton(item);
                viewportContentHeighCustom += 142;
                gapHeightCustom += 32;
            }
        }
        
        viewportContentHeighChecki += gapHeightChecki;

        RectTransform rt = _viewportContentQuestSettings.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeighChecki;
        rt.sizeDelta = size;

        foreach (Transform item in _layoutRewardsSettings)
        {
            Destroy(item.gameObject);
        }

        if (MyGameManager.Instance.ChildrenList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.ChildrenList)
            {
                ChildrenQuestCollectionButton buttonCustom = Instantiate(_collectionQuestButton, Vector3.zero, Quaternion.identity, _layoutRewardsSettings);
                buttonCustom.CreateButton(item);
            }
        }

        RectTransform rtRewards = _viewportContentRewardsSettings.GetComponent<RectTransform>();
        Vector2 sizeRewards = rtRewards.sizeDelta;
        sizeRewards.y = viewportContentHeighChecki;
        rtRewards.sizeDelta = sizeRewards;

    }
    public void AddNewChildren(int profileId)
    {
        ChildrenDatabase.Instance.GetChildrenData(profileId, (Children) =>
        {
            MyGameManager.Instance.ChildrenList = Children;
            OnDataUpdate?.Invoke();
        });
    }
}
