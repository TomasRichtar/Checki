using System;
using System.Collections.Generic;
using UnityEngine;

public class CallendarCollection : MonoBehaviour
{
    public List<CallendarDay> MyData = new List<CallendarDay>();

    [SerializeField] private CallendarCollectionButton _collectionButton;

    [Header("Custom")]
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
        MyData.Clear();

        for (int i = 0; i < 10; i++)
        {
            DateTime day = DateTime.Today.AddDays(i);
            Days myDays = (Days)(int)day.DayOfWeek;

            CallendarDay callendarDay = new CallendarDay
            {
                Day = day.DayOfWeek.ToString(),
                Month = day.Month.ToString(),
                Year = day.Year.ToString(),
                DayNumber = day.Day,
                DayEnum = myDays
            };

            MyData.Add(callendarDay);
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
            CallendarCollectionButton buttonCustom = Instantiate(_collectionButton, Vector3.zero, Quaternion.identity, _layout);
            buttonCustom.CreateButton(item);
            viewportContentHeigh += 182;
            gapHeight += 34;
        }

        viewportContentHeigh += gapHeight;

        RectTransform rt = _viewportContent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = viewportContentHeigh;
        rt.sizeDelta = size;
    }

    public bool CheckIfExists()
    {
        return true;
    }
}
