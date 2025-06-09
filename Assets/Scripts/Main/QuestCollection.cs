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
        var unlockedSet = new HashSet<string>(MyGameManager.Instance.QuestList);

        foreach (var reward in AllData)
        {
            if (unlockedSet.Contains(reward.Title))
            {
                MyData.Add(reward);
            }
        }

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

        foreach (var item in MyData)
        {
            QuestCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutCustom);
            buttonCustom.CreateButton(item);
            viewportContentHeighCustom += 142;
            gapHeightCustom += 32;
            break;
            /*
            switch (item.QuestType)
            {
                case QuestType.Custom:
                    QuestCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutCustom);
                    buttonCustom.CreateButton(item);
                    viewportContentHeighCustom += 142;
                    gapHeightCustom += 32;
                    break;
                case QuestType.Checki:
                    QuestCollectionButton buttonChecki = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layoutChecki);
                    buttonChecki.CreateButton(item);
                    viewportContentHeighChecki += 142;
                    gapHeightChecki += 32;
                    break;
            }*/
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
