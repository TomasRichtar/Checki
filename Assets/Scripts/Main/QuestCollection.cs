using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class QuestCollection : SingletonMonoBehaviour<QuestCollection>
{
    public List<Quest> AllData = new List<Quest>();
    public List<Quest> MyData = new List<Quest>();

    [SerializeField] private QuestCollectionButton _collectionButton;

    [Header("Ckecki")]
    [SerializeField] private Transform _layoutChecki;
    [SerializeField] private Transform _viewportContentChecki;
    [Header("Custom")]
    [SerializeField] private Transform _layoutCustom;
    [SerializeField] private Transform _viewportContentCustom;

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
        float viewportContentHeighChecki = 0;
        float viewportContentHeighCustom = 0;
        float gapHeightChecki = 0;
        float gapHeightCustom = 0;

        foreach (Transform item in _layoutChecki)
        {
            Destroy(item.gameObject);
        }

        if (MyGameManager.Instance.QuestList.Count > 0)
        {
            foreach (var item in MyGameManager.Instance.QuestList)
            {
                QuestCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutCustom);
                buttonCustom.CreateButton(item);
                viewportContentHeighCustom += 142;
                gapHeightCustom += 32;
            }
        }

        viewportContentHeighChecki += gapHeightChecki;
        viewportContentHeighCustom += gapHeightCustom;

        RectTransform rt = _viewportContentChecki.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeighChecki;
        rt.sizeDelta = size;

        RectTransform rtCustom = _viewportContentCustom.GetComponent<RectTransform>();
        Vector2 sizeCustom = rtCustom.sizeDelta;
        sizeCustom.y = viewportContentHeighCustom;
        rtCustom.sizeDelta = sizeCustom;
    }

    public bool CheckIfExists(Quest data)
    {
        if (AllData.Contains(data))
        {
            return true;
        }
        return false;
    }
}
