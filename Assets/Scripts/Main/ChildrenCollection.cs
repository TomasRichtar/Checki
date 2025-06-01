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

    private void Start()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        SetAllData();
        LoadColletionLayout();
    }

    public void SetAllData()
    {
        foreach (var data in MyGameManager.Instance.ChildrenList)
        {
            Children child = ChildrenManager.Instance.CreateChildren(data);
            MyData.Add(child);
        }

        OnDataLoaded?.Invoke();
    }

    public void LoadColletionLayout()
    {
        //float viewportContentHeighChecki = 0;
        //float viewportContentHeighCustom = 0;
        //float gapHeightChecki = 0;
        //float gapHeightCustom = 0;

        foreach (Transform item in _layoutChecki)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in MyData)
        {
            ChildrenCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutChecki);
            buttonCustom.CreateButton(item);
            //viewportContentHeighCustom += 142;
            //gapHeightCustom += 32;
        }
        ChildrenCollectionAddButon buttonAddCustom = Instantiate(_addButton, Vector3.zero, Quaternion.identity, _layoutChecki);
        buttonAddCustom.CreateButton();
        //viewportContentHeighCustom += 142;
        //gapHeightCustom += 32;

        //viewportContentHeighChecki += gapHeightChecki;

        //RectTransform rt = _viewportContentChecki.GetComponent<RectTransform>();
        //Vector2 size = rt.sizeDelta;
        //size.y = viewportContentHeighChecki;
        //rt.sizeDelta = size;


        float viewportContentHeighChecki = 0;
        float viewportContentHeighCustom = 0;
        float gapHeightChecki = 0;
        float gapHeightCustom = 0;

        foreach (Transform item in _layoutQuestSettings)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in MyData)
        {
            ChildrenQuestCollectionButton buttonCustom = Instantiate(_collectionQuestButton, Vector3.zero, Quaternion.identity, _layoutQuestSettings);
            buttonCustom.CreateButton(item);
            viewportContentHeighCustom += 142;
            gapHeightCustom += 32;
        }
        
        viewportContentHeighChecki += gapHeightChecki;

        RectTransform rt = _viewportContentQuestSettings.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeighChecki;
        rt.sizeDelta = size;

    }
    public void AddNewChildren(Children children)
    {
        MyData.Add(children);

        MyGameManager.Instance.ChildrenList.Add(children.Name);
        OnDataUpdate?.Invoke();
    }
}
