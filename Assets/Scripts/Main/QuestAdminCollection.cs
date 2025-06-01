using System;
using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class QuestAdminCollection : SingletonMonoBehaviour<QuestAdminCollection>
{
    public List<Quest> AllData = new List<Quest>();
    public List<Quest> MyData = new List<Quest>();

    [SerializeField] private QuestAdminCollectionButton _collectionButton;

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
            if (unlockedSet.Contains(reward.Name))
            {
                MyData.Add(reward);
            }
        }

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

        foreach (var item in MyData)
        {
            QuestAdminCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
            buttonCustom.CreateButton(item);
            viewportContentHeigh += 142;
            gapHeight += 32;
        }

        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
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
